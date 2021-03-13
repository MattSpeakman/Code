using System;
using System.Device.Gpio;

namespace DotNetRobot.RobotParts
{
    public interface ILocomotion : IDisposable
    {
        void Stop();

        void Backwards();

        void Forwards();

        void Left();

        void Right();
    }

    public class Locomotion : ILocomotion
    {
        private readonly IGpioControllerWrapper _controller;
        internal readonly int _motorBForwardsPin = 10;
        internal readonly int _motorBBackwardsPin = 9;
        internal readonly int _motorAForwardsPin = 8;
        internal readonly int _motorABackwardsPin = 7;

        public Locomotion(IGpioControllerWrapper controllerWrapper)
        {
            _controller = controllerWrapper;
            _controller.OpenPin(_motorAForwardsPin, PinMode.Output);
            _controller.OpenPin(_motorABackwardsPin, PinMode.Output);
            _controller.OpenPin(_motorBForwardsPin, PinMode.Output);
            _controller.OpenPin(_motorBBackwardsPin, PinMode.Output);
        }

        internal void SetPins(PinValue motorAForwards, PinValue motorABackwards, PinValue motorBForwards, PinValue motorBBackwards)
        {
            _controller.Write(_motorAForwardsPin, motorAForwards);
            _controller.Write(_motorABackwardsPin, motorABackwards);
            _controller.Write(_motorBForwardsPin, motorBForwards);
            _controller.Write(_motorBBackwardsPin, motorBBackwards);
        }

        public void Stop() => SetPins(PinValue.Low, PinValue.Low, PinValue.Low, PinValue.Low);

        public void Backwards() => SetPins(PinValue.Low, PinValue.High, PinValue.Low, PinValue.High);

        public void Forwards() => SetPins(PinValue.High, PinValue.Low, PinValue.High, PinValue.Low);

        public void Left() => SetPins(PinValue.Low, PinValue.High, PinValue.High, PinValue.Low);

        public void Right() => SetPins(PinValue.High, PinValue.Low, PinValue.Low, PinValue.High);

        public void Dispose()
        {
            _controller.ClosePin(_motorAForwardsPin);
            _controller.ClosePin(_motorABackwardsPin);
            _controller.ClosePin(_motorBForwardsPin);
            _controller.ClosePin(_motorBBackwardsPin);
            _controller.Dispose();
        }
    }

}