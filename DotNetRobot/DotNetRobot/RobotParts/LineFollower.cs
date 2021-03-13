using System;
using System.Device.Gpio;
using System.Diagnostics.CodeAnalysis;

namespace DotNetRobot.RobotParts
{
    public interface ILineFollower : IDisposable
    {
        event PinChangeEventHandler OnBlackRegistered;
        event PinChangeEventHandler OnWhiteRegistered;
    }

    [ExcludeFromCodeCoverage] // Simple eventing system. Not unit testable
    class LineFollower : ILineFollower
    {
        internal readonly int pinLineFollower = 25;

        private readonly IGpioControllerWrapper _controller;

        public LineFollower(IGpioControllerWrapper controller)
        {
            _controller = controller;
            _controller.OpenPin(pinLineFollower, PinMode.Input);
        }

        public event PinChangeEventHandler OnBlackRegistered
        {
            add { _controller.RegisterCallbackForPinValueChangedEvent(pinLineFollower, PinEventTypes.Rising, value); }
            remove { _controller.UnregisterCallbackForPinValueChangedEvent(pinLineFollower, value); }
        }

        public event PinChangeEventHandler OnWhiteRegistered
        {
            add { _controller.RegisterCallbackForPinValueChangedEvent(pinLineFollower, PinEventTypes.Falling, value); }
            remove { _controller.UnregisterCallbackForPinValueChangedEvent(pinLineFollower, value); }
        }

        public void Dispose()
        {
            _controller.ClosePin(pinLineFollower);
            _controller.Dispose();
        }
    }
}
