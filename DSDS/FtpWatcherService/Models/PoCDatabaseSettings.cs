namespace FtpWatcherService.Models
{
    public class PoCDatabaseSettings : IPoCDatabaseSettings
    {
        public string PoCCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }

    public interface IPoCDatabaseSettings
    {
        string PoCCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}