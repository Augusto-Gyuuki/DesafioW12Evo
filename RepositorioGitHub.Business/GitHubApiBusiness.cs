using RepositorioGitHub.Business.Contract;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Dominio.Interfaces;
using RepositorioGitHub.Infra.Contract;
using System.Linq;

namespace RepositorioGitHub.Business
{
    public class GitHubApiBusiness : IGitHubApiBusiness
    {
        private readonly IContextRepository _context;
        private readonly IGitHubApi _gitHubApi;
        public GitHubApiBusiness(IContextRepository context, IGitHubApi gitHubApi)
        {
            _context = context;
            _gitHubApi = gitHubApi;
        }

        public ActionResult<GitHubRepositoryViewModel> Get()
        {
            var repositories = _gitHubApi.GetRepositories("Augusto-Gyuuki");
            if (!repositories.IsValid)
                return null;

            return new ActionResult<GitHubRepositoryViewModel>()
            {
                IsValid = true,
                Results = repositories.Results.Select(x => new GitHubRepositoryViewModel
                {
                    Description = x.Description,
                    FullName = x.FullName,
                    Id = x.Id,
                    Language = x.Language,
                    Name = x.Name,
                    Owner = x.Owner,
                    UpdatedAt = x.UpdatedAt.DateTime,
                    Url = x.Url,
                    Homepage = x.Homepage
                }).ToList()
            };
        }

        public ActionResult<GitHubRepositoryViewModel> GetById(long id)
        {
            var repository = _gitHubApi.GetRepositoryById(id);
            if (!repository.IsValid)
                return null;

            return new ActionResult<GitHubRepositoryViewModel>()
            {
                IsValid = true,
                Result = new GitHubRepositoryViewModel
                {
                    Description = repository.Result.Description,
                    FullName = repository.Result.FullName,
                    Id = repository.Result.Id,
                    Language = repository.Result.Language,
                    Name = repository.Result.Name,
                    Owner = repository.Result.Owner,
                    UpdatedAt = repository.Result.UpdatedAt.DateTime,
                    Url = repository.Result.Url,
                    Homepage = repository.Result.Homepage
                }
            };
        }

        public ActionResult<RepositoryViewModel> GetByName(string name)
        {
            var repository = _gitHubApi.GetRepositoryByName(name);
            if (!repository.IsValid)
                return null;

            return new ActionResult<RepositoryViewModel>()
            {
                IsValid = true,
                Result = new RepositoryViewModel
                {
                    Repositories = repository.Result.Repositories,
                    TotalCount = repository.Result.TotalCount,
                }
            };
        }
    }
}
