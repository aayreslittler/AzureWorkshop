using Azure.Storage.Blobs.Models;

namespace AzureWorkshop.Web.Services.Storage
{
    public interface IContainerService
    {
        void CreateContainer(string containerName);
        List<BlobItem> GetContainerContents(string containerName);
        List<BlobContainerItem> GetContainers();
        void UploadFile(string containerName, IFormFile file);
    }
}