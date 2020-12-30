using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AboutVintageBlazorWebApp.Data;

namespace DataAccessLibrary
{
    public class StoreData : IStoreData
    {
        private readonly ISqlDataAccess _db;

        public StoreData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<StoreModel>> GetStores()
        {
            string sql = "SELECT * FROM stores ORDER BY Date DESC";

            return _db.LoadData<StoreModel, dynamic>(sql, new { });
        }

        public Task<List<StoreModel>> GetStoresByDateAndStore(string store, DateTime minDate, DateTime maxDate)
        {
            string sql = "SELECT * FROM stores WHERE Date BETWEEN @MinDate AND @MaxDate AND StoreName = @StoreName";
            if(store.Equals("All")) sql = "SELECT * FROM stores WHERE Date BETWEEN @MinDate AND @MaxDate";

            return _db.LoadData<StoreModel, dynamic>(sql, new { StoreName = store, MinDate = minDate, MaxDate = maxDate });
        }

        public Task<List<StoreModel>> GetTopTenStores()
        {
            string sql = "SELECT TOP 10 * FROM stores ORDER BY Date DESC";

            return _db.LoadData<StoreModel, dynamic>(sql, new { });
        }

        public Task AddStore(StoreModel store)
        {
            string sql = "INSERT INTO stores (Date, StoreName, RevenueCash, RevenueMobilePay, RevenueCreditCard, CustomerCount)" +
                         "VALUES (@Date, @StoreName, @RevenueCash, @RevenueMobilePay, @RevenueCreditCard, @CustomerCount);";

            return _db.SaveData(sql, store);
        }

        public Task UpdateStore(StoreModel store)
        {
            string sql = "UPDATE stores SET Date = @Date, StoreName = @StoreName, RevenueCash = @RevenueCash, RevenueMobilePay = @RevenueMobilePay, " +
                         "RevenueCreditCard = @RevenueCreditCard, CustomerCount = @CustomerCount WHERE Id = @Id;";

            return _db.SaveData(sql, store);
        }

        public Task DeleteStore(int id)
        {
            string sql = "DELETE FROM stores WHERE Id = @Id;";

            return _db.DeleteData(sql, new { Id = id });
        }
    }
}
