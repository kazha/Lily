namespace Lily.Services.Configuration
{
    public class ServiceConfiguration
    {
        public const string ServiceConfigurationKey = "ServiceConfiguration";

        public string EmployeeDataSource { get; set; }
        public Coordinates CurrentLocation { get; set; }
    }
}
