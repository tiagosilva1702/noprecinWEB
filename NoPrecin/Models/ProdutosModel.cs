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
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Imagem")]
        public string Imagem { get; set; }
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }
        [Display(Name = "Data Cadastro")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataCadastro { get; set; }
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
        [Display(Name = "Categória")]
        public Categoria Categoria { get; set; }
        public Guid Id_Usuario { get; set; }
        [Display(Name = "Vendido")]
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
