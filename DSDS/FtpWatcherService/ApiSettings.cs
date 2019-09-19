namespace FtpWatcherService
{
    public class ApiSettings : IApiSettings
    {
        public string DataTransformationApiUrl { get; set; }
    }

    public interface IApiSettings
    {
        string DataTransformationApiUrl { get; set; }
    }
}