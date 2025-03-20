using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Graph.Ediscovery;
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
            filter.PageIndex = 0;
            filter.PageSize = 10;
            var result = _songService.GetPlayList(filter);
            var singers = _singerService.GetSingerList();
            var categories = _categoryService.GetCategoriesList();

            // Create the ViewModel for the Index view
            var model = new SongViewModel
            {
                Filter = filter, 
                SongItemResult = result,
                Singers = new SelectList(singers, "Id", "Name"),
                Categories = new SelectList(categories, "Id", "Name"),
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
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
            if (categoryDetailes != null)
            {
                _categoryService.AddNewCategory(categoryDetailes);
                return RedirectToAction("GetCategories");
            }
            return RedirectToAction("GetCategories");

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
            if (songDetailes != null)
            {
                _songService.AddNewSong(songDetailes);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateSong(SongDetailes songDetailes)
        {
            if (songDetailes != null)
            {
               var result =  _songService.UpdateSong(songDetailes);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddNewSinger(SingerDetailes singerDetails)
        {
            if (singerDetails != null)
            {
                _singerService.AddNewSinger(singerDetails);
                return RedirectToAction("GetSingers");
            }
            return RedirectToAction("GetSingers");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      

        [HttpPost]
        public IActionResult DeleteSong(Guid songId) 
        {
            if (songId != null && songId != Guid.Empty)
            {
                _songService.DeleteSong(songId);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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
        public IActionResult UpdateCategory(CategoryDetailes categoryDetailes)
        {
            if (categoryDetailes != null)
            {
                _categoryService.UpdateCategory(categoryDetailes);
                return RedirectToAction("GetCategories");
            }
            return RedirectToAction("GetCategories");
        }

        public IActionResult UpdateSinger(SingerDetailes singerDetails)
        {
            if (singerDetails != null)
            {
                _singerService.UpdateSinger(singerDetails);
                return RedirectToAction("GetSingers");
            }
            return RedirectToAction("GetSingers");
        }
    }
}
