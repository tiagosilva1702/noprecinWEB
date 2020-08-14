using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoPrecin.Models
{
    public class Vendas
    {
        public Guid ProdutoId { get; set; }
        public DateTime Data { get; set; }
        public String EmailComprador { get; set; }
    }
}
