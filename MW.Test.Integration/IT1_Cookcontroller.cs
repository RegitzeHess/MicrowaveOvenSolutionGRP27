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
using System.Threading;
using Timer = MicrowaveOvenClasses.Boundary.Timer;


namespace MW.Test.Integration
{
    [TestFixture]
    public class IT1_Cookcontroller
    {
        private ITimer _timer;

        private IDisplay _display;
        private IPowerTube _powertube;
        private IUserInterface _userInterface;
        private IOutput _output;

        private CookController _driver;


        [SetUp]
        public void SetUp()
        {
         
            
            _output = Substitute.For<IOutput>();

            _timer = new Timer();
            _display = new Display(_output);
            _powertube = new PowerTube(_output);

            _userInterface = Substitute.For<IUserInterface>();
            _driver = new CookController(_timer, _display, _powertube) {UI = _userInterface};
        }
       
        //test om cooking starter med valide værdier
        [Test]
        public void StartCooking_100power_2time_IsValid()
        {
            _driver.StartCooking(100, 2);

            _output.Received().OutputLine(Arg.Is<string>(_string => _string.ToLower().Contains("works") && _string.ToLower().Contains("2")));
        }
       

        //for lav eller for høj  power

        [TestCase(0, 2)]
        [TestCase(800, 2)]
      

        public void StartCooking_TooHighOrTooLowOrInvalidPower(int power, int time)
        {
            Assert.That(() => _driver.StartCooking(power, time), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        
        //efter et tick skal output være lig tid på display
        [Test]
        public void Cooking_AfterOneTick_DisplayShowsTheNewTime()
        {
            //DENNE TEST FEJLEDE, DA DER VAR FEJL I COOKCONTROLLER OG TIMERS FORSTÅELSE AF TID. ÆNDREDE I CONTROLLEREN
           _driver.StartCooking(100, 2);
            Thread.Sleep(2000); 
            _output.Received().OutputLine(Arg.Is<string>(_string => _string.ToLower().Contains("00:01")));
            _output.Received().OutputLine(Arg.Is<string>(_string => _string.Contains("00:00")));
            Thread.Sleep(500);
            _output.Received().OutputLine("PowerTube turned off");
        }

        //når tid udløber skal power slukkes
        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            _driver.StartCooking(100, 2);
            Thread.Sleep(2100);
            _output.Received().OutputLine("PowerTube turned off"); 
        }


        //når tiden er udløbet skal det udskrives via UI
        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            _driver.StartCooking(100, 2);
            Thread.Sleep(2100);
            _userInterface.Received().CookingIsDone();
        }



        //når stop bliver kaldt på en igangværende cooking skal display vise det
        [Test]
        public void Cooking_Stop_DisplayShowsThatPowerTubeIsStoped()
        {
            _driver.StartCooking(100,2);
            Thread.Sleep(2001);
            _driver.Stop();
            _output.Received().OutputLine("PowerTube turned off"); 
        }

      
    }
}
