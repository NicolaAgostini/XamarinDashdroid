using System;
using System.Collections.Generic;
using Plugin.Media;

using Xamarin.Forms;

namespace Dashdroid
{
    public partial class Camera : ContentPage
    {
        public Camera()
        {
            InitializeComponent();
            CameraButton.Clicked += CameraButton_Clicked;
        }
        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (photo != null)
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
        }


    }
}
