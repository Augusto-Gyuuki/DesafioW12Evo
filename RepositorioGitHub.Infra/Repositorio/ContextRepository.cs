using RepositorioGitHub.Dominio;
using RepositorioGitHub.Infra.Contract;
using RepositorioGitHub.Infra.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioGitHub.Infra.Repositorio
{
    public class ContextRepository : IContextRepository
    {
        private readonly SQLiteContext dbContext;

        public ContextRepository()
        {
            dbContext = new SQLiteContext();
        }

        public Favorite GetFavoriteByRepositoryId(long repositoryId)
        {
            return dbContext.Favorites.FirstOrDefault(x => x.RepositoryId.Equals(repositoryId));
        }

        public List<Favorite> GetAll()
        {
            return dbContext.Favorites.ToList();
        }

        public async Task<Favorite> InsertAsync(Favorite favorite)
        {
            dbContext.Favorites.Add(favorite);
            await dbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task<bool> RemoveAsync(long favoriteId)
        {
            var favorite = dbContext.Favorites.FirstOrDefault(x => x.Id.Equals(favoriteId));
            dbContext.Favorites.Remove(favorite);
            return (await dbContext.SaveChangesAsync()).Equals(1);
        }
    }
}
