using System;
using System.Collections.Generic;
using Xamarin.Essentials;


using Xamarin.Forms;

namespace Dashdroid
{
    public partial class Sensors : ContentPage
    {
         SensorSpeed speed = SensorSpeed.UI;

        public Sensors()
        {
            InitializeComponent();
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
         {
        var data = e.Reading;
        Console.WriteLine($"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}");
            Accelerometer_value.Text = "X: "+ data.Acceleration.X + " Y: " + data.Acceleration.Y + " Z: " + data.Acceleration.Z;
        // Process Acceleration X, Y, and Z
         }
private void ToggleAccelerometer_OnToggled(object sender, ToggledEventArgs e)
    {
        try
        {
            if (Accelerometer.IsMonitoring)
              Accelerometer.Stop();
            else
              Accelerometer.Start(speed);
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            // Feature not supported on device
        }
        catch (Exception ex)
        {
            // Other error has occurred.
        }
    }



    }
}
