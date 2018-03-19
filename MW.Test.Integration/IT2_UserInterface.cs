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
        private ICookController _cookcontroller;
        private UserInterface _uut;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
            _display = new Display(_output);
            _powertube = new PowerTube(_output);
            _door = new Door();
            _timer = new Timer();
            
            _powerbutton = new Button();
            _timebutton = new Button();
            _startcancel = new Button();
            
            _cookcontroller = new CookController(_timer, _display, _powertube) { UI = _uut };
            _uut = new UserInterface(_powerbutton, _timebutton, _startcancel, _door, _display, _light, _cookcontroller);
        }
        
        //1
        [Test]
        public void Ready_DoorOpen_LightOn()
        {
            _door.Open();
            _output.Received().OutputLine("Light is turned on");
            
        }
        //2
        [Test]
        public void DoorOpen_DoorClose_LightOff()
        {
            _door.Close();
            _output.Received().OutputLine("Light is turned off");
        }
        //3
        [Test]
        public void Ready_PowerButtonIsPressed_PowerIs50()
        {
            _powerbutton.Press();
            _output.Received().OutputLine("Display shows: 50 W");
        }
        //4
        [Test]
        public void Ready_2PowerButtonIsPressed_PowerIs100()
        {
            _powerbutton.Press();
            _powerbutton.Press();
            _output.Received().OutputLine("Display shows: 100 W");
        }
        //5
        [Test]
        public void Ready_15PowerButton_PowerIs50Again()
        {
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _powerbutton.Press();
            _output.Received().OutputLine("Display shows: 50 W");
        }

        //I UNITTESTENE ER DER 6, 7, OG 8 FOR SETPOWER DER TESTER AT DISPLAY BLIVER CLEARED NÅR DØR ÅBNER ELLER 
        //CANCEL OG AT LYS TÆNDES NÅR DØR ÅBNER
        //6
        [Test]
        public void SetPower_CancelButton_DisplayCleared()
        {

        }

        //7
        [Test]
        public void SetPower_DoorOpened_DisplayCleared()
        {
            
        }

        //8
        [Test]
        public void SetPower_DoorOpened_LightOn()
        {
            
        }

        //9
        [Test]
        public void SetPower_TimeButton_TimeIs1()
        {
            _powerbutton.Press();
            _timebutton.Press();
            _output.Received().OutputLine("Display shows: 01:00");
        }
        //10
        [Test]
        public void SetPower_2TimeButton_TimeIs2()
        {
            _powerbutton.Press();
            _timebutton.Press();
            _timebutton.Press();
            _output.Received().OutputLine("Display shows: 02:00");
        }

       
        //11
        [Test]
        public void SetTime_StartButton_CookerIsCalled()
        {
            _powerbutton.Press();
            _timebutton.Press();
            _startcancel.Press();
            Thread.Sleep(60100); //HVOR KOMMER DET TAL FRA?
            _output.Received().OutputLine("PowerTube turned off");

        }

        //12
        [Test]
        public void SetTime_DoorOpened_DisplayCleared()
        {
            
        }

        //13
        [Test]
        public void SetTime_DoorOpened_LightOn()
        {
            
        }
        
        //14
        [Test]
        public void Ready_PowerAndTime_CookerIsCalledCorrectly()
        {
            _powerbutton.Press();
            _powerbutton.Press();
            _timebutton.Press();
            _timebutton.Press();
            _startcancel.Press();
            Thread.Sleep(100);
            _output.Received().OutputLine("PowerTube works with ? %"); //HVAD SKAL DER STÅ HER? METODEN REGNER IKKE OM I %?
        }

        //15
        [Test]
        public void Ready_FullPower_CookerIsCalledCorrectly()
        {
            
        }

        //16
        [Test]
        public void SetTime_StartButton_LightIsCalled()
        {
            _powerbutton.Press();
            _timebutton.Press();
            _startcancel.Press();
            _output.Received().OutputLine("Light is turned on");
        }

        //17
        [Test]
        public void Cooking_CookingIsDone_LightOff()
        {
            
        }

        //18
        [Test]
        public void Cooking_CookingIsDone_DisplayIsCleared()
        {
            _powerbutton.Press();
            _timebutton.Press();
            _startcancel.Press();
            Thread.Sleep(60300);
            _output.Received().OutputLine("Display cleared");
        }

        //19
        [Test]
        public void Cooking_DoorIsOpened_CookerCalled()
        {
            
        }

        //20
        [Test]
        public void Cooking_CancelButton_CookerCalled()
        {
            
        }

        //21
        [Test]
        public void Cooking_CancelButton_LightCalled()
        {
            _powerbutton.Press();
            _timebutton.Press();
            _startcancel.Press();
            Thread.Sleep(62000); //HVORDAN SÆTTES DET HER TAL?
            _output.Received().OutputLine("Light is turned off");
            
        }

        
        //MAIKEN HAR EN TEST HVOR HUN TESTER AT TIDEN GÅR NÅR COOKER ER I GANG?

    }
}
