namespace RepositorioGitHub.Dominio.Interfaces
{
    public interface IGitHubApi
    {
        ActionResult<GitHubRepository> GetRepositories(string owner);
        ActionResult<GitHubRepository> GetRepositoryById(long repositoryId);
        ActionResult<RepositoryModel> GetRepositoryByName(string name);     
    }
}
