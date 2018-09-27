using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SGA.Model
{
    public class Pessoa
    {
        #region Atributos
        private int _idAluno;
        private string _nomeAluno;
        private string _email;
        private string _cpf;
        private string _rg;
        private DateTime _dataNascimento;
        private string _telefone1;
        private string _telefone2;
        private string _cep;
        private string _rua;
        private int _numero;
        private string _bairro;
        private string _cidade;
        private string _estado;
        private string _complemento;
        private DateTime _dataVencimento;
        private string _foto;

        #endregion

        #region Propriedades
        public int IdAluno
        {
            get { return _idAluno; }
            set { _idAluno = value; }
        }
        public string NomeAluno
        {
            get { return _nomeAluno; }
            set { _nomeAluno = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string CPF
        {
            get { return _cpf; }
            set { _cpf = value; }
        }
        public string Rg
        {
            get { return _rg; }
            set { _rg = value; }
        }
        public DateTime DataNascimento
        {
            get { return _dataNascimento; }
            set { _dataNascimento = value; }
        }
        public string Telefone1
        {
            get { return _telefone1; }
            set { _telefone1 = value; }
        }
        public string Telefone2
        {
            get { return _telefone2; }
            set { _telefone2 = value; }
        }
        public string Cep
        {
            get { return _cep; }
            set { _cep = value; }
        }
        public string Rua
        {
            get { return _rua; }
            set { _rua = value; }
        }
        public int Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        public string Bairro
        {
            get { return _bairro; }
            set { _bairro = value; }
        }
        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        public string Complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }
        public DateTime DataVencimento
        {
            get { return _dataVencimento; }
            set { _dataVencimento = value; }
        }
        public string Foto
        {
            get { return _foto; }
            set { _foto = value; }
        }

        public bool Persisted { get; set; }
        #endregion

        #region Construtores

        public Pessoa()
        {
            Persisted = false;
        }

        public Pessoa(int IdAluno)
        {
            this._idAluno = IdAluno;
            Persisted = true;
        }
        #endregion
    }
}
