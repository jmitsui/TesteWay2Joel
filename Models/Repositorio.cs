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
    public class Repositorio
    {
        const int maxRegistros = 1000;
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

        public List<RepositorioModels> ListaMeusRepos(out int totalRegistros, int paginaTamanho, int paginaIndex)
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

        public List<RepositorioModels> ListaPesquisaRepos(string pesquisaString, out int totalRegistros, int paginaTamanho, int paginaIndex)
        {
            try
            {
                var url = "https://api.github.com/search/repositories?q=" + pesquisaString.Trim() + "&page=" + paginaIndex.ToString().Trim();
                PesquisaRepositorioModels pesquisaRepos = JsonConvert.DeserializeObject<PesquisaRepositorioModels>(GetJsonString(url));
                
                //GitHub Search API provides up to 1,000 results for each search.
                totalRegistros = (pesquisaRepos.total_count < maxRegistros) ? pesquisaRepos.total_count : maxRegistros;
                List<RepositorioModels> repos = pesquisaRepos.items;
                return repos;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro na listagem da Pesquisa de Repositórios: ", ex);
            }
        }

        public RepositorioModels RetornaRepos(string fullName)
        {
            try
            {
                var url = "https://api.github.com/repos/" + fullName;

                RepositorioModels repos = JsonConvert.DeserializeObject<RepositorioModels>(GetJsonString(url));

                List<ContributorModels> contributors = JsonConvert.DeserializeObject<List<ContributorModels>>(GetJsonString(repos.contributors_url));
                repos.contributors = contributors;

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