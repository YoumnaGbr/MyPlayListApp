using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Graph;
using Microsoft.Graph.Ediscovery;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.FilterModels;
using MyPlayListApp.Data.Helpers;
using MyPlayListApp.Data.Interfaces;
using MyPlayListApp.Data.Repositories;
using MyPlayListApp.Data.Services;
using MyPlayListApp.Data.ViewModels;
using MyPlayListApp.Models;

namespace MyPlayListApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISongService _songService;
        private readonly ISingerService _singerService;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, ISongService songService, ISingerService singerService, ICategoryService categoryService)
        {
            _logger = logger;
            _songService = songService;
            _singerService = singerService;
            _categoryService = categoryService;
        }
        public IActionResult GetSingerDetails(Guid id)
        {
            var singer = _singerService.GetSingerDetailes(id);  
            if (singer == null)
            {
                return NotFound();
            }
            var viewModel = new SingerDetailes
            {
                Name = singer.Name,
                Image = singer.Image,
                DateOfBirth = singer.DateOfBirth
            };

            return PartialView("_SingerDetalies", viewModel);
        }
        public IActionResult Index(SongsFilter filter)
        {
            if (filter.PageIndex < 1)
            {
                filter.PageIndex = 1;
            }
            if (filter.PageSize < 1)
            {
                filter.PageSize = 10;
            }
            var result = _songService.GetPlayList(filter);
            var singers = _singerService.GetSingerList();
            var categories = _categoryService.GetCategoriesList();

            var model = new SongViewModel
            {
                Filter = filter, 
                songs = result.songs,
                PageIndex = result.PageIndex,
                TotalPages = result.TotalPages,
                TotalCount = result.TotalCount,
                Singers = new SelectList(singers, "Id", "Name"),
                Categories = new SelectList(categories, "Id", "Name"),
            };

            return View(model);
        }
        public IActionResult GetCategories(CategoriesFilter filter)
        {
            filter.PageIndex = 0;
            filter.PageSize = 10;
            var categories = _categoryService.GetCategories(filter);

            return View("Category", categories.Categories);
        }
        [HttpPost]
        public IActionResult AddNewCategory(CategoryDetailes categoryDetailes)
        {
            var result = _categoryService.AddNewCategory(categoryDetailes);
            return Json(result);
        }
        public IActionResult GetSingers(SingersFilter filter)
        {
            filter.PageIndex = 0;
            filter.PageSize = int.MaxValue;
            var singerDetails = _singerService.GetSingers(filter);

            return View("Singer", singerDetails.Singers);
        }
        
        [HttpPost]
        public IActionResult AddSong(SongDetailes songDetailes)
        {
            var result = _songService.AddNewSong(songDetailes);
            return Json(result);
        }

        [HttpPost]
        public IActionResult UpdateSong(SongDetailes songDetailes)
        {
            var result = new ResultBase();
            if (string.IsNullOrWhiteSpace(songDetailes.Name))
            {
                result.Success = false;
                result.Message = "Song Name Cannot Be Empty";
                return Json(result);
            }
            
            result = _songService.UpdateSong(songDetailes);
            return Json(result);
        }

        [HttpPost]
        public IActionResult AddNewSinger(SingerDetailes singerDetails)
        {
            var result = _singerService.AddNewSinger(singerDetails);
            return Json(result);
        }

        [HttpPost]
        public IActionResult DeleteSong(Guid songId) 
        {
            var result = _songService.DeleteSong(songId);
            return Json(result);
        }

        [HttpPost]
        public IActionResult DeleteSinger(Guid singerId)
        {
            var result = _singerService.DeleteSinger(singerId);
            return Json(result);
        }
        [HttpPost]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            var result = _categoryService.DeleteCategory(categoryId);
            return Json(result);
        }
        [HttpPost]
        public IActionResult UpdateCategory([FromBody] CategoryDetailes categoryDetailes)
        {
            var result = new ResultBase();
            if (string.IsNullOrWhiteSpace(categoryDetailes.CategoryName))
            {
                result.Success = false;
                result.Message = "Category Name Cannot Be Empty";
                return Json(result);
            }
            result = _categoryService.UpdateCategory(categoryDetailes);
            return Json(result);
        }

        public IActionResult UpdateSinger(SingerDetailes singerDetails)
        {
            var result = new ResultBase();
            if (string.IsNullOrWhiteSpace(singerDetails.Name))
            {
                result.Success = false;
                result.Message = "Name Cannot Br Empty";
                return Json(result);
            }
            result = _singerService.UpdateSinger(singerDetails);
            return Json(result);
        }
    }
}
