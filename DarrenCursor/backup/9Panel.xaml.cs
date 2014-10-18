using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensorChooser sensorChooser;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
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
      int[] buttons = new int[9];
      private void ButtonOnClick(object sender, RoutedEventArgs e)
      {
      }
      private void ButtonOnClick0(object sender, RoutedEventArgs e)
      {
          buttons[0]++;
          if (buttons[0] > 3) buttons[0] = 0;
          Panel_0.Label = "Tint: " + buttons[0];
          Panel_0.Opacity = buttons[0] * 0.33;
      }
      private void ButtonOnClick1(object sender, RoutedEventArgs e)
      {
          buttons[1]++;
          if (buttons[1] > 3) buttons[1] = 0;
          Panel_1.Label = "Tint: " + buttons[1];
          Panel_1.Opacity = buttons[1] * 0.33;
      }
      private void ButtonOnClick2(object sender, RoutedEventArgs e)
      {
          buttons[2]++;
          if (buttons[2] > 3) buttons[2] = 0;
          Panel_2.Label = "Tint: " + buttons[2];
          Panel_2.Opacity = buttons[2] * 0.33;
      }
      private void ButtonOnClick3(object sender, RoutedEventArgs e)
      {
          buttons[3]++;
          if (buttons[3] > 3) buttons[3] = 0;
          Panel_3.Label = "Tint: " + buttons[3];
          Panel_3.Opacity = buttons[3] * 0.33;
      }
      private void ButtonOnClick4(object sender, RoutedEventArgs e)
      {
          buttons[4]++;
          if (buttons[4] > 3) buttons[4] = 0;
          Panel_4.Label = "Tint: " + buttons[4];
          Panel_4.Opacity = buttons[4] * 0.33;
      }
      private void ButtonOnClick5(object sender, RoutedEventArgs e)
      {
          buttons[5]++;
          if (buttons[5] > 3) buttons[5] = 0;
          Panel_5.Label = "Tint: " + buttons[5];
          Panel_5.Opacity = buttons[5] * 0.33;
      }
      private void ButtonOnClick6(object sender, RoutedEventArgs e)
      {
          buttons[6]++;
          if (buttons[6] > 3) buttons[6] = 0;
          Panel_6.Label = "Tint: " + buttons[6];
          Panel_6.Opacity = buttons[6] * 0.33;
      }
      private void ButtonOnClick7(object sender, RoutedEventArgs e)
      {
          buttons[7]++;
          if (buttons[7] > 3) buttons[7] = 0;
          Panel_7.Label = "Tint: " + buttons[7];
          Panel_7.Opacity = buttons[7] * 0.33;
      }
      private void ButtonOnClick8(object sender, RoutedEventArgs e)
      {
          buttons[8]++;
          if (buttons[8] > 3) buttons[8] = 0;
          Panel_8.Label = "Tint: " + buttons[8];
          Panel_8.Opacity = buttons[8] * 0.33;
      }
   }//public MainWindow
}//namespace DarrenCursor

