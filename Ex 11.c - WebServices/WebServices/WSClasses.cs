using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WebServices.WSClasses
{
    public class ConsultaNIF
    {
        public bool existe;
        public string codigo;
        public string nome;
    }

    public class Cliente
    {
        public string cliente;
        public string nome;
        public string nif;
        public string morada1;
        public string morada2;
        public string localidade;
        public string codPostal;
    }

    public class Encomenda
    {
        public string tipodoc;
        public string cliente;
        public List<LinhaEncomenda> linhas;
    }

    public class LinhaEncomenda
    {
        public string artigo;
        public double quantidade;
        public double precoUnitario;
    }

    public class EncomendaResultado 
    {
        public string referencia;
        public bool erro;
        public string descricaoErro;
    }

    public class Authentication : SoapHeader
    {
        public string User;
        public string Password;
    }

}