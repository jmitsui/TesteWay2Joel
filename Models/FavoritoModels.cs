using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteWay2Joel.Models
{
    /// <summary>
    /// Classe de modelo que representa um respositório favorito
    /// </summary>
    public class FavoritoModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        [DisplayName("Nome")]
        public string name { get; set; }
        public string full_name { get; set; }
        [DisplayName("Descrição")]
        public string description { get; set; }
        [DisplayName("Última atualização")]
        public DateTime updated_at { get; set; }
    }

    /// <summary>
    /// Classe responsável por inicializar a conexão com o banco de dados
    /// </summary>
    public class FavoritoDBContext : DbContext
    {
        public FavoritoDBContext() {
            Database.SetInitializer<FavoritoDBContext>(null);
        }

        public DbSet<FavoritoModels> Favoritos { get; set; }
    }
}