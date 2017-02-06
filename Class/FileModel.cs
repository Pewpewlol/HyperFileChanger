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
using Windows.Media.Core;
using Windows.UI.Text;
using System.Collections.ObjectModel;

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

        MediaSource mSource;
        public MediaSource MSource
        {
            get { return mSource; }
            set
            {
                if (mSource != value)
                {
                    mSource = value;

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
        StorageFolder folder;
        public StorageFolder Folder
        {
            get { return folder; }
            set
            {
                if(folder != value)
                {
                    folder = value;
                    this.OnPropertyChanged();
                }
            }
        }

        ObservableCollection<StorageFile> filesliste;
        public ObservableCollection<StorageFile> FileslisteObserv
        { get { return filesliste; }
          set
            {
                if(filesliste != value)
                {
                    filesliste = value;
                    this.OnPropertyChanged();
                }
            }
        }
        
        
        IReadOnlyList<IStorageItem> filesList;
        public IReadOnlyList<IStorageItem> FilesList
        {
            get { return filesList; }
            set
            {
                if (filesList != value)
                {
                    filesList = value;
                    this.OnPropertyChanged();
                }
            }
        }
        ITextDocument textDocument;
        public ITextDocument TextDocument
        {
            get { return textDocument; }
            set
            {
                if (textDocument != value)
                {
                    textDocument = value;
                    this.OnPropertyChanged();
                }
            }
        }
        IRandomAccessStream FileStream;
        public IRandomAccessStream fileStream
        {
            get { return FileStream; }
            set
            {
                if (FileStream != value)
                {
                    FileStream = value;
                    this.OnPropertyChanged();
                }
            }
        }

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
                Folder = await Files.GetParentAsync();
                
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
                    using ( fileStream = await Files.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {

                        PDF = await PdfDocument.LoadFromStreamAsync(fileStream);
                        


                    }
                }
                else if( DateiTyp == "Textdokument" )
                {
                    using ( fileStream = await Files.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {
                        TextDocument.LoadFromStream(TextSetOptions.FormatRtf, fileStream);
                        Document = await Files.Properties.GetDocumentPropertiesAsync();
                        using (DataReader textReader = new DataReader(fileStream))
                        {
                            uint textLength = (uint)fileStream.Size;
                            await textReader.LoadAsync(textLength);
                            Content = textReader.ReadString(textLength);
                        }

                    }
                }
                else if( DateiTyp == "MP3-Datei")
                {
                    
                    using (fileStream = await Files.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {

                        Music = await Files.Properties.GetMusicPropertiesAsync();
                        MSource = MediaSource.CreateFromStream(fileStream, "audio / mpeg");
                        
                    }
                    
                }
                
            }
            catch (NullReferenceException)
            {

                return;
            }
            


        }
        public async Task GetPartentFolder(ObservableCollection<IStorageItem> wuhu)
        {

            
            FilesList = await Folder.GetItemsAsync();
            foreach (var item in FilesList)
            {
                //FileslisteObserv.Add(item);
            }
            
        }
        public async Task PickFolder()
        {
            try
            {
                FolderPicker picker = new FolderPicker();
                picker.SuggestedStartLocation = PickerLocationId.Desktop;
                picker.FileTypeFilter.Add("*");
                Folder = await picker.PickSingleFolderAsync();
                FilesList = await Folder.GetItemsAsync();
                
                foreach (var item in FilesList)
                {
                    FileslisteObserv.Add(item as StorageFile);
                }
            }
            catch(NullReferenceException)
            {
                return;
            }
        }

    }
}
