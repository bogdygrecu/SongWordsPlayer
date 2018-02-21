using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SongWordsPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static private MediaPlayer mediaPlayer;

        public static MainPage Instance { get; private set; }
        
        void Awake()
        {
            Instance = this;
        }

        public MainPage()
        {
            this.InitializeComponent();
            //mediaPlayer = new MediaPlayer();  //error, check why
            mediaPlayer = BackgroundMediaPlayer.Current;
            Awake();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MyMusicListBoxItem.IsSelected = true;
        }

        private void HamburgerMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;
        }

        private void MenuItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(MyMusicListBoxItem.IsSelected)
            {
                ContentFrame.Navigate(typeof(Frames.MyMusicFrame));
                TitleTextBlock.Text = "My Music";
            }
            else if(NowPlayingListBoxItem.IsSelected)
            {
                ContentFrame.Navigate(typeof(Frames.NowPlayingFrame));
                TitleTextBlock.Text = "Now Playing";
            }
            else if(PlaylistsListBoxItem.IsSelected)
            {
                ContentFrame.Navigate(typeof(Frames.PlaylistsFrame));
                TitleTextBlock.Text = "Playlists";
            }
            else if(AutoOffListBoxItem.IsSelected)
            {
                ContentFrame.Navigate(typeof(Frames.AutoOffFrame));
                TitleTextBlock.Text = "Auto Off";
            }
            else if(SettingsListBoxItem.IsSelected)
            {
                ContentFrame.Navigate(typeof(Frames.SettingsFrame));
                TitleTextBlock.Text = "Settings";
            }
            else
            {
                //do nothing
            }

            MenuSplitView.IsPaneOpen = false;
        }

        public void GetSelectedSong(object file)
        {
            var value = (StorageFile)file;
            mediaPlayer.SetFileSource(value);
            mediaPlayer.Play();
            NowPlayingListBoxItem.IsSelected = true;
            
        }

        private void AutoSuggest_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void AutoSuggest_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }
    }
}
