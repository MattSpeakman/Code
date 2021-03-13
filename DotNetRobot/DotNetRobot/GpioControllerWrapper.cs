using System;
using System.Device.Gpio;
using System.Diagnostics.CodeAnalysis;

namespace DotNetRobot
{
    public interface IGpioControllerWrapper : IDisposable
    {
        void ClosePin(int pinNumber);
        void OpenPin(int pinNumber, PinMode mode);
        void RegisterCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback);
        void UnregisterCallbackForPinValueChangedEvent(int pinNumber, PinChangeEventHandler callback);
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

        public void RegisterCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback) 
            => _controller.RegisterCallbackForPinValueChangedEvent(pinNumber, eventTypes, callback);

        public void UnregisterCallbackForPinValueChangedEvent(int pinNumber, PinChangeEventHandler callback)
            => _controller.UnregisterCallbackForPinValueChangedEvent(pinNumber, callback);

        public void Write(int pinNumber, PinValue pinValue) => _controller.Write(pinNumber, pinValue);
    }
}
