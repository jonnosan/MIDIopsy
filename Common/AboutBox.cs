﻿/* AboutBox.cs - Implementation of AboutBox class, which displays information about the application.
 * Note that this file is shared across applications.
 *
 * Copyright (c) 2017-9 Jeffrey Paul Bourdier
 *
 * Licensed under the MIT License.  This file may be used only in compliance with this License.
 * Software distributed under this License is provided "AS IS", WITHOUT WARRANTY OF ANY KIND.
 * For more information, see the accompanying License file or the following URL:
 *
 *   https://opensource.org/licenses/MIT
 */


/* Uri, Environment */
using System;

/* Process */
using System.Diagnostics;

/* Window, ResizeMode, GridLength, Thickness, SizeToContent */
using System.Windows;

/* Grid, ColumnDefinition, RowDefinition, Image, TextBlock, Button */
using System.Windows.Controls;

/* LineBreak, Hyperlink */
using System.Windows.Documents;

/* FocusManager */
using System.Windows.Input;

/* BitmapImage */
using System.Windows.Media.Imaging;

/* RequestNavigateEventArgs */
using System.Windows.Navigation;


namespace JeffBourdier
{
    /// <summary>Displays an About box that contains information about the application.</summary>
    public static class AboutBox
    {
        /**********
         * Fields *
         **********/

        #region Public Fields

        /// <summary>Predefined width of the banner to be displayed across the top of the About box.</summary>
        public const int BannerWidth = 512;

        /// <summary>Predefined height of the banner to be displayed across the top of the About box.</summary>
        public const int BannerHeight = 80;

        #endregion

        #region Private Fields

        private const int MarginLength = AboutBox.BannerHeight / 5;

        #endregion

        /**************
         * Properties *
         **************/

        #region Private Properties

        /// <summary>Gets a string describing the version of the executing application.</summary>
        private static string VersionInfo
        { get { return string.Format("{0} {1}", Common.Resources.Version, AppHelper.Version); } }

        #endregion

        /***********
         * Methods *
         ***********/

        #region Public Methods

        /// <summary>Displays an About box in front of the specified window.</summary>
        /// <param name="owner">The owner window of the About box.</param>
        public static void Show(Window owner)
        {
            /* Image (banner) (Note: "Build Action" property must be set to "Resource") */
            Image image = new Image();
            Uri uri = AppHelper.CreateResourceUri(false, "Banner.bmp");
            image.Source = new BitmapImage(uri);

            /* Info text block */
            TextBlock textBlock = new TextBlock();
            textBlock.Margin = new Thickness(AboutBox.MarginLength, AboutBox.MarginLength / 2, 0, 0);
            textBlock.Inlines.Add(AppHelper.Product);
            textBlock.Inlines.Add(new LineBreak());
            textBlock.Inlines.Add(AboutBox.VersionInfo);
            textBlock.Inlines.Add(new LineBreak());
            textBlock.Inlines.Add(AppHelper.Copyright);
            textBlock.Inlines.Add(new LineBreak());
            Hyperlink hyperlink = new Hyperlink();
            hyperlink.NavigateUri = new Uri("https://jeffbourdier.github.io/" + AppHelper.Title.ToLower());
            hyperlink.Inlines.Add(hyperlink.NavigateUri.AbsoluteUri);
            hyperlink.RequestNavigate += AboutBox.Hyperlink_RequestNavigate;
            textBlock.Inlines.Add(hyperlink);

            /* OK button */
            Button button = new Button();
            button.Margin = new Thickness(AboutBox.MarginLength);
            button.Content = Common.Resources.OK;
            button.Click += UI.OkButton_Click;
            button.IsCancel = true;
            button.IsDefault = true;

            /* Define the grid content panel. */
            Grid grid = new Grid();
            grid.Width = AboutBox.BannerWidth;
            grid.Height = 2 * AboutBox.BannerHeight;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions[0].Width = new GridLength(AboutBox.BannerWidth - AboutBox.BannerHeight);
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            /* Add controls to the grid. */
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, 0);
            Grid.SetColumnSpan(image, 2);
            Grid.SetRow(textBlock, 1);
            Grid.SetColumn(textBlock, 0);
            Grid.SetRow(button, 1);
            Grid.SetColumn(button, 1);
            grid.Children.Add(image);
            grid.Children.Add(textBlock);
            grid.Children.Add(button);

            /* Build and show the window. */
            Window window = new Window();
            window.WindowStyle = WindowStyle.ToolWindow;
            window.ResizeMode = ResizeMode.NoResize;
            window.Title = Common.Resources.About + " " + AppHelper.Title;
            window.Content = grid;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Owner = owner;
            FocusManager.SetFocusedElement(window, button);
            window.ShowDialog();
        }

        #endregion

        #region Private Methods

        #region Event Handlers

        private static void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        { Process.Start(e.Uri.AbsoluteUri); }

        #endregion

        #endregion
    }
}
