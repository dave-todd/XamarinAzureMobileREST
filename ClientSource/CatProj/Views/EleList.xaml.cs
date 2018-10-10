using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CatProj
{

    public partial class EleList : ContentPage
    {

        private EleItemManager manager;
        private EleItem current;
        private string currentCatId;

        public EleList(string id)
        {
            InitializeComponent();
            manager = EleItemManager.DefaultManager;
            currentCatId = id;
            SetCategoryName();
            SetColours();
            MessagingCenter.Subscribe<PreferencesPage>(this, "PreferenceChange", (sender) => { PreferenceChange(); });
        }

        private void PreferenceChange()
        {
            SetColours();
        }

        private async void SetCategoryName()
        {
            CatItemManager catItemManager = CatItemManager.DefaultManager;
            CatItem catItem = await catItemManager.GetCategoryAsync(currentCatId);
            categoryName.Text = "Category { " + catItem.Name + " }";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshItems();
        }

        public void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            current = e.SelectedItem as EleItem;
            eleList.SelectedItem = current;
            newItemName.Text = current.Name;
            Library.PlayClick();
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
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

        private async Task RefreshItems()
        {
            using (var scope = new ActivityIndicatorScope(syncIndicator))
            {
                eleList.ItemsSource = await manager.GetDataItemsAsync(currentCatId);
            }
        }

        public async void OnAdd(object sender, EventArgs e)
        {
            if (newItemName.Text.Trim() == "") { return; }

            if (await DisplayAlert("Add?", "Would you like to Add element {" + newItemName.Text + "}", "Yes", "No"))
            {
                EleItem item = new EleItem();
                item.Name = newItemName.Text;
                item.CatId = currentCatId;
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

        public async void OnPreferences(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreferencesPage());
        }

        private void SetColours()
        {
            topBar.BackgroundColor = Preferences.Instance.BackgroundColour;
            bottomBar.BackgroundColor = Preferences.Instance.BackgroundColour;
        }

    }

}

