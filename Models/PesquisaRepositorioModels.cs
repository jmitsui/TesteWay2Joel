﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteWay2Joel.Models
{
    /// <summary>
    /// Classe de modelo que representa uma pesquisa de repositórios
    /// </summary>
    public class PesquisaRepositorioModels
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public List<RepositorioModels> items { get; set; }
    }
}