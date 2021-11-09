using Newtonsoft.Json;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Dominio.Interfaces;
using RestSharp;
using System.Collections.Generic;

namespace RepositorioGitHub.Infra.ApiGitHub
{
    public class GitHubApi : IGitHubApi
    {
        public ActionResult<GitHubRepository> GetRepositories(string owner)
        {
            var client = new RestClient(string.Concat("https://api.github.com/users/", owner, "/repos"))
            {
                Timeout = -1,
            };

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //request.AddQueryParameter("direction", "asc");
            //request.AddQueryParameter("sort", "created");
            var response = client.Execute(request);

            var repositories = new ActionResult<GitHubRepository>();
            if (response.IsSuccessful)
            {
                repositories.Results = JsonConvert.DeserializeObject<IList<GitHubRepository>>(response.Content);
                repositories.IsValid = true;
            }
            else
            {
                repositories.IsValid = false;
            }
            return repositories;

        }

        public ActionResult<GitHubRepository> GetRepositoryById(long repositoryId)
        {
            var client = new RestClient($"https://api.github.com/repositories/{repositoryId}")
            {
                Timeout = -1,
            };

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //request.AddQueryParameter("direction", "asc");
            //request.AddQueryParameter("sort", "created");
            var response = client.Execute(request);

            var repository = new ActionResult<GitHubRepository>();
            if (response.IsSuccessful)
            {
                repository.Result = JsonConvert.DeserializeObject<GitHubRepository>(response.Content);
                repository.IsValid = true;
            }
            else
            {
                repository.IsValid = false;
            }
            return repository;
        }

        public ActionResult<RepositoryModel> GetRepositoryByName(string name)
        {
            var client = new RestClient("https://api.github.com/search/repositories")
            {
                Timeout = -1,
            };

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //request.AddQueryParameter("direction", "asc");
            //request.AddQueryParameter("sort", "created");
            request.AddQueryParameter("q", name);
            var response = client.Execute(request);

            var searchedRepositories = new ActionResult<RepositoryModel>();
            if (response.IsSuccessful)
            {
                searchedRepositories.Result = JsonConvert.DeserializeObject<RepositoryModel>(response.Content);
                searchedRepositories.IsValid = true;
            }
            else
            {
                searchedRepositories.IsValid = false;
            }
            return searchedRepositories;
        }
    }
}
