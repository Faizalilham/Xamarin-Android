using SQLite;

namespace CRUDSample
{
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int IdUser { get; set; }
        public string NameUser { get; set; }
    }
}