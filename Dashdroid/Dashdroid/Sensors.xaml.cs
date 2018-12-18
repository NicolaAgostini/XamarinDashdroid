using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Collections.ObjectModel;


using Xamarin.Forms;

namespace Dashdroid
{
    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }

    public partial class Sensors : ContentPage
    {
         SensorSpeed speed = SensorSpeed.UI;
        ObservableCollection<SensorDef> SensorDatas = new ObservableCollection<SensorDef>();

        public Sensors()
        {
            InitializeComponent();
            SersorView.ItemsSource = SensorDatas;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            SensorDatas.Add(new SensorDef { DisplayValue = "Accelerometer" });
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
         {
        var data = e.Reading;
            // Console.WriteLine($"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}");
            SensorDatas.Add(new SensorDef { DisplayValue = "X: " + data.Acceleration.X.ToString().Truncate(3) +
             " Y: " + data.Acceleration.Y.ToString().Truncate(3) + 
                " Z: " + data.Acceleration.Z.ToString().Truncate(3)
            });

            SensorDatas.Add(new SensorDef { DisplayValue = " " });

            //Accelerometer_value.Text = "X: "+ data.Acceleration.X + " Y: " + data.Acceleration.Y + " Z: " + data.Acceleration.Z;
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
