using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteWay2Joel.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace TesteWay2Joel.Models
{
    /// <summary>
    /// Classe que provê funções relacionadas ao modelo Repositorio, como listar e retornar um repositório específico
    /// </summary>
    public class Repositorio
    {
        /// <summary>
        /// Constante que define a quantidade máxima de registros de uma pesquisa de repositórios. 
        /// Referência: https://developer.github.com/v3/search/
        /// </summary>
        const int maxRegistros = 1000;

        /// <summary> 
        /// Método que retorna uma string em formato JSON a partir de uma requisição web. 
        /// </summary> 
        /// <param name="url">Endereço da API</param> 
        private static string GetJsonString(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.UserAgent = "Foo";
                request.Accept = "application/json";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string jsonString = reader.ReadToEnd();
                        return jsonString;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro na chamada da API GitHub: ", ex);
            }
        }

        /// <summary> 
        /// Método que retorna uma lista de repositórios do usuário GitHub "jmitsui". 
        /// </summary> 
        /// <param name="totalRegistros">Total de registros.</param> 
        public List<RepositorioModels> ListaMeusRepos(out int totalRegistros)
        {
            try
            {
                var url = "https://api.github.com/users/jmitsui/repos";
                List<RepositorioModels> repos = JsonConvert.DeserializeObject<List<RepositorioModels>>(GetJsonString(url));

                totalRegistros = repos.Count();
                return repos;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro na listagem de Meus Repositórios: ", ex);
            }

        }

        /// <summary> 
        /// Método que retorna uma lista de repositórios da pesquisa. 
        /// </summary> 
        /// <param name="pesquisaString">Palavra chave da pesquisa.</param> 
        /// <param name="totalRegistros">Total de registros.</param> 
        /// <param name="paginaTamanho">Tamanho da paginação</param> 
        /// <param name="paginaIndex">Indice da paginação</param> 
        public List<RepositorioModels> ListaPesquisaRepos(string pesquisaString, out int totalRegistros, int paginaTamanho, int paginaIndex)
        {
            try
            {
                var url = "https://api.github.com/search/repositories?q=" + pesquisaString.Trim() + "&page=" + paginaIndex.ToString().Trim();
                PesquisaRepositorioModels pesquisaRepos = JsonConvert.DeserializeObject<PesquisaRepositorioModels>(GetJsonString(url));
                
                //GitHub Search API provides up to 1,000 results for each search.
                //Apesar do resultado da pesquisa de repositório da API informar 
                //a quantidade total de repositórios encontrados maior que 1000, 
                //a lista de fato só retorna no máximo 1000. Para poder paginar
                //corretamente a Webgrid, este valor é verificado.
                totalRegistros = (pesquisaRepos.total_count < maxRegistros) ? pesquisaRepos.total_count : maxRegistros;
                List<RepositorioModels> repos = pesquisaRepos.items;
                return repos;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro na listagem da Pesquisa de Repositórios: ", ex);
            }
        }

        /// <summary> 
        /// Método que retorna os detalhes de um repositório específico. 
        /// </summary> 
        /// <param name="fullName">Nome completo do repositório que é único, [usuário]/[nome do repositório].</param> 
        public RepositorioModels RetornaRepos(string fullName)
        {
            try
            {
                var url = "https://api.github.com/repos/" + fullName;

                RepositorioModels repos = JsonConvert.DeserializeObject<RepositorioModels>(GetJsonString(url));

                //Lista os contribuidores do repositório
                List<ContributorModels> contributors = JsonConvert.DeserializeObject<List<ContributorModels>>(GetJsonString(repos.contributors_url));
                repos.contributors = contributors;

                //Verifica se o repositório está marcado como favorito
                Favorito favorito = new Favorito();
                FavoritoModels favoritomodels = favorito.RetornaFavorito(repos.id);
                repos.is_favorite = (favoritomodels != null);

                return repos;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro no retorno dos Detalhes do Repositório: ", ex);
            }

        }
    }
}