using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CRUDSample.Droid;
using SQLite;
using Xamarin.Forms;

[assembly:Dependency(typeof(SQLite_Android))]
namespace CRUDSample.Droid
{
    class SQLite_Android : ISQLite
    {
        SQLiteConnection con;
        public SQLiteConnection GetConnectionWithCreateDatabase()
        {
            string fileName = "sampleDatabase.db3";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentPath, fileName);
            con = new SQLiteConnection(path);
            con.CreateTable<Transaction>();
            con.CreateTable<User>();
            con.CreateTable<Goods>();

            // Insert default data if tables are empty
            if (!con.Table<User>().Any())
            {
                // Insert default user data
                con.Insert(new User { NameUser = "Faizal" });
                con.Insert(new User { NameUser = "Ilham" });
            }

            if (!con.Table<Goods>().Any())
            {
                // Insert default goods data
                con.Insert(new Goods { NameGoods = "Indomie", PriceGoods = 4000 });
                con.Insert(new Goods { NameGoods = "Supermie", PriceGoods = 3000 });
            }
            return con;
        }
        public bool SaveTransaction(Transaction transaction)
        {
            bool res = false;
            try
            {
                con.Insert(transaction);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public List<Transactions> GetTransaction()
        {
            var query = from transaction in con.Table<Transaction>()
                join goods in con.Table<Goods>() on transaction.IdGoods equals goods.IdGoods
                join user in con.Table<User>() on transaction.IdUser equals user.IdUser
                select new Transactions
                {
                    IdTransactions = transaction.IdTransaction,
                    name = goods.NameGoods,
                    user = user.NameUser,
                    Quantity = transaction.Quantity,
                    Price = transaction.Quantity * goods.PriceGoods,
                    DateTransaction = transaction.DateTransaction,
                };

            List<Transactions> result = query.ToList();
            foreach (var item in result)
            {
               
            }

            return result;
        }
        public bool UpdateTransaction(Transaction transaction)
        {
            bool res = false;
            try
            {
                string sql = $"UPDATE [Transaction] SET IdGoods='{transaction.IdGoods}',IdUser='{transaction.IdUser}',Quantity='{transaction.Quantity}'," +
                                $"DateTransaction='{transaction.DateTransaction}' WHERE IdTransaction={transaction.IdTransaction}";
                con.Execute(sql);
                res = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UHUY ${ex}");
                throw;

            }
            return res;
        }
        public void DeleteTransaction(int Id)
        {
            try
            {
                string sql = $"DELETE FROM [Transaction] WHERE IdTransaction={Id}";
                con.Execute(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine($"TESTYGY 2 ${e}");
                throw;
            }
        }

        public List<User> GetAllUser()
        {
            string sql = "SELECT * FROM User";
            List<User> users = con.Query<User>(sql);
            return users;
        }

        public List<Goods> GetAllGoods()
        {
            string sql = "SELECT * FROM Goods";
            List<Goods> goods = con.Query<Goods>(sql);
            return goods;
            
        }

        public Transaction TransactionById(int id)
        {
            Transaction transaction = con.Find<Transaction>(id);
            return transaction;
        }
    }   
}