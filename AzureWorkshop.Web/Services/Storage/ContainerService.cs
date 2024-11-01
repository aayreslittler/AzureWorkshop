using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureWorkshop.Web.Services.Configuration;
using Microsoft.Extensions.Options;

namespace AzureWorkshop.Web.Services.Storage
{
    public class ContainerService : IContainerService
    {
        private readonly string _connectionString;

        public ContainerService(IOptions<ServiceOptions> options)
        {
            _connectionString = options.Value.BlobStorageConnectionString;
        }

        public List<BlobContainerItem> GetContainers()
        {
            //1 - Create a new Blob Service Client
            var client = new BlobServiceClient(_connectionString);

            //2 - Use the client to get a list of containers
            var containers = client.GetBlobContainers();

            return containers.ToList();
        }

        public void CreateContainer(string containerName)
        {
            //1 - Create a new Blob Service Client
            var client = new BlobServiceClient(_connectionString);

            //2 - Use the client to create a new container
            client.CreateBlobContainer(containerName);
        }

        public List<BlobItem> GetContainerContents(string containerName)
        {
            //1 - Create a new Blob Service Client
            var client = new BlobServiceClient(_connectionString);

            //2 - Use the client to get a reference to the container client
            var containerClient = client.GetBlobContainerClient(containerName);

            //3 - Use the container client to get a list of blobs
            var blobs = containerClient.GetBlobs();

            return blobs.ToList();
        }

        public void UploadFile(string containerName, IFormFile file)
        {
            //1 - Create a new Blob Service Client
            var client = new BlobServiceClient(_connectionString);

            //2 - Use the client to get a reference to the container client
            var containerClient = client.GetBlobContainerClient(containerName);

            //3 - Use the container client to get a reference to the blob client
            var blobClient = containerClient.GetBlobClient(file.FileName);

            //4 - Use the blob client to upload the file
            blobClient.Upload(file.OpenReadStream(), true);

            //5 - Set the content type
            //blobClient.SetHttpHeaders(new BlobHttpHeaders()
            //{
            //    ContentType = file.ContentType
            //});

            //6 - Set metadata
            //var metaData = new Dictionary<string, string>();
            //metaData.Add("UploadedBy", "Adam");

            //blobClient.SetMetadata(metaData);
        }
    }
}
