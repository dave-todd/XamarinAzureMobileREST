using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatProj
{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PreferencesPage : ContentPage
	{

		public PreferencesPage ()
		{
			InitializeComponent ();
            SetColours();
            colourPicker.SelectedIndex = (int)Preferences.Instance.ColourScheme;
            soundSwitch.IsToggled = Preferences.Instance.PlaySound;
        }

        public void OnSave(object sender, EventArgs e)
        {
            Preferences.Instance.SaveSettings();
            MessagingCenter.Send(this, "PreferenceChange");
            Library.PlayClick();
            Navigation.PopAsync();
        }

        public void OnCancel(object sender, EventArgs e)
        {
            Preferences.Instance.LoadSettings();
            Library.PlayClick();
            Navigation.PopAsync();
        }

        private void OnPickerChanged(object sender, EventArgs e)
        {
            Preferences.Instance.ColourScheme = (ColourSchemes)colourPicker.SelectedIndex;
            SetColours();
        }

        public void OnSoundToggle(object sender, ToggledEventArgs e)
        {
            Switch x = (Switch)sender;
            Preferences.Instance.PlaySound = x.IsToggled;
            Preferences.Instance.SaveSettings();
            Library.PlayClick();
        }

        private void SetColours()
        {
            topBar.BackgroundColor = Preferences.Instance.BackgroundColour;
            bottomBar.BackgroundColor = Preferences.Instance.BackgroundColour;
        }

    }

}