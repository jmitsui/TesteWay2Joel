using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteWay2Joel.Models;

namespace TesteWay2Joel.Controllers
{
    public class FavoritoController : Controller
    {
        private Favorito favorito = new Favorito();

        //
        // GET: /Favorito/
        public ActionResult Index(int page = 1)
        {
            const int paginaTamanho = 30;

            int totalRegistros;

            List<FavoritoModels> favoritos = favorito.ListaFavoritos(out totalRegistros, paginaTamanho: paginaTamanho, paginaIndex: page);

            PaginacaoFavoritoModels model = new PaginacaoFavoritoModels
            {
                paginaTamanho = paginaTamanho,
                paginaIndex = page,
                favoritos = favoritos,
                totalRegistros = totalRegistros
            };

            return View(model); 
        }

        [HttpPost]
        public string Inserir(string jsonRepositorioModels)
        {
            return favorito.Inserir(jsonRepositorioModels);
        }

        [HttpPost]
        public string Remover(string jsonRepositorioModels)
        {
            return favorito.Remover(jsonRepositorioModels);
        }
    }
}