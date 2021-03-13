using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetRobot.RobotParts
{
    public interface IDistanceCalculator
    {
        Task Begin();
        event EventHandler<DistanceCalculatedEvent> OnDistanceCalculated;
    }

    class DistanceCalculator
    {
        private readonly GpioController _controller;

        internal int pinTrigger = 17;
        internal int pinEcho = 18;

        public DistanceCalculator(GpioController controller)
        {
            _controller = controller;
            _controller.OpenPin(pinTrigger, PinMode.Output);
            _controller.OpenPin(pinEcho, PinMode.Input);

            var timer = new System.Timers.Timer() { Interval = 2000 };
            timer.Elapsed += OnTimerElapsed;
        }

        public event EventHandler<DistanceCalculatedEvent> OnDistanceCalculated;

        public async Task Begin()
        {
            await Task.Run(() =>
            {
                var timer = new System.Timers.Timer() { Interval = 2000 };
                timer.Elapsed += OnTimerElapsed;
            });            
        }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var stopwatch = new Stopwatch();
            // Send a high low signal to the trigger pin
            _controller.Write(pinTrigger, PinValue.High);
            Thread.Sleep(new TimeSpan(100L));
            _controller.Write(pinTrigger, PinValue.Low);

            // Start the clock
            stopwatch.Start();

            var result = _controller.WaitForEvent(pinEcho, PinEventTypes.Falling, new TimeSpan(0, 0, 1));

            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds <= 400)
            {
                OnDistanceCalculated?.Invoke(this, new DistanceCalculatedEvent(stopwatch.ElapsedMilliseconds, "Too close to calculate distance", false));
            }
            else
            {
                // Magic number which is due to the type of sensor used
                var multiplier = 34326;
                // The distance is there and back again so half it
                double distance = (stopwatch.ElapsedMilliseconds * multiplier) / 2;

                OnDistanceCalculated?.Invoke(this, new DistanceCalculatedEvent(distance, $"Distance to nearest object is {string.Format("{0:0.00}", distance)}", true));
            }            
        }
    }

    public class DistanceCalculatedEvent
    {
        public DistanceCalculatedEvent(double distance, string message, bool success)
        {
            Distance = distance;
            Message = message;
        }

        public bool Success { get; private set; }

        public double Distance { get; private set; }
        public string Message { get; private set; }
    }
}
