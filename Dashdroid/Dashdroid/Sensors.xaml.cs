using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

using Xamarin.Forms;

namespace Dashdroid
{
    public static class StringExt //classe per troncare le stringhe (i valori dell'accelerometro troppo lungnhi)
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

        public Sensors()
        {
            InitializeComponent();

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged; //Registrazione all'evento accelerometro


            Battery.BatteryInfoChanged += Battery_BatteryChanged; //Registrazione evento batteria
            var level = Battery.ChargeLevel;
      
            var source = Battery.PowerSource;

            Battery_value.Text = "Battery level: " + level*100 + 
            " Battery Power Source: " + source;

            RetrievePosition();

            Gyroscope.ReadingChanged += Gyroscope_ReadingChanged; //Registrazione evento giroscopio

            Magnetometer.ReadingChanged += Magnetometer_ReadingChanged; // Registrazione all'evento magnetometro

        }



       async void RetrievePosition()
        {

            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status == PermissionStatus.Granted)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    GPS_value.Text = "Latitude: " + location.Latitude + Environment.NewLine
                    + "Longitude: " + location.Longitude + Environment.NewLine
                         + " Altitude: " + location.Altitude;
                }
            }

           
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e) //gestisco l'evento accelerometro
         {
        var data = e.Reading;
            // Console.WriteLine($"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}");
          

           

            Accelerometer_value.Text = "X: "+ data.Acceleration.X.ToString().Truncate(4) + 
            " Y: " + data.Acceleration.Y.ToString().Truncate(4) +
                 " Z: " + data.Acceleration.Z.ToString().Truncate(4);
            // Process Acceleration X, Y, and Z
        }


        void Battery_BatteryChanged(object sender, BatteryInfoChangedEventArgs e)  //gestisco l'evento della batteria
        {
            var level = e.ChargeLevel;
            level = Convert.ToInt32(level*100);
            // Console.WriteLine("Ecco level:"+level);
            var state = e.State;
            var source = e.PowerSource;
            //Console.WriteLine($"Reading: Level: {level}, State: {state}, Source: {source}");
            Battery_value.Text = "Battery level: "+ level +Environment.NewLine + "Battery Power Source: " + source;

        }

        void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)  //gestisco l'evento giroscopio
        {
            var data = e.Reading;
            // Process Angular Velocity X, Y, and Z reported in rad/s
            //Console.WriteLine($"Reading: X: {data.AngularVelocity.X}, Y: {data.AngularVelocity.Y}, Z: {data.AngularVelocity.Z}");

            Gyroscope_value.Text = "X: " + data.AngularVelocity.X.ToString().Truncate(5) +
             " Y: "+ data.AngularVelocity.Y.ToString().Truncate(5) +
                 " Z: "+ data.AngularVelocity.Y.ToString().Truncate(5);
        }

        void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
        {
            var data = e.Reading;
            // Process MagneticField X, Y, and Z
            //Console.WriteLine($"Reading: X: {data.MagneticField.X}, Y: {data.MagneticField.Y}, Z: {data.MagneticField.Z}");
            Magnetometer_value.Text = "X: " + data.MagneticField.X.ToString().Truncate(2) +
             " Y: " + data.MagneticField.Y.ToString().Truncate(2) +
                 " Z: " + data.MagneticField.Y.ToString().Truncate(2);
        }




        private void ToggleAccelerometer_OnToggled(object sender, ToggledEventArgs e) //non è detto mi serva magari lo metto nella pagina settings
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
                DisplayAlert("Alert", "Accelerometer not supported", "OK");
                Console.WriteLine("Error!"+fnsEx);
            }
        catch (Exception ex)
        {
                DisplayAlert("Alert", "An error has occurred using accelerator", "OK");
                Console.WriteLine("Error!" + ex);
            }
    }

        public void ToggleGyroscope_OnToggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (Gyroscope.IsMonitoring)
                    Gyroscope.Stop();
                else
                    Gyroscope.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                DisplayAlert("Alert", "Gyroscope not supported", "OK");
                Console.WriteLine("Error!" + fnsEx);
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "An error has occurred using gyroscope", "OK");
                Console.WriteLine("Error!" + ex);
            }
        }

        public void ToggleMagnetometer_OnToggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (Magnetometer.IsMonitoring)
                    Magnetometer.Stop();
                else
                    Magnetometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                DisplayAlert("Alert", "Magnetometer not supported", "OK");
                Console.WriteLine("Error!" + fnsEx);
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "An error has occurred using magnetometer", "OK");
                Console.WriteLine("Error!" + ex);
            }
        }
    }
}
