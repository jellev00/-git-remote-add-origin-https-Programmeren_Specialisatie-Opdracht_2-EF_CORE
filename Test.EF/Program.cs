using ParkDataLayer.Model;
using System.Configuration;

namespace Test.EF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EF Model Test");

            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=ParkBeheerDB;Integrated Security=True;TrustServerCertificate=true";

            ParkBeheerContext ctx = new ParkBeheerContext(connectionString);

            ctx.Database.EnsureDeleted();
            Console.WriteLine("DB Deleted");
            ctx.Database.EnsureCreated();
            Console.WriteLine("DB Created");
        }
    }
}