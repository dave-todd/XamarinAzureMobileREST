using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CatProj
{

    public static class Library
    {

        private static Plugin.SimpleAudioPlayer.ISimpleAudioPlayer player = null;

        public static string GetOS()
        {
            return Device.RuntimePlatform.ToString();
        }

        public static void PlayClick()
        {
            if (Preferences.Instance.PlaySound)
            {
                if (player == null)
                {
                    player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                    player.Load("tap.wav");
                }
                player.Play();
            }
        }

    }
}
