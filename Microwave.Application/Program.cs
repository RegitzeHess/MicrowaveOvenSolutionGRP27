using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Boundary;
using Timer = MicrowaveOvenClasses.Boundary.Timer;


namespace Microwave.Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            //IT4_Output

            //Vi har udkommenteret nogle af testene, da de tager lang tid at køre

            var _output = new Output();
            var _light = new Light(_output);
            var _display = new Display(_output);
            var _powertube = new PowerTube(_output);
            var _door = new Door();
            var _timer = new Timer();
            var _powerbutton = new Button();
            var _timebutton = new Button();
            var _startcancel = new Button();
            var _cookcontroller = new CookController(_timer, _display, _powertube);
            var _userinterface = new UserInterface(_powerbutton , _timebutton, _startcancel, _door, _display, _light, _cookcontroller);
            _cookcontroller.UI = _userinterface;

            ManualResetEvent pause = new ManualResetEvent(false);

            //1
            Console.WriteLine("Ready_DoorOpen_LightOn");
            _door.Open();
            Console.WriteLine();

            //2
            Console.WriteLine("Ready_DoorOpen_LightOff");
            _door.Open();
            _door.Close();
            Console.WriteLine();

            //3
            Console.WriteLine("Ready_PowerButtonIsPressed_PowerIs50");
            _powerbutton.Press();
            Console.WriteLine();

            //nulstiller power
            _startcancel.Press();

            //4
            Console.WriteLine("Ready_2PowerButtonIsPressed_PowerIs100");
            _powerbutton.Press();
            _powerbutton.Press();
            Console.WriteLine();

            //nulstiller power
            _startcancel.Press();

            //5
            Console.WriteLine("Ready_15PowerButtonIsPressed_PowerIs50Again");
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
            Console.WriteLine();

            //nulstiller power
            _startcancel.Press();

            //6
            Console.WriteLine("SetPower_CancelButton_DisplayCleared");
            _powerbutton.Press();
            _startcancel.Press();
            Console.WriteLine();

            //7
            Console.WriteLine("SetPower_DoorOpened_DisplayCleared");
            _powerbutton.Press();
            _door.Open();
            Console.WriteLine();

            //nulstiller dør og power
            _door.Close();
            _startcancel.Press();

            //8
            Console.WriteLine("SetPower_DoorOpened_LightOn");
            _powerbutton.Press();
            _door.Open();
            Console.WriteLine();

            //nulstiller dør og power
            _door.Close();
            _startcancel.Press();
            Console.WriteLine();

            //9
            Console.WriteLine("SetPower_TimeButton_TimeIs1");
            _powerbutton.Press();
            _timebutton.Press();
            Console.WriteLine();

            //nulstiller 
           _startcancel.Press();
            _startcancel.Press();
            Console.WriteLine();

            //10
            Console.WriteLine("SetPower_2TimeButton_TimeIs2");
            _powerbutton.Press();
            _timebutton.Press();
            _timebutton.Press();
            Console.WriteLine();

            //nulstiller 
            _startcancel.Press();
            _startcancel.Press();
            Console.WriteLine();

            //11
            //Console.WriteLine("SetTime_StartButton_CookerIsCalled");
            //_powerbutton.Press();
            //_timebutton.Press();
            //_startcancel.Press();
            //pause.WaitOne(60100);
            //Console.WriteLine();

            //12
            Console.WriteLine("SetTime_DoorOpened_DisplayCleared");
            _powerbutton.Press();
            _timebutton.Press();
            _door.Open();
            Console.WriteLine();

            //nulstiller dør og power
            _door.Close();
            _startcancel.Press();
            Console.WriteLine();

            //13
            Console.WriteLine("SetTime_DoorOpened_LightOn");
            _powerbutton.Press();
            _timebutton.Press();
            _door.Open();
            Console.WriteLine();

            //nulstiller dør og power
            _door.Close();
            _startcancel.Press();
            Console.WriteLine();

            ////14
            //Console.WriteLine("Ready_PowerAndTime_CookerIsCalledCorectly");
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_timebutton.Press();
            //_timebutton.Press();
            //_startcancel.Press();
            //pause.WaitOne(100);
            //Console.WriteLine();

            ////nulstiller
            //pause.WaitOne(121000);

            ////15
            //Console.WriteLine("Ready_FullPower_CookerIsCalledCorectly");
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_powerbutton.Press();
            //_timebutton.Press();
            //_startcancel.Press();
            //pause.WaitOne(100);
            //Console.WriteLine();

            ////17
            //Console.WriteLine("Cooking_CookingIsDone_LightOff");
            //pause.WaitOne(60100);
            
            //16
            Console.WriteLine("SetTime_StartButton_LightIsCalled");
            _powerbutton.Press();
            _timebutton.Press();
            _startcancel.Press();
            Console.WriteLine();

            ////18
            //Console.WriteLine("Cooking_CookingIsDone_DisplayCleared");
            //_powerbutton.Press();
            //_timebutton.Press();
            //_startcancel.Press();
            //pause.WaitOne(60300);
            //Console.WriteLine();

            
            //19
            Console.WriteLine("Cooking_DoorIsOpen_CookerCalled");
            _powerbutton.Press();
            _timebutton.Press();
            _startcancel.Press();
            _door.Open();
            Console.WriteLine();

            //nulstille dør
            _door.Close();

            //20
            Console.WriteLine("Cooking_cancelButton_CookerCalled");
            _powerbutton.Press();
            _timebutton.Press();
            _startcancel.Press();
            _startcancel.Press();
            Console.WriteLine();

            //21
            Console.WriteLine("Cooking_CancelBotton_lightCalled");
            _powerbutton.Press();
            _timebutton.Press();
            _startcancel.Press();
            _startcancel.Press();
            Console.WriteLine();
        }
    }
}

