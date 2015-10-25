using System.Collections.Generic;

namespace TesteWay2Joel.Models
{
    /// <summary>
    /// Classe de modelo que contém as propriedades utlizadas na paginação, além da lista de favoritos
    /// </summary>
    public class PaginacaoFavoritoModels
    {
        public int paginaTamanho { get; set; }
        public int paginaIndex { get; set; }
        public List<FavoritoModels> favoritos { get; set; }
        public int totalRegistros { get; set; }
    }
}