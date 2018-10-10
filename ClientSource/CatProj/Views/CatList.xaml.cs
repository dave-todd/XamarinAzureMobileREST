using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CatProj
{

    public partial class CatList : ContentPage
    {

        private CatItemManager manager;
        private CatItem current;

        public CatList()
        {
            InitializeComponent();
            manager = CatItemManager.DefaultManager;
            SetColours();
            MessagingCenter.Subscribe<PreferencesPage>(this, "PreferenceChange", (sender) => { PreferenceChange(); });
        }

        private void PreferenceChange()
        {
            SetColours();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshItems();
        }

        public async void OnSyncItems(object sender, EventArgs e)
        {
            await RefreshItems();
        }

        public async void OnRefreshItems(object sender, EventArgs e)
        {
            await RefreshItems();
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            await RefreshItems();
        }

        private async Task RefreshItems()
        {
            using (var scope = new ActivityIndicatorScope(syncIndicator))
            {
                catList.ItemsSource = await manager.GetDataItemsAsync();
            }
        }

        public void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            current = e.SelectedItem as CatItem;
            catList.SelectedItem = current;
            newItemName.Text = current.Name;
            Library.PlayClick();
        }

        public async void OnAdd(object sender, EventArgs e)
        {
            if (newItemName.Text.Trim() == "") { return; }

            if (await DisplayAlert("Add?", "Would you like to Add category {" + newItemName.Text + "}", "Yes", "No"))
            {
                CatItem item = new CatItem();
                item.Name = newItemName.Text;
                item.OS = Library.GetOS();
                await manager.SaveTaskAsync(item);
                await RefreshItems();
            }
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            if (await DisplayAlert("Delete?", "Would you like to Delete category {" + current.Name + "}", "Yes", "No"))
            {
                current.OS = Library.GetOS();
                await manager.DeleteTaskAsync(current);
                await RefreshItems();
                newItemName.Text = string.Empty;
                newItemName.Unfocus();
            }
        }

        public async void OnRename(object sender, EventArgs e)
        {
            if (newItemName.Text.Trim() == "") { return; }

            if (await DisplayAlert("Rename?", "Would you like to Rename category {" + current.Name + "} to {" + newItemName.Text + "}", "Yes", "No"))
            {
                current.Name = newItemName.Text;
                current.OS = Library.GetOS();
                await manager.SaveTaskAsync(current);
                await RefreshItems();
            }
        }

        public async void OnDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EleList(current.Id));
        }

        public async void OnPreferences(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreferencesPage());
        }

        private void SetColours()
        {
            Preferences.Instance.LoadSettings();
            topBar.BackgroundColor = Preferences.Instance.BackgroundColour;
            bottomBar.BackgroundColor = Preferences.Instance.BackgroundColour;
        }

    }

}

