﻿using System.Linq;
using Windows.UI.Xaml.Controls;
using RMP.App.Settings;
using NavigationViewItem = Microsoft.UI.Xaml.Controls.NavigationViewItem;
using NavigationViewItemBase = Microsoft.UI.Xaml.Controls.NavigationViewItemBase;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using System;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RMP.App.Dialogs
{
    public sealed partial class SettingsDialog : ContentDialog
    {
        #region Variables
        public static SettingsDialog Current;
        public ObservableCollection<string> Breadcrumbs =
            new ObservableCollection<string>();
        #endregion

        #region NavView Icons
        private readonly ImageIcon libraryIconColor =
            new ImageIcon() { Source = new BitmapImage(new Uri("ms-appx:///Assets/Settings/Library.png")) };

        private readonly FontIcon libraryIconMono =
            new FontIcon() { Glyph = "\uEA69" };

        private readonly ImageIcon playbackIconColor =
            new ImageIcon() { Source = new BitmapImage(new Uri("ms-appx:///Assets/Settings/Playback.png")) };

        private readonly FontIcon playbackIconMono =
            new FontIcon() { Glyph = "\uE102" };

        private readonly ImageIcon appearanceIconColor =
            new ImageIcon() { Source = new BitmapImage(new Uri("ms-appx:///Assets/Settings/Appearance.png")) };

        private readonly FontIcon appearanceIconMono =
            new FontIcon() { Glyph = "\uE771" };

        private readonly ImageIcon feedbackIconColor =
            new ImageIcon() { Source = new BitmapImage(new Uri("ms-appx:///Assets/Settings/Feedback.png")) };

        private readonly FontIcon feedbackIconMono =
            new FontIcon() { Glyph = "\uE11D" };

        private readonly ImageIcon languageIconColor =
            new ImageIcon() { Source = new BitmapImage(new Uri("ms-appx:///Assets/Settings/Language.png")) };

        private readonly FontIcon languageIconMono =
            new FontIcon() { Glyph = "\uE12B" };

        private readonly ImageIcon aboutIconColor =
            new ImageIcon() { Source = new BitmapImage(new Uri("ms-appx:///Assets/Settings/About.png")) };

        private readonly FontIcon aboutIconMono =
            new FontIcon() { Glyph = "\uE946" };
        #endregion

        public SettingsDialog()
        {
            this.InitializeComponent();
            Current = this;

            // Sidebar icon colors
            UpdateIconColor(NavigationSettings.ColorfulIcons);

            SettingsFrame.Navigate(typeof(MediaLibraryPage));
            FinishNavigation();
            ContentDialog_SizeChanged(null, null);
        }

        #region Navigation
        private void SettingsNav_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer.Content.ToString() == Breadcrumbs.Last())
            {
                FinishNavigation();
                return;
            }

            string navTo = args.InvokedItemContainer.Tag.ToString();
            if (navTo != null)
            {
                switch (navTo)
                {
                    case "AppearancePage":
                        SettingsFrame.Navigate(typeof(AppearancePage));
                        break;

                    case "MediaLibraryPage":
                        SettingsFrame.Navigate(typeof(MediaLibraryPage));
                        break;

                    case "PlaybackPage":
                        SettingsFrame.Navigate(typeof(PlaybackPage));
                        break;

                    case "LanguagePage":
                        SettingsFrame.Navigate(typeof(LanguagePage));
                        break;

                    case "AboutPage":
                        SettingsFrame.Navigate(typeof(AboutPage));
                        break;

                    default:
                        break;
                }
            }

            FinishNavigation();
        }

        private void FinishNavigation()
        {
            string type = SettingsFrame.CurrentSourcePageType.ToString();
            string tag = type.Split('.').Last();

            foreach (NavigationViewItemBase item in SettingsNav.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == tag)
                {
                    SettingsNav.SelectedItem = item;
                    Breadcrumbs.Clear();
                    Breadcrumbs.Add(item.Content.ToString());
                    break;
                }
            }

            foreach (NavigationViewItemBase item in SettingsNav.FooterMenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == tag)
                {
                    SettingsNav.SelectedItem = item;
                    Breadcrumbs.Clear();
                    Breadcrumbs.Add(item.Content.ToString());
                    break;
                }
            }
        }
        #endregion

        #region Settings
        public void UpdateIconColor(bool coloredIcons)
        {
            if (coloredIcons)
            {
                MediaLibraryPageItem.Icon = libraryIconColor;
                PlaybackPageItem.Icon = playbackIconColor;
                AppearancePageItem.Icon = appearanceIconColor;
                FeedbackPageItem.Icon = feedbackIconColor;
                LanguagePageItem.Icon = languageIconColor;
                AboutPageItem.Icon = aboutIconColor;
                return;
            }

            MediaLibraryPageItem.Icon = libraryIconMono;
            PlaybackPageItem.Icon = playbackIconMono;
            AppearancePageItem.Icon = appearanceIconMono;
            FeedbackPageItem.Icon = feedbackIconMono;
            LanguagePageItem.Icon = languageIconMono;
            AboutPageItem.Icon = aboutIconMono;
        }
        #endregion

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            MainPage.Current.FinishNavigation();
        }

        private void ContentDialog_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            double windowWidth = Window.Current.Bounds.Width;
            double windowHeight = Window.Current.Bounds.Height;

            if (windowWidth < 800)
            {
                SettingsNav.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.LeftCompact;
                SettingsFrame.Width = windowWidth - 178;
                DualTone.Width = 96;
            }
            else
            {
                SettingsNav.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Left;
                SettingsFrame.Width = 422;
                DualTone.Width = 248;
            }

            if (windowHeight < 630)
            {
                RootGrid.Height = windowHeight - 100;
            }
            else
            {
                RootGrid.Height = 530;
            }
        }
    }
}
