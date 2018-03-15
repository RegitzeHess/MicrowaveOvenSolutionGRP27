using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using NUnit;
using NSubstitute;
using NUnit.Framework;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;

namespace MW.Test.Integration
{
    [TestFixture]
    public class IT1_CookcontrollerTimer
    {
        private ITimer _timer;

        private IDisplay _display;
        private IPowerTube _powertube;
        private IUserInterface _userInterface;
        private IOutput _output;

        private CookController _cookController;


        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();

            _display = Substitute.For<Display>(_output);
            _powertube = Substitute.For<PowerTube>(_output);
            _userInterface = Substitute.For<UserInterface>();
            _output = Substitute.For<Output>();

            _cookController = new CookController(_timer, _display, _powertube, _userInterface);
        }


        [Test]
        public void StartCooking_ValidParameters_TimerStarted()
        {
            _cookController.StartCooking(50, 60);

            _timer.Received().Start(60);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            _cookController.StartCooking(50, 60);

            _powertube.Received().TurnOn(50);
        }

        [Test]
        public void Cooking_TimerTick_DisplayCalled()
        {
            _cookController.StartCooking(50, 60);

            _timer.TimeRemaining.Returns(115);
            _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            _display.Received().ShowTime(1, 55);
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            _cookController.StartCooking(50, 60);

            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            _powertube.Received().TurnOff();
        }

        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            _cookController.StartCooking(50, 60);

            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            _userInterface.Received().CookingIsDone();
        }

        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            _cookController.StartCooking(50, 60);
            _cookController.Stop();

            _powertube.Received().TurnOff();
        }




    }
}
