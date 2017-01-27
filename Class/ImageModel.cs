using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using XamlStandardsLib;
using Windows.UI.Xaml.Media.Imaging;

namespace HyperFileChanger.Class
{
    public class ImageModel : ModelBase
    {
        int height;
        public int Height
        {
            get { return height; }
            set
            {
                if(height != value)
                {
                    height = value;
                    this.OnPropertyChanged();
                }
            }
        }
        int length;
        public int Length
        {
            get { return length; }
            set
            {
                if(length != value)
                {
                    length = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
