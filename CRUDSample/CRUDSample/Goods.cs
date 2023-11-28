using SQLite;

namespace CRUDSample
{
    public class Goods
    {
        [PrimaryKey,AutoIncrement]
        public int IdGoods { get; set; }
        public string NameGoods { get; set; }
        public int PriceGoods { get; set; }
    }
}