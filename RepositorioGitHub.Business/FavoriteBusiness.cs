using RepositorioGitHub.Business.Contract;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Infra.Contract;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioGitHub.Business
{
    public class FavoriteBusiness : IFavoriteBusiness
    {
        private readonly IContextRepository _context;
        public FavoriteBusiness(IContextRepository context)
        {
            _context = context;
        }

        public ActionResult<FavoriteViewModel> GetFavoriteRepository()
        {
            var favorites = _context.GetAll();
            if (favorites.Count.Equals(0))
            {
                return new ActionResult<FavoriteViewModel>()
                {
                    IsValid = false,
                    Message = "Nenhum repositorio salvo como favorito",
                };
            }

            return new ActionResult<FavoriteViewModel>()
            {
                IsValid = true,
                Results = favorites.Select(x => new FavoriteViewModel
                {
                    UpdateLast = x.UpdateLast.ToString(),
                    Description = x.Description,
                    Id = x.Id,
                    Language = x.Language,
                    Name = x.Name,
                    Owner = x.Owner,
                }).ToList(),
            };
        }

        public async Task<ActionResult<FavoriteViewModel>> SaveFavoriteRepository(FavoriteViewModel view)
        {
            try
            {
                var favorite = new Favorite
                {
                    Description = view.Description,
                    RepositoryId = view.Id,
                    Language = view.Language,
                    Name = view.Name,
                    Owner = view.Owner,
                    UpdateLast = view.ConvertedUpdateLast,
                };
                if (_context.GetFavoriteByRepositoryId(favorite.RepositoryId) != null)
                {
                    return new ActionResult<FavoriteViewModel>
                    {
                        IsValid = false,
                        Message = "Esse repositório já está salvo como favorito."
                    };
                }
                await _context.InsertAsync(favorite);
                if (favorite.Id.Equals(0))
                {
                    return new ActionResult<FavoriteViewModel>
                    {
                        IsValid = false,
                        Message = "Não foi possivel realizar esta operação",
                    };
                };


                return new ActionResult<FavoriteViewModel>
                {
                    IsValid = true,
                    Message = "Repositório adicionado aos favoritos."
                };
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        public async Task<ActionResult<FavoriteViewModel>> RemoveFavoriteRepository(long favoriteId)
        {
            if (!await _context.RemoveAsync(favoriteId))
            {
                return new ActionResult<FavoriteViewModel>
                {
                    IsValid = false,
                    Message = "Não foi possivel realizar esta operação",
                };
            };


            return new ActionResult<FavoriteViewModel>
            {
                IsValid = true,
                Message = "Repositório removido dos favoritos."
            };
        }
    }
}
