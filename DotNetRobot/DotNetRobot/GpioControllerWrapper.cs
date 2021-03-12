using System;
using System.Device.Gpio;
using System.Diagnostics.CodeAnalysis;

namespace DotNetRobot
{
    public interface IGpioControllerWrapper : IDisposable
    {
        void OpenPin(int pinNumber, PinMode mode);
        void ClosePin(int pinNumber);
        void Write(int pinNumber, PinValue pinValue);
    }

    [ExcludeFromCodeCoverage] // Not unit testable
    class GpioControllerWrapper : IGpioControllerWrapper
    {
        private readonly GpioController _controller;
        public GpioControllerWrapper(GpioController gpioController)
        {
            _controller = gpioController;
        }

        public void ClosePin(int pinNumber) => _controller.ClosePin(pinNumber);

        public void Dispose() => _controller.Dispose();

        public void OpenPin(int pinNumber, PinMode mode) => _controller.OpenPin(pinNumber, mode);

        public void Write(int pinNumber, PinValue pinValue) => _controller.Write(pinNumber, pinValue);
    }
}
