using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;
using System.Threading;
using Timer= MicrowaveOvenClasses.Boundary.Timer;

namespace MW.Test.Integration
{
    [TestFixture]
    class IT3_Button
    {
        private IDoor _door;
        private IOutput _output;
        private ILight _light;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IButton _uutPowerButton;
        private IButton _uutTimeButton;
        private IButton _uutStartCancel;
        private ICookController _cookController;
        private IUserInterface _userInterface;

        [SetUp]
        public void SetUP()
        {
            _output = Substitute.For<IOutput>();
            _door = new Door();
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _uutPowerButton = new Button();
            _uutTimeButton = new Button();
            _uutStartCancel = new Button();
            _cookController = new CookController(_timer, _display, _powerTube) { UI = _userInterface };
            _userInterface = new UserInterface(_uutPowerButton, _uutTimeButton, _uutStartCancel, _door, _display, _light, _cookController);
        }

        [Test]
        public void PowerButtonIsPressed_PowerIs50()
        {
            _door.Open();
            _door.Close();
            _uutPowerButton.Press();
            _output.Received().OutputLine("Display shows: 50 W");
        }

        [Test]
        public void PowerButtonIsPressed2_PowerIs100()
        {
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _output.Received().OutputLine("Display shows: 100 W");
        }

        [Test]
        public void PowerButtonIsPressed15_PowerIs50()
        {
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _output.Received().OutputLine("Display shows: 50 W");
        }

        [Test]
        public void PowerButtonIsPressed_CancelButtonPressed_DisplayClear()
        {
            _uutPowerButton.Press();
            _uutStartCancel.Press();
            _output.Received().OutputLine("Display cleared");
        }

        [Test]
        public void PowerButton_DoorOpened_DisplayClear()
        {
            _uutPowerButton.Press();
            _door.Open();
            _output.Received().OutputLine("Display cleared");
        }

        [Test]
        public void PowerButton_DoorOpened_LightOn()
        {
            _uutPowerButton.Press();
            _door.Open();
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void PowerButton_TimeButton_Time1()
        {
            _uutPowerButton.Press();
            _uutTimeButton.Press();
            _output.Received().OutputLine("Display shows: 01:00");
        }

        [Test]
        public void PowerButton_2TimeButton_Time2()
        {
            _uutPowerButton.Press();
            _uutTimeButton.Press();
            _uutTimeButton.Press();
            _output.Received().OutputLine("Display shows: 02:00");
        }

        [Test]
        public void SetTime_StartButton_CookerIsCalled()
        {
            _uutPowerButton.Press();
            _uutTimeButton.Press();
            _uutStartCancel.Press();
            Thread.Sleep(5000);
            _output.Received().OutputLine("Display shows: 00:56");
        }

        [Test]
        public void SetTime_DoorOpened_DisplayClear()
        {
            _uutPowerButton.Press();
            _uutTimeButton.Press();
            _door.Open();
            _output.Received().OutputLine("Display cleared");
        }

        [Test]
        public void SetTime_DoorOpenn_LightOn()
        {
            _uutPowerButton.Press();
            _uutTimeButton.Press();
            _door.Open();
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void Ready_PowerAndTime_CookerIsCalledCorrect()
        {
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutTimeButton.Press();
            _uutTimeButton.Press();
            _uutStartCancel.Press();
            _output.Received().OutputLine("Display shows: 02:00");
            _output.Received().OutputLine("Display shows: 200 W");

        }

        [Test]
        public void Ready_FullPower_CookerIsCalled()
        {
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();
            _uutPowerButton.Press();

            _uutTimeButton.Press();
            _uutStartCancel.Press();

            _output.Received().OutputLine("Display shows: 700 W");
            _output.Received().OutputLine("PowerTube works with 100 %");
        }

        [Test]
        public void SetTime_StartButton_LightIsCalled()
        {
            _uutPowerButton.Press();
            _uutTimeButton.Press();
            _uutStartCancel.Press();
            _output.Received().OutputLine("Light is turned on");
        }

    }
}
