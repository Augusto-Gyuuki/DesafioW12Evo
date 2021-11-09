
using RepositorioGitHub.Dominio;

namespace RepositorioGitHub.Business.Contract
{
    public interface IGitHubApiBusiness
   {
        ActionResult<GitHubRepositoryViewModel> Get();
        ActionResult<RepositoryViewModel> GetByName(string name);
        ActionResult<GitHubRepositoryViewModel> GetById(long id);
   }
}
