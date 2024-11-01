using Microsoft.Extensions.Options;

namespace AzureWorkshop.Web.Services.Configuration
{
    public class ServiceOptions : IOptions<ServiceOptions>
    {
        public ServiceOptions Value => this;

        public string BlobStorageConnectionString { get; set; } = string.Empty;
    }
}
