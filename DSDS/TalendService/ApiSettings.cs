namespace TalendService
{
    public class ApiSettings : IApiSettings
    {
        public string LookupApiUrl { get; set; }
        public string DataTransformationApiUrl { get; set; }
    }

    public interface IApiSettings
    {
        string LookupApiUrl { get; set; }
        string DataTransformationApiUrl { get; set; }
    }
}