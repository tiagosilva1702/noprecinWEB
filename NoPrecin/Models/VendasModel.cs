using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NoPrecin.Models
{
    public class Vendas
    {
        public Guid ProdutoId { get; set; }
        public DateTime Data { get; set; }
        public String EmailComprador { get; set; }
        public Produtos Produto { get; set; }

    }
}
