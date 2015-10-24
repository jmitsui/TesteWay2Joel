using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteWay2Joel.Models;

namespace TesteWay2Joel.Controllers
{
    /// <summary>
    /// Classe de controle que faz a interação da visão com os modelos
    /// </summary>
    public class RepositorioController : Controller
    {
        private Repositorio repositorio = new Repositorio();

        /// <summary> 
        /// Método que mostra os meus repositórios (jmitsui)
        /// </summary> 
        public ActionResult MeusRepositorios()
        {
            // Constante do tamanho máximo da paginação. Default API GitHub V3.
            const int paginaTamanho = 30;

            int totalRegistros;

            List<RepositorioModels> repos = repositorio.ListaMeusRepos(out totalRegistros);

            PaginacaoRepositorioModels model = new PaginacaoRepositorioModels
            {
                paginaTamanho = paginaTamanho,
                repositorios = repos,
                totalRegistros = totalRegistros
            };

            return View(model); 
        }

        /// <summary> 
        /// Método que mostra os repositórios da pesquisa
        /// </summary> 
        /// <param name="pesquisaString">Palavra chave da pesquisa.</param> 
        /// <param name="page">Indice da páginação para consulta na API GitHub.</param> 
        public ActionResult Pesquisa(string pesquisaString, int page = 1)
        {

            // Constante do tamanho máximo da paginação. Default API GitHub V3.
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

        /// <summary> 
        /// Método que retorna os detalhes de um repositório específico. 
        /// </summary> 
        /// <param name="full_name">Nome completo do repositório que é único, [usuário]/[nome do repositório].</param> 
        public ActionResult Detalhes(string full_name)
        {
            RepositorioModels repos = repositorio.RetornaRepos(full_name);
            
            return View(repos);
        }

    }
}
