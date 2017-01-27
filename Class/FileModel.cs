using System;
using XamlStandardsLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.FileProperties;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;
using Windows.Storage.Streams;
using Windows.Data.Pdf;
using ImageMagick;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;

namespace HyperFileChanger.Class
{
    public class FileModel : ModelBase
    {

        /*
        public FileModel(string pfad)
        {
            this.Paths = pfad;
            
        }
        */
        string path;
        public string Paths
        {
            get { return path; }
            set
            {
                if (path != value)
                {
                    path = value;
                    this.OnPropertyChanged();
                }
            }
        } //Pfad der Datei
        string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;

                    this.OnPropertyChanged();
                    this.OnPropertyChanged("Exist");
                }
            }
        } // Name der Datei
        bool exist;
        public bool Exist
        {
            get { return exist; }
            set
            {
                if (exist != value)
                {
                    exist = value;
                    this.OnPropertyChanged();
                }
            }
        } //Gibt es die Datei
        string dateiTyp;
        public string DateiTyp // DateiTyp
        {
            get { return dateiTyp; }
            set
            {
                if (dateiTyp != value)
                {
                    dateiTyp = value;
                    this.OnPropertyChanged();
                }
            }
        }
        DateTime datecreated; // Datum
        public DateTime DateCreated
        {
            get { return datecreated; }
            set
            {
                if (datecreated != value)
                {
                    datecreated = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged("Time");
                }
            }
        }
        public string Time // Datum Tag/Monat/Jahr
        {
            get
            {

                return String.Format(DateCreated.Day.ToString() + "." + DateCreated.Month.ToString() + "." + DateCreated.Year.ToString());
            }
        }
        ulong größe;
        public ulong Größe //Größe der Datei
        {
            get { return größe; }
            set
            {
                if (größe != value)
                {
                    größe = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged("GrößenAngabe");
                }
            }
        }
        private enum Size : ulong
        {
            kbytes = 1000,
            mbytes = 1000000,
            gbytes = 1000000000,
            tbytes = 1000000000000

        }
        private string GetSize(out double groß)
        {

            if ((int)Size.kbytes > Größe)
            {
                groß = Größe;
                return "Bytes";
            }
            else if ((int)Size.mbytes > (Größe))
            {
                groß = Größe / (double)Size.kbytes;
                return "KB";
            }
            else if ((int)Size.gbytes > Größe)
            {
                groß = Größe / (double)Size.mbytes;
                return "MB";
            }
            else
            {
                groß = Größe / (double)Size.gbytes;
                return "GB";
            }

        }
        private double größenAngabe;
        public string GrößenAngabe
        {

            get
            {
                GetSize(out größenAngabe);
                return String.Format("{0:f} {1}", größenAngabe, GetSize(out größenAngabe));

            }
        }
        string content;
        public string Content // Inhalt in String
        {
            get { return content; }
            set
            {
                if (content != value)
                {
                    content = value;
                    this.OnPropertyChanged();
                }
            }
        }

        ImageModel comeon;       
        public ImageModel Comeon
        {
            get { return comeon; }
            set
            {
                if (comeon != value)
                {
                    comeon = value;
                    this.OnPropertyChanged();
                }
            }
        }
        ImageProperties image;
        public ImageProperties Imagei
        {
            get { return image; }
            set
            {
                if (image != value)
                {
                    image = value;
                    this.OnPropertyChanged();
                }
            }
        } // Bildinformationen
        DocumentProperties Document { get; set; } //Dokumentinformation
        MusicProperties Music { get; set; } //Musikinformation
        BitmapImage mapperImage;
        public BitmapImage MapperImage
        {
            get { return mapperImage; }
            set
            {
                if (mapperImage != value)
                {
                    mapperImage = value;
                    this.OnPropertyChanged();
                }
            }
        }//Gezeigtes Bild
        PdfDocument pdf;
        public PdfDocument PDF
        {
            get { return pdf; }
            set
            {
                if (pdf != value)
                {
                    pdf = value;
                    this.OnPropertyChanged();
                }
            }
        } //PDF-Dokument Ablage
        StorageFile Files { get; set; } // Ausgewählte Datei aus dem Dateipicker
        FileInfo FileInformation { get; set; } // Weitere Informationen zur Datei
        MagickImage imageManipulation;
        public MagickImage ImageManipulation
        {
            get { return imageManipulation; }
            set
            {
                if (imageManipulation != value)
                {
                    imageManipulation = value;
                    this.OnPropertyChanged();
                }
            }
        }
        MagickImageInfo imageInfo;
        public MagickImageInfo ImageInfo
        {
            get { return imageInfo; }
            set
            {
                if(imageInfo != value)
                {
                    imageInfo = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public IRandomAccessStream fileStream { get; set; }
        

        public async Task PickFile()
        {

            Comeon = new ImageModel();
            FileOpenPicker picker = new FileOpenPicker(); //Datei auswählen
            picker.FileTypeFilter.Add("*"); //Muss dabei sein
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            
            try
            {
                
                Files = await picker.PickSingleFileAsync();

                Paths = Files.Path;

                var basisEigenschaften = await Files.GetBasicPropertiesAsync();

                Name = Files.DisplayName;
                DateiTyp = Files.DisplayType;
                DateCreated = Files.DateCreated.DateTime;

                Größe = basisEigenschaften.Size;
               
                if (DateiTyp == "JPG-Datei" || DateiTyp == "PNG-Datei" || DateiTyp == "GIF-Datei")
                {
                    
                    using ( fileStream = await Files.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {
                        
                        // Set the image source to the selected bitmap 
                        MapperImage = new BitmapImage();
                        //MapperImage.DecodePixelWidth = 1088; //match the target Image.Width
                        await MapperImage.SetSourceAsync(fileStream);
                        Imagei = await Files.Properties.GetImagePropertiesAsync();

                    }
                }
                else if (DateiTyp == "Adobe Acrobat Document")
                {
                    using (IRandomAccessStream fileStream = await Files.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {

                        PDF = await PdfDocument.LoadFromStreamAsync(fileStream);
                        


                    }
                }
                else if(DateiTyp == "Textdokument")
                {
                    using (IRandomAccessStream fileStream = await Files.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {
                        Document = await Files.Properties.GetDocumentPropertiesAsync();
                        using (DataReader textReader = new DataReader(fileStream))
                        {
                            uint textLength = (uint)fileStream.Size;
                            await textReader.LoadAsync(textLength);
                            Content = textReader.ReadString(textLength);
                        }

                    }
                }
            }
            catch (NullReferenceException)
            {

                return;
            }
            


        }
        public async Task CheckFile()
        {



            Document = await Files?.Properties.GetDocumentPropertiesAsync();
            



            //Music = await Files?.Properties.GetMusicPropertiesAsync();


        }
        /*
        public async Task ImageManipulationBasis(IRandomAccessStream fileStream)
        {

            
            // Read from stream
            /*
            using (MemoryStream memStream = (MemoryStream)fileStream)
            {
                ImageInfo = new MagickImageInfo(memStream);
            }

        }*/


    }
}
