using PhotoSave.Commands;
using PhotoSave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PhotoSave.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private ImageSource imageSource;

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; OnPropertyChanged(); }
        }


        public RelayCommand NextCommand { get; set; }
        public RelayCommand PrevCommand { get; set; }
        public RelayCommand ScreenShotCommand { get; set; }


        public AppViewModel()
        {

            Originator originator = new Originator(ImageSource);
            Taker Taker = new Taker(originator);
            ScreenShotCommand = new RelayCommand(c =>
            {
                var result = ScreenCapture.CaptureActiveWindow();
                var imageSource = BitmapConverter.ImageSourceFromBitmap(result);
                ImageSource = imageSource;
                originator.TakeScreenShot(ImageSource);
                Taker.Backup();
            });

            PrevCommand = new RelayCommand(c =>
            {
                var imageSource = Taker.Undo();
                if (imageSource != null)
                    ImageSource = imageSource;
            });


            NextCommand = new RelayCommand(c =>
            {
                var imageSource = Taker.Redo();
                if (imageSource != null)
                    ImageSource = imageSource;
            });
        }
    }
}
