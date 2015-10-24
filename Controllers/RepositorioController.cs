using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteWay2Joel.Models;

namespace TesteWay2Joel.Controllers
{
    public class RepositorioController : Controller
    {
        private Repositorio repositorio = new Repositorio();

        public ActionResult MeusRepositorios(int page = 1)
        {
            const int paginaTamanho = 30;

            int totalRegistros;

            List<RepositorioModels> repos = repositorio.ListaMeusRepos(out totalRegistros, paginaTamanho: paginaTamanho, paginaIndex: page);

            PaginacaoRepositorioModels model = new PaginacaoRepositorioModels
            {
                paginaTamanho = paginaTamanho,
                paginaIndex = page,
                repositorios = repos,
                totalRegistros = totalRegistros
            };

            return View(model); 
        }

        public ActionResult Pesquisa(string pesquisaString, int page = 1)
        {
            const int paginaTamanho = 30;

            int totalRegistros;

            List<RepositorioModels> repos = repositorio.ListaPesquisaRepos(pesquisaString, out totalRegistros, paginaTamanho: paginaTamanho, paginaIndex: page);

            PaginacaoRepositorioModels model = new PaginacaoRepositorioModels
            {
                paginaTamanho = paginaTamanho,
                paginaIndex = page,
                repositorios = repos,
                totalRegistros = totalRegistros
            };

            return View(model);
        }

        public ActionResult Detalhes(string full_name)
        {
            RepositorioModels repos = repositorio.RetornaRepos(full_name);
            
            return View(repos);
        }

    }
}
