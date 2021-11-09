using System;

namespace RepositorioGitHub.Dominio
{
    public class FavoriteViewModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string UpdateLast { get; set; }
        public DateTime ConvertedUpdateLast { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
    }
}
