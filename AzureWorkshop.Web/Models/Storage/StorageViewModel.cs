using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AzureWorkshop.Web.Models.Storage
{
    public class StorageViewModel
    {
        public string SelectedContainer { get; set; } = string.Empty;

        public List<SelectListItem>? Containers { get; set; }

        public List<BlobItem>? BlobItems { set; get; }
    }
}
