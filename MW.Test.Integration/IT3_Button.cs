using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
        public void 
    }
}
