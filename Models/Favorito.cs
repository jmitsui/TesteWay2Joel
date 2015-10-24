using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TesteWay2Joel.Models
{
    public class Favorito
    {
        private FavoritoDBContext db = new FavoritoDBContext();

        public List<FavoritoModels> ListaFavoritos(out int totalRegistros, int paginaTamanho, int paginaIndex)
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