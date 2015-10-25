using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        /// Método que verificar se um favorito já está marcado 
        /// </summary> 
        /// <param name="id">Id do favorito, que é o mesmo Id do repositório.</param> 
        public bool ExisteFavorito(int id)
        {
            try
            {
                FavoritoModels favoritomodels = db.Favoritos.Find(id);
                return (favoritomodels != null);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro no verificação do favorito: ", ex);
            }
        }

        /// <summary> 
        /// Método que atualiza um favorito, inserindo se não existe ou removendo um existente
        /// </summary> 
        /// <param name="jsonRepositorioModels">Conteúdo em formato JSON do repositório.</param> 
        public string Atualizar(string jsonRepositorioModels)
        {
            try
            {
                string retorno = "";
                FavoritoModels favoritomodels = JsonConvert.DeserializeObject<FavoritoModels>(jsonRepositorioModels);

                if (db.Favoritos.Find(favoritomodels.id) == null)
                {
                    db.Favoritos.Add(favoritomodels);
                    retorno = "marcado";
                }
                else
                {
                    favoritomodels = db.Favoritos.Find(favoritomodels.id);
                    db.Favoritos.Remove(favoritomodels);
                    retorno = "desmarcado";
                }
                db.SaveChanges();

                return retorno;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro na inclusão de favoritos: ", ex);
            }
        }
    }
}