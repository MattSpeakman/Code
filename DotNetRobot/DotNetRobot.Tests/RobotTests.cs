using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Device.Gpio;

namespace DotNetRobot.Tests
{
    [TestClass]
    public class RobotTests
    {
        private Mock<IGpioControllerWrapper> _mockControllerWrapper;
        private Robot instance;

        [TestInitialize]
        public void Init()
        {
            _mockControllerWrapper = new Mock<IGpioControllerWrapper>();
            _mockControllerWrapper.Setup(x => x.ClosePin(It.IsAny<int>()));
            _mockControllerWrapper.Setup(x => x.OpenPin(It.IsAny<int>(), It.IsAny<PinMode>()));
            _mockControllerWrapper.Setup(x => x.Write(It.IsAny<int>(), It.IsAny<PinValue>()));
            instance = new Robot(_mockControllerWrapper.Object);
        }

        [TestMethod]
        public void Robot_PinsAreSetCorrectly()
        {
            Assert.AreEqual(10, instance._motorBForwardsPin);
            Assert.AreEqual(9, instance._motorBBackwardsPin);
            Assert.AreEqual(8, instance._motorAForwardsPin);
            Assert.AreEqual(7, instance._motorABackwardsPin);
        }

        [TestMethod]
        public void Robot_Dispose_ClosesThePins()
        {
            instance.Dispose();
            _mockControllerWrapper.Verify(x => x.ClosePin(instance._motorBForwardsPin), Times.Once);
            _mockControllerWrapper.Verify(x => x.ClosePin(instance._motorBBackwardsPin), Times.Once);
            _mockControllerWrapper.Verify(x => x.ClosePin(instance._motorAForwardsPin), Times.Once);
            _mockControllerWrapper.Verify(x => x.ClosePin(instance._motorABackwardsPin), Times.Once);
            _mockControllerWrapper.Verify(x => x.Dispose(), Times.Once);
        }

        [TestMethod]
        public void Robot_Stop_SetsAllPinsToLow()
        {
            instance.Stop();
            _mockControllerWrapper.Verify(x => x.Write(instance._motorBForwardsPin, PinValue.Low), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorBBackwardsPin, PinValue.Low), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorAForwardsPin, PinValue.Low), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorABackwardsPin, PinValue.Low), Times.Once);
        }

        [TestMethod]
        public void Robot_Forwards_SetsThePinsCorrectly()
        {
            instance.Forwards();
            _mockControllerWrapper.Verify(x => x.Write(instance._motorBForwardsPin, PinValue.High), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorBBackwardsPin, PinValue.Low), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorAForwardsPin, PinValue.High), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorABackwardsPin, PinValue.Low), Times.Once);
        }

        [TestMethod]
        public void Robot_Backwards_SetsThePinsCorrectly()
        {
            instance.Backwards();
            _mockControllerWrapper.Verify(x => x.Write(instance._motorBForwardsPin, PinValue.Low), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorBBackwardsPin, PinValue.High), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorAForwardsPin, PinValue.Low), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorABackwardsPin, PinValue.High), Times.Once);
        }

        [TestMethod]
        public void Robot_Right_SetsThePinsCorrectly()
        {
            instance.Right();
            _mockControllerWrapper.Verify(x => x.Write(instance._motorAForwardsPin, PinValue.High), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorABackwardsPin, PinValue.Low), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorBForwardsPin, PinValue.Low), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorBBackwardsPin, PinValue.High), Times.Once);
        }

        [TestMethod]
        public void Robot_Left_SetsThePinsCorrectly()
        {
            instance.Left();
            _mockControllerWrapper.Verify(x => x.Write(instance._motorAForwardsPin, PinValue.Low), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorABackwardsPin, PinValue.High), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorBForwardsPin, PinValue.High), Times.Once);
            _mockControllerWrapper.Verify(x => x.Write(instance._motorBBackwardsPin, PinValue.Low), Times.Once);

        }
    }
}
