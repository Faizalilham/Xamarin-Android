using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CRUDSample
{
    public class Transaction
    {
        [PrimaryKey,AutoIncrement]
        public int IdTransaction { get; set; }
        
        [ForeignKey(typeof(Goods))]
        public int IdGoods { get; set; }
        
        [ForeignKey(typeof(User))]
        public int IdUser { get; set; }
        
        public int Quantity { get; set; }
        public string DateTransaction { get; set; }
    }
}