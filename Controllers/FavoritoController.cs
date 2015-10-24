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
    /// <summary>
    /// Classe de controle que faz a interação da visão com os modelos
    /// </summary>
    public class FavoritoController : Controller
    {
        private Favorito favorito = new Favorito();

        //
        // GET: /Favorito/
        /// <summary> 
        /// Método que mostra a lista de favoritos
        /// </summary> 
        public ActionResult Index()
        {
            const int paginaTamanho = 30;

            int totalRegistros;

            List<FavoritoModels> favoritos = favorito.ListaFavoritos(out totalRegistros);

            PaginacaoFavoritoModels model = new PaginacaoFavoritoModels
            {
                paginaTamanho = paginaTamanho,
                favoritos = favoritos,
                totalRegistros = totalRegistros
            };

            return View(model); 
        }

        [HttpPost]
        /// <summary> 
        /// Método que insere um novo favorito
        /// </summary> 
        /// <param name="jsonRepositorioModels">Conteúdo em formato JSON do repositório.</param> 
        public string Inserir(string jsonRepositorioModels)
        {
            return favorito.Inserir(jsonRepositorioModels);
        }

        /// <summary> 
        /// Método que remove um favorito
        /// </summary> 
        /// <param name="jsonRepositorioModels">Conteúdo em formato JSON do repositório.</param> 
        [HttpPost]
        public string Remover(string jsonRepositorioModels)
        {
            return favorito.Remover(jsonRepositorioModels);
        }
    }
}