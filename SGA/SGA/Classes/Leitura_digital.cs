using System;
using System.Collections.Generic;
using System.Text;

namespace SGA.Model
{
        public class Leitura_digital
        {
            #region Atributos
            private int _idLeituraDigital;
            private Pessoa _pessoa;
            private int _dedo;
            private int _qualidade;
            private byte[] _tptData;
            #endregion

            #region Propriedades

            public int IdLeituraDigital
            {
                get { return _idLeituraDigital; }
                set { _idLeituraDigital = value; }
            }
            public Pessoa Pessoa
            {
                get { return _pessoa; }
                set {  _pessoa = value; }
            }
            public int Dedo
            {
                get { return _dedo; }
                set { _dedo = value; }
            }
            public int Qualidade
            {
                get { return _qualidade; }
                set { _qualidade = value; }
            }
            public byte[] TptData
            {
                get { return _tptData; }
                set { _tptData = value; }
            }
            public bool Persisted { get; set; }

            #endregion

            #region Construtores

            public Leitura_digital()
            {
                Persisted = false;
            }

            public Leitura_digital(int IdLeituraDigital)
            {
                this._idLeituraDigital = IdLeituraDigital;
                Persisted = true;
            }
            #endregion
        }
}
