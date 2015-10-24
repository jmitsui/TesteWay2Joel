using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteWay2Joel.Models
{
    public class PaginacaoFavoritoModels
    {
        public int paginaTamanho { get; set; }
        public int paginaIndex { get; set; }
        public List<FavoritoModels> favoritos { get; set; }
        public int totalRegistros { get; set; }
    }
}