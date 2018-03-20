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

        private CookController _uut;


        [SetUp]
        public void SetUp()
        {
         
            
            _output = Substitute.For<IOutput>();

            _timer = new Timer();
            _display = new Display(_output);
            _powertube = new PowerTube(_output);

            _userInterface = Substitute.For<IUserInterface>();
            _uut = new CookController(_timer, _display, _powertube) {UI = _userInterface};
        }
       
        //test om cooking starter med valide værdier
        [Test]
        public void StartCooking_100power_2000time_IsValid()
        {
            _uut.StartCooking(100, 2000);

            _output.Received().OutputLine(Arg.Is<string>(_string => _string.ToLower().Contains("works") && _string.ToLower().Contains("100")));
        }
        //HVORFOR WORKS OG 100?

        //for lav, for høj eller ugyldig power

        [TestCase(0, 2000)]
        [TestCase(800, 2000)]
        [TestCase(110, 2000)]

        public void StartCooking_TooHighOrTooLowOrInvalidPower(int power, int time)
        {
            Assert.That(() => _uut.StartCooking(power, time), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        
        //efter et tick skal output være lig tid på display
        [Test]
        public void Cooking_AfterOneTick_DisplayShowsTheNewTime()
        {
           _uut.StartCooking(100, 2500);
            Thread.Sleep(2003); 
            _output.Received().OutputLine(Arg.Is<string>(_string => _string.ToLower().Contains("00:01")));
            _output.Received().OutputLine(Arg.Is<string>(_string => _string.Contains("00:00")));
            Thread.Sleep(500);
            _output.Received().OutputLine("PowerTube turned off");
        }

        //når tid udløber skal power slukkes
        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            _uut.StartCooking(100, 2000);
            Thread.Sleep(2100);
            _output.Received().OutputLine("PowerTube turned off"); //HVOR LÆSES HVAD DER UDSKRIVES I OUTPUT?
        }


        //når tiden er udløbet skal det udskrives via UI
        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            _uut.StartCooking(100, 2000);
            Thread.Sleep(2005);
            _userInterface.Received().CookingIsDone();
        }



        //når stop bliver kaldt på en igangværende cooking skal display vise det
        [Test]
        public void Cooking_Stop_DisplayShowsThatPowerTubeIsStoped()
        {
            _uut.StartCooking(100,2000);
            Thread.Sleep(2001);
            _uut.Stop();
            _output.Received().OutputLine("PowerTube turned off"); //HVOR LÆSES HVAD DER UDSKRIVES I OUTPUT?
        }

        //JEG VED IKKE OM VI SKAL LAVE DEM HER - MAIKEN HAR MEN DE ER IKKE I UNITTEST MEN DET ER LOGGER STÅR
        //OVERALT I SEKVENSDIAGRAMMET

        //mikroovn bruges allerede

        //for hvert tick bliver loggeren kaldt
    }
}
