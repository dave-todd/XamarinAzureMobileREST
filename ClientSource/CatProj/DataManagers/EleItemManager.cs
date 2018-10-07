using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace CatProj
{
    public partial class EleItemManager
    {
        static EleItemManager defaultInstance = new EleItemManager();
        MobileServiceClient client;
        IMobileServiceSyncTable<EleItem> dataTable;
        
        private EleItemManager()
        {
            client = new MobileServiceClient(Constants.ApplicationURL);
            MobileServiceSQLiteStore store = new MobileServiceSQLiteStore(Constants.OfflineDbPath);
            store.DefineTable<EleItem>();
            client.SyncContext.InitializeAsync(store);
            dataTable = client.GetSyncTable<EleItem>();
        }

        public static EleItemManager DefaultManager
        {
            get { return defaultInstance; }
            private set { defaultInstance = value; }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public async Task<ObservableCollection<EleItem>> GetDataItemsAsync(string id)
        {
            try
            {
                await SyncAsync();
                IEnumerable<EleItem> items = await dataTable.Where(dataItem => dataItem.CatId == id).ToEnumerableAsync();
                return new ObservableCollection<EleItem>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task SaveTaskAsync(EleItem item)
        {
            try
            {
                if (item.Id == null) { await dataTable.InsertAsync(item); }
                else { await dataTable.UpdateAsync(item); }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Save error: {0}", new[] { e.Message });
            }
        }

        public async Task DeleteTaskAsync(EleItem item)
        {
            try
            {
                await dataTable.DeleteAsync(item);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Delete error: {0}", new[] { e.Message });
            }
        }

        public async Task SyncAsync()
        {

            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await client.SyncContext.PushAsync();
                await dataTable.PullAsync("allCatItems", dataTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null) { syncErrors = exc.PushResult.Errors; }
            }

            if (syncErrors != null)
            {
                foreach (MobileServiceTableOperationError error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }

        }

    }

}
