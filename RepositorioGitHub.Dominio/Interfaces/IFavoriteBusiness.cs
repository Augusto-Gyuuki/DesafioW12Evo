
using RepositorioGitHub.Dominio;
using System.Threading.Tasks;

namespace RepositorioGitHub.Business.Contract
{
    public interface IFavoriteBusiness
    {
        ActionResult<FavoriteViewModel> GetFavoriteRepository();
        Task<ActionResult<FavoriteViewModel>> SaveFavoriteRepository(FavoriteViewModel view);
        Task<ActionResult<FavoriteViewModel>> RemoveFavoriteRepository(long favoriteId);
   }
}
