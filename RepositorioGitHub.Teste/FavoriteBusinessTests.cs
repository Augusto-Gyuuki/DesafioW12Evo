using FluentAssertions;
using Moq;
using RepositorioGitHub.Business;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Infra.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FavoriteTest
{
    public class FavoriteBusinessTests
    {
        private readonly FavoriteBusiness _favoriteBusiness;
        private readonly Mock<IContextRepository> _contextRepository = new();

        public FavoriteBusinessTests()
        {
            _favoriteBusiness = new FavoriteBusiness(_contextRepository.Object);
        }

        [Fact]
        public void GetFavoriteRepository_ShouldNotBeNull_WhenThereIsZeroFavorites()
        {
            //Arrange
            var favoriteList = new List<Favorite>();
            _contextRepository.Setup(x => x.GetAll()).Returns(favoriteList);

            //Act
            var repositories = _favoriteBusiness.GetFavoriteRepository();

            //Assert
            repositories.IsValid.Should().BeFalse();
            repositories.Message.Should().Be("Nenhum repositorio salvo como favorito");
            repositories.Results.Should().BeNull();
            repositories.Should().NotBeNull();
        }

        [Fact]
        public void GetFavoriteRepository_ShouldReturnAll_WhenThereIsFavorites()
        {
            //Arrange
            var favoriteList = new List<Favorite>();
            favoriteList.Add(new Favorite());
            favoriteList.Add(new Favorite());
            _contextRepository.Setup(x => x.GetAll()).Returns(favoriteList);
            
            //Act
            var repositories = _favoriteBusiness.GetFavoriteRepository();

            //Assert
            repositories.IsValid.Should().BeTrue();
            repositories.Results.Should().NotBeNullOrEmpty();
            repositories.Results.Count.Should().Be(2);
        }

        [Fact]
        public async Task SaveFavoriteRepository_ShouldReturnError_WhenRepositoryAlreadyFavorite()
        {
            //Arrange
            var favoriteViewModel = new FavoriteViewModel()
            {
                Description = "Test Repository Description",
                Id = 999999999999,
                Language = "C#",
                Name = "Test Repository",
                Owner = "Augusto",
                UpdateLast = "09/11/2021 13:00:00"
            };
            _contextRepository.Setup(x => x.GetFavoriteByRepositoryId(It.IsAny<long>())).Returns(() => new Favorite());

            //Act
            var ActionResultFavoriteViewModel = await _favoriteBusiness.SaveFavoriteRepository(favoriteViewModel);

            //Assert
            ActionResultFavoriteViewModel.IsValid.Should().BeFalse();
            ActionResultFavoriteViewModel.Message.Should().Be("Esse repositório já está salvo como favorito.");
            ActionResultFavoriteViewModel.Should().NotBeNull();
        }
    }
}
