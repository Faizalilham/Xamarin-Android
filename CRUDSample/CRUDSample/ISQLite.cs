using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDSample
{
    public interface ISQLite
    {
        SQLiteConnection GetConnectionWithCreateDatabase();

        bool SaveTransaction(Transaction transaction);

        List<Transactions> GetTransaction();

        bool UpdateTransaction(Transaction transaction);
        void DeleteTransaction(int Id);
        
        List<User> GetAllUser();
        
        List<Goods> GetAllGoods();

        Transaction TransactionById(int id);
    }
}
