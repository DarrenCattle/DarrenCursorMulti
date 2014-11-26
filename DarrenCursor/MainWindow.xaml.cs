using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace DarrenCursor
{
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Toolkit;
    using Microsoft.Kinect.Toolkit.Controls;
    using Microsoft.Kinect.Toolkit.Interaction;
    using System.IO.Ports;
    using System.Threading.Tasks;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensorChooser sensorChooser;
        DispatcherTimer timer = new DispatcherTimer();
        DateTime[] start = new DateTime[9];
        SerialPort arduino = new SerialPort();
        char[] state = new char[9];
        double[] millis = new double[9];
        double[] hold = new double[9];
        double[] charge = new double[9];
        String[] statemap = {"Holding", "Charging", "Holding", "Discharging", "Holding"};

        public MainWindow()
        {
            //timer initialization
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();

            //serial initialization
            arduino.PortName = "COM3";
            arduino.BaudRate = 9600;
            arduino.Open();

            //screen sizing

            //kinect
            InitializeComponent();
            Loaded += OnLoaded;

            //looping
            int panels = 9;
            for (int x = 0; x<panels; x++)
            {
                start[x] = DateTime.Now;
                state[x] = '0';
                millis[x] = hold[x] = charge[x] = 0;
            }
        }
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();
        }
        private void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            bool error = false;
            if (args.OldSensor != null) {
                try {
                    args.OldSensor.DepthStream.Range = DepthRange.Default;
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException) {error = true;}
            }
            if (args.NewSensor != null) {
                try {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();
                    try {
                        args.NewSensor.DepthStream.Range = DepthRange.Near;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                        args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                    }
                    catch (InvalidOperationException) {
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                        error = true;
                    }
                }
          catch (InvalidOperationException) {error = true;}
          }
          if (!error)
            kinectRegion.KinectSensor = args.NewSensor;
      }

      private void Panel_Click(object sender, RoutedEventArgs e)
      {
          var button = sender as KinectTileButton;
          int p = Convert.ToInt32(button.Tag);
          switch (state[p])
          {
              case '0': //0: no charge, not charging
                  state[p] = '1';
                  start[p] = DateTime.Now;
                  break;
              case '1': //1: partially charged, charging
                  state[p] = '2';
                  hold[p] += millis[p];
                  break;
              case '2': //2: partially charged, holding
                  state[p] = hold[p] >= 120000 ? '1' : '3';
                  start[p] = DateTime.Now;
                  break;
              case '3': //3: partially charged, discharging
                  state[p] = '2';
                  hold[p] -= millis[p];
                  millis[p] = 0;
                  break;
              case '4': //4: fully charged, not charging
                  state[p] = '3';
                  start[p] = DateTime.Now;
                  break;
          }
          if (arduino.IsOpen && p==4) {char[] fourth = {state[4]}; arduino.Write(fourth, 0, 1);}
      }

      private void timer_Tick(object sender, EventArgs e)
      {
          for (int x = 0; x < 9; x++)
          {
            millis[x] = DateTime.Now.Subtract(start[x]).TotalMilliseconds;
            switch (state[x])
            {
                case '0': //0: no charge, not charging
                    hold[x] = 0;
                    charge[x] = 0;
                    if (arduino.IsOpen) { char[] fourth = { state[4] }; arduino.Write(fourth, 0, 1); }
                    break;
                case '1': //1: partially charged, charging
                    state[x] = (millis[x] + hold[x]) >= 240000 ? '4' : state[x];
                    charge[x] = hold[x] + millis[x];
                    break;
                case '2': //2: partially charged, holding
                    charge[x] = hold[x];
                    if (arduino.IsOpen) { char[] fourth = { state[4] }; arduino.Write(fourth, 0, 1); }
                    break;
                case '3': //3: partially charged, discharging
                    charge[x] = hold[x] - millis[x];
                    state[x] = charge[x] <= 0 ? '0' : state[x];
                    break;
                case '4': //4: fully charged, not charging
                    hold[x] = 240000;
                    charge[x] = 240000;
                    if (arduino.IsOpen) { char[] fourth = { state[4] }; arduino.Write(fourth, 0, 1); }
                    break;
            }
          }
          int stat = Convert.ToInt32(state[4])-48;
          StatusBox.Text = statemap[stat];
          Panel0.Opacity = charge[0] / 240000 + 0.2 >= 1 ? 1 : charge[0] / 240000 + 0.2;
          Panel0.Label = "Tint: " + Math.Round(charge[0] / 2400, 1) + "%, T_surf: " + Math.Round(27 - charge[0] / 60000, 1) +
                "°C, I_tot: " + Math.Round(1000 - 1000 * charge[0] / 240000, 1) + "W/m^2";

          Panel1.Opacity = charge[1] / 240000 + 0.2 >= 1 ? 1 : charge[1] / 240000 + 0.2;
          Panel1.Label = "Tint: " + Math.Round(charge[1] / 2400, 1) + "%, T_surf: " + Math.Round(27 - charge[1] / 60000, 1) +
                "°C, I_tot: " + Math.Round(1000 - 1000 * charge[1] / 240000, 1) + "W/m^2";

          Panel2.Opacity = charge[2] / 240000 + 0.2 >= 1 ? 1 : charge[2] / 240000 + 0.2;
          Panel2.Label = "Tint: " + Math.Round(charge[2] / 2400, 1) + "%, T_surf: " + Math.Round(27 - charge[2] / 60000, 1) +
              "°C, I_tot: " + Math.Round(1000 - 1000 * charge[2] / 240000, 1) + "W/m^2";

          Panel3.Opacity = charge[3] / 240000 + 0.2 >= 1 ? 1 : charge[3] / 240000 + 0.2;
          Panel3.Label = "Tint: " + Math.Round(charge[3] / 2400, 1) + "%, T_surf: " + Math.Round(27 - charge[3] / 60000, 1) +
              "°C, I_tot: " + Math.Round(1000 - 1000 * charge[3] / 240000, 1) + "W/m^2";

          Panel4.Opacity = charge[4] / 240000 + 0.2 >= 1 ? 1 : charge[4] / 240000 + 0.2;
          Panel4.Label = "Tint: " + Math.Round(charge[4] / 2400, 1) + "%, T_surf: " + Math.Round(27 - charge[4] / 60000, 1) +
              "°C, I_tot: " + Math.Round(1000 - 1000 * charge[4] / 240000, 1) + "W/m^2";

          Panel5.Opacity = charge[5] / 240000 + 0.2 >= 1 ? 1 : charge[5] / 240000 + 0.2;
          Panel5.Label = "Tint: " + Math.Round(charge[5] / 2400, 1) + "%, T_surf: " + Math.Round(27 - charge[5] / 60000, 1) +
              "°C, I_tot: " + Math.Round(1000 - 1000 * charge[5] / 240000, 1) + "W/m^2";

          Panel6.Opacity = charge[6] / 240000 + 0.2 >= 1 ? 1 : charge[6] / 240000 + 0.2;
          Panel6.Label = "Tint: " + Math.Round(charge[6] / 2400, 1) + "%, T_surf: " + Math.Round(27 - charge[6] / 60000, 1) +
              "°C, I_tot: " + Math.Round(1000 - 1000 * charge[6] / 240000, 1) + "W/m^2";

          Panel7.Opacity = charge[7] / 240000 + 0.2 >= 1 ? 1 : charge[7] / 240000 + 0.2;
          Panel7.Label = "Tint: " + Math.Round(charge[7] / 2400, 1) + "%, T_surf: " + Math.Round(27 - charge[7] / 60000, 1) +
              "°C, I_tot: " + Math.Round(1000 - 1000 * charge[7] / 240000, 1) + "W/m^2";

          Panel8.Opacity = charge[8] / 240000 + 0.2 >= 1 ? 1 : charge[8] / 240000 + 0.2;
          Panel8.Label = "Tint: " + Math.Round(charge[8] / 2400, 1) + "%, T_surf: " + Math.Round(27 - charge[8] / 60000, 1) +
              "°C, I_tot: " + Math.Round(1000 - 1000 * charge[8] / 240000, 1) + "W/m^2";
      }

      private void Window_Closed(object sender, EventArgs e)
      {
          if (arduino.IsOpen)
          {
              arduino.Write(new char[] { '0' }, 0, 1);
              arduino.Close();
          }
      }
   }//public MainWindow
}//namespace DarrenCursor