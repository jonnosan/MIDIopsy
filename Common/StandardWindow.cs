﻿/* StandardWindow.cs - Implementation of StandardWindow class, which standardizes the way windows are created.
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


/* Window, WindowState, Rect, WindowStartupLocation */
using System.Windows;


namespace JeffBourdier
{
    /// <summary>Extends the Window class by standardizing the way windows are created.</summary>
    public class StandardWindow : Window
    {
        /**************
         * Properties *
         **************/

        #region Public Properties

        /// <summary>Gets whether or not the window is maximized.</summary>
        public bool Maximized { get { return this.WindowState == WindowState.Maximized; } }

        /// <summary>Gets the location and size of the window when it is not maximized.</summary>
        public Rect Bounds
        {
            get
            {
                return this.WindowState == WindowState.Normal ?
                    new Rect(this.Left, this.Top, this.Width, this.Height) : this.RestoreBounds;
            }
        }

        #endregion

        /***********
         * Methods *
         ***********/

        #region Public Methods

        /// <summary>Restores the preferences of this window object from user configuration settings.</summary>
        /// <param name="maximized">Indicates whether or not the window is maximized.</param>
        /// <param name="bounds">The location and size of the window when it is not maximized.</param>
        public void RestorePreferences(bool maximized, Rect bounds)
        {
            if (maximized) this.WindowState = WindowState.Maximized;

            if (bounds.Width < 1 || bounds.Height < 1) return;
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Top = bounds.Top;
            this.Left = bounds.Left;
            this.Width = bounds.Width;
            this.Height = bounds.Height;
        }

        #endregion
    }
}
