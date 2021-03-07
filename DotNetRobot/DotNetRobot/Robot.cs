using System;
using System.Device.Gpio;
using System.Threading;

namespace DotNetRobot
{
    public class Robot : IDisposable
    {
        private int motorBForwardsPin = 10;
        private int motorBBackwardsPin = 9;
        private int motorAForwardsPin = 8;
        private int motorABackwardsPin = 7;

        private GpioController controller;

        public Robot()
        {
            controller = new GpioController(PinNumberingScheme.Logical);
            controller.OpenPin(motorAForwardsPin, PinMode.Output);
            controller.OpenPin(motorABackwardsPin, PinMode.Output);
            controller.OpenPin(motorBForwardsPin, PinMode.Output);
            controller.OpenPin(motorBBackwardsPin, PinMode.Output);
        }

        private void SetPins(PinValue motorAForwards, PinValue motorABackwards, PinValue motorBForwards, PinValue motorBBackwards)
        {
            controller.Write(this.motorAForwardsPin, motorAForwards);
            controller.Write(this.motorABackwardsPin, motorABackwards);
            controller.Write(this.motorBForwardsPin, motorBForwards);
            controller.Write(this.motorBBackwardsPin, motorBBackwards);
        }

        public void Stop() => SetPins(PinValue.Low, PinValue.Low, PinValue.Low, PinValue.Low);

        public void Backwards()=> SetPins(PinValue.Low, PinValue.High, PinValue.Low, PinValue.High);

        public void Forwards() => SetPins(PinValue.High, PinValue.Low, PinValue.High, PinValue.Low);

        public void Left() => SetPins(PinValue.Low, PinValue.High, PinValue.High, PinValue.Low);

        public void Right() => SetPins(PinValue.High, PinValue.Low, PinValue.Low, PinValue.High);

        public void Dispose()
        {
            controller.ClosePin(motorAForwardsPin);
            controller.ClosePin(motorABackwardsPin);
            controller.ClosePin(motorBForwardsPin);
            controller.ClosePin(motorBBackwardsPin);
        }
    }

}