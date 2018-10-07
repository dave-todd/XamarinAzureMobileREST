using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CatProj
{

    public enum ColourSchemes
    {
        Red = 0, Green = 1, Blue = 2, Yellow = 3
    }

    public sealed class Preferences
    {

        public ColourSchemes ColourScheme;
        public bool PlaySound;
        private static Preferences instance = null;
        private static readonly object padlock = new object();

        public Preferences()
        {
            ColourScheme = ColourSchemes.Blue;
            PlaySound = false;
        }

        public static Preferences Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Preferences();
                    }
                    return instance;
                }
            }
        }

        public Color BackgroundColour
        {
            get
            {
                if (ColourScheme == ColourSchemes.Red)
                    return Color.LightPink;
                else if (ColourScheme == ColourSchemes.Green)
                    return Color.LightGreen;
                else if (ColourScheme == ColourSchemes.Blue)
                    return Color.LightBlue;
                else
                    return Color.LightYellow;
            }
        }

        public void LoadSettings()
        {
            if (Application.Current.Properties.ContainsKey("ColourScheme"))
            {
                ColourScheme = (ColourSchemes)Application.Current.Properties["ColourScheme"];
            }
            if (Application.Current.Properties.ContainsKey("PlaySound"))
            {
                PlaySound = (bool)Application.Current.Properties["PlaySound"];
            }
        }

        public void SaveSettings()
        {
            Application.Current.Properties["ColourScheme"] = (int)ColourScheme;
            Application.Current.Properties["PlaySound"] = PlaySound;
        }

    }

}
