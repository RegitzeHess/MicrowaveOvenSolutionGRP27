using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NSubstitute;
using System.Threading;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace MW.Test.Integration
{
    public class IT2_UserInterface
    {
        private IOutput _output;
        private ILight _light;
        private IDisplay _display;
        private IPowerTube _powertube;
        private IDoor _door;
        private ITimer _timer;
        private IButton _powerbutton;
        private IButton _timebutton;
        private IButton _startcancel;
        private CookController _cookcontroller;
        private UserInterface _driver;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
            _display = new Display(_output);
            _powertube = new PowerTube(_output);
            _timer = new Timer();

            _door = Substitute.For<IDoor>();
            _powerbutton = Substitute.For<IButton>();
            _timebutton = Substitute.For<IButton>();
            _startcancel = Substitute.For<IButton>();

            _cookcontroller = new CookController(_timer, _display, _powertube);
            _driver = new UserInterface(_powerbutton, _timebutton, _startcancel, _door, _display, _light, _cookcontroller);
            _cookcontroller.UI = _driver;
        }
        
        //1
        [Test]
        public void Ready_DoorOpen_LightOn()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty); //vi raiser et event, da vi ikke kan kalde metoden på vores fake. Magen til metoden i unittesten
            _output.Received().OutputLine("Light is turned on");
            
        }
        //2
        [Test]
        public void DoorOpen_DoorClose_LightOff()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned off");
        }
        //3
        [Test]
        public void Ready_PowerButtonIsPressed_PowerIs50()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Display shows: 50 W");
        }
        //4
        [Test]
        public void Ready_2PowerButtonIsPressed_PowerIs100()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Display shows: 100 W");
        }
        //5
        [Test]
        public void Ready_15PowerButton_PowerIs50Again()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Display shows: 50 W");
        }

       
        //6
        [Test]
        public void SetPower_CancelButton_DisplayCleared()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Display cleared");

        }

        //7
        [Test]
        public void SetPower_DoorOpened_DisplayCleared()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Display cleared");
        }

        //8
        [Test]
        public void SetPower_DoorOpened_LightOn()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
        }

        //9
        [Test]
        public void SetPower_TimeButton_TimeIs1()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Display shows: 01:00");
        }
        //10
        [Test]
        public void SetPower_2TimeButton_TimeIs2()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Display shows: 02:00");
        }

       
        //11
        [Test]
        public void SetTime_StartButton_CookerIsCalled()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            Thread.Sleep(61000); 
            _output.Received().OutputLine("PowerTube turned off");

        }

        //12
        [Test]
        public void SetTime_DoorOpened_DisplayCleared()
        {

            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Display cleared");

        }

        //13
        [Test]
        public void SetTime_DoorOpened_LightOn()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");

        }
        
        //14
        [Test]
        public void Ready_PowerAndTime_CookerIsCalledCorrectly()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            Thread.Sleep(100);
            _output.Received().OutputLine("PowerTube works with 14,2857142857143 %"); 
        }

        //15
        [Test]
        public void Ready_FullPower_CookerIsCalledCorrectly()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            Thread.Sleep(100);
            _output.Received().OutputLine("PowerTube works with 100 %");


        }

        //16
        [Test]
        public void SetTime_StartButton_LightIsCalled()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
        }

        //17 fejler
        [Test]
        public void Cooking_CookingIsDone_LightOff()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            Thread.Sleep(61000);
            _output.Received().OutputLine("Light is turned off");

        }

        //18
        [Test]
        public void Cooking_CookingIsDone_DisplayIsCleared()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            Thread.Sleep(60300);
            _output.Received().OutputLine("Display cleared");
        }

        //19
        [Test]
        public void Cooking_DoorIsOpened_CookerCalled()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");

        }

        //20
        [Test]
        public void Cooking_CancelButton_CookerCalled()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned off");
        }

        //21
        [Test]
        public void Cooking_CancelButton_LightCalled()
        {
            _powerbutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timebutton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startcancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            Thread.Sleep(500);
            _output.Received().OutputLine("Light is turned off");
            
        }

      

    }
}
