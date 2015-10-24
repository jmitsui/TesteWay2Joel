using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TesteWay2Joel.Models
{
    /// <summary>
    /// Classe que provê funções relacionadas ao modelo Favorito, como listar, inserir, remover
    /// </summary>
    public class Favorito
    {
        private FavoritoDBContext db = new FavoritoDBContext();

        /// <summary> 
        /// Método que retorna uma lista de favoritos. 
        /// </summary> 
        /// <param name="totalRegistros">Total de registros.</param> 
        public List<FavoritoModels> ListaFavoritos(out int totalRegistros)
        {
            try
            {
                List<FavoritoModels> favoritos = db.Favoritos.ToList();

                totalRegistros = favoritos.Count();
                return favoritos;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro na listagem de Favoritos: ", ex);
            }

        }

        /// <summary> 
        /// Método que retorna um favorito. 
        /// </summary> 
        /// <param name="id">Id do favorito, que é o mesmo Id do repositório.</param> 
        public FavoritoModels RetornaFavorito(int id)
        {
            try
            {
                FavoritoModels favoritomodels = db.Favoritos.Find(id);
                return favoritomodels;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro no retorno do favorito: ", ex);
            }
        }

        /// <summary> 
        /// Método que insere um novo favorito. 
        /// </summary> 
        /// <param name="jsonRepositorioModels">Conteúdo em formato JSON do repositório.</param> 
        public string Inserir(string jsonRepositorioModels)
        {
            try
            {
                FavoritoModels favoritomodels = JsonConvert.DeserializeObject<FavoritoModels>(jsonRepositorioModels);
                db.Favoritos.Add(favoritomodels);
                db.SaveChanges();
                return "marcado";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro na inclusão de favoritos: ", ex);
            }
        }

        /// <summary> 
        /// Método que remove um favorito. 
        /// </summary> 
        /// <param name="jsonRepositorioModels">Conteúdo em formato JSON do repositório.</param> 
        public string Remover(string jsonRepositorioModels)
        {
            try
            {
                FavoritoModels favoritomodels = JsonConvert.DeserializeObject<FavoritoModels>(jsonRepositorioModels);
                favoritomodels = db.Favoritos.Find(favoritomodels.id);

                if (favoritomodels == null)
                {
                    return "erroNotFound";
                }

                db.Favoritos.Remove(favoritomodels);
                db.SaveChanges();

                return "desmarcado";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro na exclusão de favoritos: ", ex);
            }
        }
    }
}