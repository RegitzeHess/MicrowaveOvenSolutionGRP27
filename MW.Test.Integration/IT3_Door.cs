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
    class IT3_Door
    {

        private IDoor _uut;
        private IOutput _output;
        private ILight _light;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startcancel;
        private ICookController _cookController;
        private IUserInterface _userInterface;

        [SetUp]
        public void SetUP()
        {
            _output = Substitute.For<IOutput>();
            _uut = new Door();
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _powerButton = new Button();
            _timeButton = new Button();
            _startcancel = new Button();
            _cookController = new CookController(_timer, _display, _powerTube) {UI = _userInterface};
            _userInterface = new UserInterface(_powerButton, _timeButton, _startcancel, _uut, _display, _light, _cookController);

        }

        [Test]
        public void DoorOpen_IsValid()
        {
            _uut.Open();
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void DoorOpen_ThenClosed_IsValid()
        {
            _uut.Open();
            _uut.Close();
            _output.Received().OutputLine("Light is turned off");
        }
    }
}
