using RepositorioGitHub.Business.Contract;
using RepositorioGitHub.Dominio;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RepositorioGitHub.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGitHubApiBusiness _gitHubApiBusiness;
        private readonly IFavoriteBusiness _favoriteBusiness;

        public HomeController(IGitHubApiBusiness business, IFavoriteBusiness favoriteBusiness)
        {
            _gitHubApiBusiness = business;
            _favoriteBusiness = favoriteBusiness;
        }
        public ActionResult Index()
        {

            var model = _gitHubApiBusiness.Get();
            TempData.Clear();
            if (model.IsValid)
            {
                TempData["success"] = model.Message;
            }
            else
            {
                TempData["warning"] = model.Message;
            }

            return View(model);
        }

        public ActionResult Details(long id)
        {

            var model = _gitHubApiBusiness.GetById(id);
            if (model.IsValid)
            {
                TempData["success"] = model.Message;
            }
            else
            {
                TempData["warning"] = model.Message;
            }

            ViewBag.PreviousAction = "Index";
            return View(nameof(DetailsRepository), model);
        }

        [HttpPost]
        public ActionResult GetRepositorie(ActionResult<RepositoryViewModel> view)
        {
            ActionResult<RepositoryViewModel> model = new ActionResult<RepositoryViewModel>();
            if (string.IsNullOrEmpty(view.Result?.Name))
            {
                model.IsValid = false;
                model.Message = "O Campo Nome Repositório tem que ser Presenchido";
                TempData["warning"] = model.Message;
                return View(model);
            }

            model = _gitHubApiBusiness.GetByName(view.Result.Name);

            if (model.IsValid)
            {
                TempData["success"] = model.Message;
            }
            else
            {
                TempData["warning"] = model.Message;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult GetRepositorie()
        {
            var model = new ActionResult<RepositoryViewModel>
            {
                Result = new RepositoryViewModel()
            };
            return View(model);
        }

        public ActionResult DetailsRepository(long id)
        {
            ActionResult<GitHubRepositoryViewModel> model;

            if (id.Equals(0))
            {
                return RedirectToAction("GetRepositorie");
            }
            else
            {
                model = _gitHubApiBusiness.GetById(id);

                if (model.IsValid)
                {
                    TempData["success"] = model.Message;
                }
                else
                {
                    TempData["warning"] = model.Message;
                }
            }
            ViewBag.PreviousAction = "GetRepositorie";
            return View(model);
        }

        public ActionResult Favorite()
        {

            ActionResult<FavoriteViewModel> model;

            var response = _favoriteBusiness.GetFavoriteRepository();

            model = response;

            if (model.IsValid)
            {
                TempData["success"] = model.Message;
            }
            else
            {
                TempData["warning"] = model.Message;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> FavoriteSave(FavoriteViewModel favoriteViewModel)
        {
            ActionResult<FavoriteViewModel> model = new ActionResult<FavoriteViewModel>();

            if (string.IsNullOrEmpty(favoriteViewModel.Owner) && string.IsNullOrEmpty(favoriteViewModel.Name)
                && string.IsNullOrEmpty(favoriteViewModel.UpdateLast) && string.IsNullOrEmpty(favoriteViewModel.Description))
            {
                model.IsValid = false;
                model.Message = "Não foi possivel realizar esta operação";

                return Json(new
                {
                    Data = model
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                favoriteViewModel.ConvertedUpdateLast = DateTime.Parse(favoriteViewModel.UpdateLast);

                model = await _favoriteBusiness.SaveFavoriteRepository(favoriteViewModel);

                if (model.IsValid)
                {
                    TempData["success"] = model.Message;
                }
                else
                {
                    TempData["warning"] = model.Message;
                }

                return Json(new
                {
                    Data = model
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        //public ActionResult FavoriteSave(string owner, string name, string language, string lastUpdat, string description)
        public async Task<ActionResult> RemoveFavorite(long favoriteId)
        {
            ActionResult<FavoriteViewModel> model = new ActionResult<FavoriteViewModel>();

            if (favoriteId.Equals(0))
            {
                model.IsValid = false;
                model.Message = "Não foi possivel realizar esta operação";

                return Json(new
                {
                    Data = model
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model = await _favoriteBusiness.RemoveFavoriteRepository(favoriteId);

                if (model.IsValid)
                {
                    TempData["success"] = model.Message;
                }
                else
                {
                    TempData["warning"] = model.Message;
                }

                return Json(new
                {
                    Data = model
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}