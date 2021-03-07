import RPi.GPIO as GPIO
import time

class Robot:
    __pinMotorAForwards = 10
    __pinMotorABackwards = 9
    __pinMotorBForwards = 8
    __pinMotorBBackwards = 7

    def __init__(self):
        GPIO.setwarnings(False)
        GPIO.setmode(GPIO.BCM)
        GPIO.setup(self.__pinMotorAForwards, GPIO.OUT)
        GPIO.setup(self.__pinMotorABackwards, GPIO.OUT)
        GPIO.setup(self.__pinMotorBForwards, GPIO.OUT)
        GPIO.setup(self.__pinMotorBBackwards, GPIO.OUT)

    def __setPins(self, motorAForward, motorABackward, motorBForward, motorBBackward):
        GPIO.output(self.__pinMotorAForwards, motorAForward)
        GPIO.output(self.__pinMotorABackwards, motorABackward)
        GPIO.output(self.__pinMotorBForwards, motorBForward)
        GPIO.output(self.__pinMotorBBackwards, motorBBackward)

    def stop(self):
        self.__setPins(0, 0, 0, 0)

    def forwards(self):
        self.__setPins(1, 0, 1, 0)

    def backwards(self):
        self.__setPins(0, 1, 0, 1)

    def left(self):
        self.__setPins(0, 1, 1, 0)

    def right(self):
        self.__setPins(1, 0, 0, 1)