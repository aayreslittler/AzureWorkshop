using AzureWorkshop.Web.Models.Storage;
using AzureWorkshop.Web.Services.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AzureWorkshop.Web.Controllers
{
    public class StorageController : Controller
    {
        private readonly IContainerService _containerService;

        public StorageController(IContainerService containerService)
        {
            _containerService = containerService;
        }

        [HttpGet]
        public IActionResult Index(string? container = "")
        {
            var model = new StorageViewModel();
            model.SelectedContainer = container;

            model.Containers =
            [
                new SelectListItem() { Text = "Select a Container...", Value = "" },
                .. _containerService.GetContainers().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Name,
                    Selected = x.Name == container
                })
            ];

            if (!string.IsNullOrEmpty(container))
            {
                model.BlobItems = _containerService.GetContainerContents(container);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateContainer()
        {
            var model = new CreateContainerViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateContainer(CreateContainerViewModel model)
        {
            if (ModelState.IsValid)
            {
                _containerService.CreateContainer(model.ContainerName);
                return RedirectToAction("Index", new { container = model.ContainerName});
            }

            return View();
        }

        [HttpPost]
        public IActionResult Upload(string containerName, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            _containerService.UploadFile(containerName, file);
            return RedirectToAction("Index", new { container = containerName });
        }
    }
}
