using System;

namespace RepositorioGitHub.Dominio
{
    public class Favorite
    {
        public long Id { get; set; }
        public long RepositoryId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public DateTime UpdateLast { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
    }
}
