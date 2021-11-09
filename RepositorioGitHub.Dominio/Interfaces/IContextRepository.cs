using RepositorioGitHub.Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositorioGitHub.Infra.Contract
{
    public interface IContextRepository
    {
        Task<Favorite> InsertAsync(Favorite favorite);
        Task<bool> RemoveAsync(long favoriteId);
        List<Favorite> GetAll();
        Favorite GetFavoriteByRepositoryId(long repositoryId);

    }
}
