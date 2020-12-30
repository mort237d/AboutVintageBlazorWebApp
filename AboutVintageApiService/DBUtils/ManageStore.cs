using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AboutVintageBlazorWebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace AboutVintageApiService.DBUtils
{
    public class ManageStore : IManageStore
    {
        private SqlConnection connection = Utils.GetConnection();

        public List<Store> GetAllStore()
        {
            string queryString = "SELECT * FROM stores;";
            List<Store> allStores = new List<Store>();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Store store = new Store();
                    store.Id = reader.GetInt32(0);
                    store.Date = reader.GetDateTime(1);
                    store.StoreName = reader.GetString(2);
                    store.RevenueCash = (float) reader.GetDouble(3);
                    store.RevenueMobilePay = (float)reader.GetDouble(4);
                    store.RevenueCreditCard = (float) reader.GetDouble(5);
                    store.CustomerCount = reader.GetInt32(6);
                    allStores.Add(store);
                }
            }
            finally
            {
                connection.Close();
                reader.Close();
            }

            return allStores;
        }

        public List<Store> GetTenStore(string storeName)
        {
            string queryString;
            if (!string.IsNullOrEmpty(storeName)) queryString = string.Format("SELECT TOP 10 * FROM stores WHERE StoreName = '{0}' ORDER BY Date DESC;", storeName);
            else queryString = string.Format("SELECT TOP 10 * FROM stores ORDER BY Date DESC;");
            List<Store> allStores = new List<Store>();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Store store = new Store();
                    store.Id = reader.GetInt32(0);
                    store.Date = reader.GetDateTime(1);
                    store.StoreName = reader.GetString(2);
                    store.RevenueCash = (float)reader.GetDouble(3);
                    store.RevenueMobilePay = (float)reader.GetDouble(4);
                    store.RevenueCreditCard = (float)reader.GetDouble(5);
                    store.CustomerCount = reader.GetInt32(6);
                    allStores.Add(store);
                }
            }
            finally
            {
                connection.Close();
                reader.Close();
            }

            return allStores;
        }

        public Store GetStoreFromId(int storeId)
        {
            string queryString = string.Format("SELECT * FROM stores WHERE Id = {0};", storeId);
            Store store = new Store();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    store.Id = reader.GetInt32(0);
                    store.Date = reader.GetDateTime(1);
                    store.StoreName = reader.GetString(2);
                    store.RevenueCash = (float)reader.GetDouble(3);
                    store.RevenueMobilePay = (float)reader.GetDouble(4);
                    store.RevenueCreditCard = (float)reader.GetDouble(5);
                    store.CustomerCount = reader.GetInt32(6);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            if(store.Id == 0) return null;
            return store;
        }

        public List<Store> GetStoreFromNameAndDate(DateTime minDate, DateTime maxDate, string storeName)
        {
            string queryString = string.Format("SELECT * FROM stores WHERE Date BETWEEN '{0}' AND '{1}' AND StoreName = '{2}';", 
                minDate.ToString("yyyy-MM-dd"), maxDate.ToString("yyyy-MM-dd"), storeName);

            if (storeName.Equals("All")) queryString = string.Format("SELECT * FROM stores WHERE Date BETWEEN '{0}' AND '{1}';", 
                minDate.ToString("yyyy-MM-dd"), maxDate.ToString("yyyy-MM-dd"));

            List<Store> allStores = new List<Store>();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Store store = new Store();
                    store.Id = reader.GetInt32(0);
                    store.Date = reader.GetDateTime(1);
                    store.StoreName = reader.GetString(2);
                    store.RevenueCash = (float)reader.GetDouble(3);
                    store.RevenueMobilePay = (float)reader.GetDouble(4);
                    store.RevenueCreditCard = (float)reader.GetDouble(5);
                    store.CustomerCount = reader.GetInt32(6);
                    allStores.Add(store);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return allStores;
        }

        public List<Store> GetStoreFromName(string storeName)
        {
            string queryString = string.Format("SELECT * FROM stores WHERE StoreName = '{0}';", storeName);
            List<Store> allStores = new List<Store>();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Store store = new Store();
                    store.Id = reader.GetInt32(0);
                    store.Date = reader.GetDateTime(1);
                    store.StoreName = reader.GetString(2);
                    store.RevenueCash = (float)reader.GetDouble(3);
                    store.RevenueMobilePay = (float)reader.GetDouble(4);
                    store.RevenueCreditCard = (float)reader.GetDouble(5);
                    store.CustomerCount = reader.GetInt32(6);
                    allStores.Add(store);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return allStores;
        }

        public bool CreateStore(Store store)
        {
            string queryString = string.Format("INSERT INTO stores (Date, StoreName, RevenueCash, RevenueMobilePay, RevenueCreditCard, CustomerCount) " +
                                               "VALUES (cast('{0}' as date), '{1}', {2}, {3}, {4}, {5});", 
                store.Date.ToString("yyyy-MM-dd"), store.StoreName, store.RevenueCash, store.RevenueMobilePay, store.RevenueCreditCard, store.CustomerCount);

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            try
            {
                return command.ExecuteNonQuery() == 0 ? false : true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool UpdateStore(Store store, int storeId)
        {
            string queryString = string.Format("UPDATE stores SET Date = cast('{0}' as date), StoreName = '{1}', RevenueCash = {2}, RevenueMobilePay = {3}, " +
                                               "RevenueCreditCard = {4}, CustomerCount = {5} WHERE Id = {6};",
                store.Date.ToString("yyyy-MM-dd"), store.StoreName, store.RevenueCash, store.RevenueMobilePay, store.RevenueCreditCard, store.CustomerCount, storeId);

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            try
            {
                return command.ExecuteNonQuery() == 0 ? false : true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool DeleteStore(int storeId)
        {
            string queryString = string.Format("DELETE FROM stores WHERE Id = {0};", storeId);

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            try
            {
                return command.ExecuteNonQuery() == 0 ? false : true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
