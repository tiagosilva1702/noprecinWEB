using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NoPrecin.Models
{
    public class Produtos
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }

    public enum Categoria
    {
        Imoveis,
        AutosPecas,
        ParaSuaCasa,
        EletronicosCelulares,
        VagasEmprego,
        Servicos,
        MusicasHobbies,
        EsportesLazer,
        ModaBeleza,
        AgroIndustria,
    }
}
