﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoPrecin.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string AcessToken { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }

    public enum Perfil
    {
        administrador,
        usuario
    }

    public class UsuarioLogado
    {
        public bool sucess { get; set; }
        public List<string> errors { get; set; }

        public Data data { get; set; }
    }

    public class Data
    {
        public string acessToken { get; set; }

        public userToken userToken { get; set; }
    }

    public class userToken
    {
        public Guid id { get; set; }
        public string email { get; set; }
    }

    public class EquipeNoPrecin
    {
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public String Imagem { get; set; }

        public EquipeNoPrecin(string nome, string descricao, string imagem)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Imagem = imagem;
        }
    }


}
