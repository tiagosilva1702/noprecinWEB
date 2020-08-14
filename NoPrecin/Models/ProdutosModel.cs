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
        public TipoProduto TipoProduto { get; set; }
        public string EmailProprietario { get; set; }
        [Display(Name = "Vendido")]
        public bool Vendido{ get; set; }
    }

    public enum TipoProduto
    {
        [Display(Name = "Imóveis")]
        Imoveis = 1,
        [Display(Name = "Autos Peças")]
        AutosPecas = 2,
        [Display(Name = "Para Sua Casa")]
        ParaSuaCasa = 3,
        [Display(Name = "Eletrônicos Celulares")]
        EletronicosCelulares = 4,
        [Display(Name = "Vagas Emprego")]
        VagasEmprego = 5,
        [Display(Name = "Serviços")]
        Servicos = 6,
        [Display(Name = "Músicas Hobbies")]
        MusicasHobbies = 7,
        [Display(Name = "Esportes Lazer")]
        EsportesLazer = 8,
        ModaBeleza = 9,
        [Display(Name = "Agro Indústria")]
        AgroIndustria = 10,
    }
}
