using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace HyperFileChanger.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MusicFrame : Page
    {
        MediaPlayer _mediaplayer;
        

        // Benutz fucking mediaplayer
        public MusicFrame()
        {
            this.InitializeComponent();
            //Player.SetMediaPlayer(_mediaplayer);
            _mediaplayer = Player.MediaPlayer;
            _mediaplayer.SourceChanged += new TypedEventHandler<MediaPlayer, object>( fisch );
        } 
        public void fisch(MediaPlayer e, object sender)
        {
            
            
        }

        private void Unloading(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
