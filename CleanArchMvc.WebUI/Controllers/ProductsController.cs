using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;
        private string ImagesFolder { get => $@"{_environment.WebRootPath}\images"; }

        public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DefineViewBagImagesFolder();
            var products = await _productService.GetAllAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await DefineViewBagCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileToCopyForImage = product.FileToCopyForImage;
                    product = await _productService.CreateAsync(product);
                    product.FileToCopyForImage = fileToCopyForImage;
                    await _productService.SaveOrUpdateImage(product, ImagesFolder);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            await DefineViewBagCategories();
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, bool deleteImage)
        {
            ProductDTO product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await DefineViewBagCategories();
            DefineViewBagImagesFolder();

            if (deleteImage)
            {
                product.ExtensionImage = null;
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateAsync(product);

                    if (product.FileToCopyForImage != null)
                    {
                        await _productService.SaveOrUpdateImage(product, ImagesFolder);
                    }
                    else if (product.ExtensionImage == null)
                    {
                        await _productService.DeleteImage(product, ImagesFolder);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            await DefineViewBagCategories();
            return View(product);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ProductDTO product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.ImageExists = product.ImageExists(ImagesFolder);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfimed(int? id)
        {
            try
            {
                ProductDTO productDTO = await _productService.GetByIdAsync(id);
                await _productService.DeleteImage(productDTO, ImagesFolder);
                await _productService.RemoveAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            ProductDTO product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.ImageExists = product.ImageExists(ImagesFolder);

            return View(product);
        }

        private async Task DefineViewBagCategories()
        {
            var categories = (await _categoryService.GetAllAsync()).ToList().Select(i => new { i.Id, i.Name }).ToList();

            categories.Insert(0, new { Id = 0, Name = "[Select a Category]" });

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }

        private void DefineViewBagImagesFolder()
        {
            ViewBag.ImagesFolder = ImagesFolder;
        }
    }
}
