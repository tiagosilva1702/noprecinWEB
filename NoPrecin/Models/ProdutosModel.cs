using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public Categoria Categoria { get; set; }
        public Guid Id_Usuario { get; set; }
        public bool Vendido{ get; set; }
    }

    public enum Categoria
    {
        [Display(Name = "Imóveis")]
        Imoveis = 0,
        [Display(Name = "Autos Peças")]
        AutosPecas = 1,
        [Display(Name = "Para Sua Casa")]
        ParaSuaCasa = 2,
        [Display(Name = "Eletrônicos Celulares")]
        EletronicosCelulares = 3,
        [Display(Name = "Vagas Emprego")]
        VagasEmprego = 4,
        [Display(Name = "Serviços")]
        Servicos = 5,
        [Display(Name = "Músicas Hobbies")]
        MusicasHobbies = 6,
        [Display(Name = "Esportes Lazer")]
        EsportesLazer = 7,
        ModaBeleza = 8,
        [Display(Name = "Agro Indústria")]
        AgroIndustria = 9,
    }
}
