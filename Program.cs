namespace Studentregistreringsprogram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            StudentDbContext dbCtx = new StudentDbContext();
            DatabaseHandler databaseHandler = new DatabaseHandler(dbCtx);
            ApplicationUI applicationUI = new ApplicationUI(databaseHandler);
            applicationUI.RunUI();

            // connectionstring: Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Studentregister;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
        }
    }
}
