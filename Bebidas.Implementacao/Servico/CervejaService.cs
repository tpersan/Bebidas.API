using Bebidas.Implementacao.Dto;
using Bebidas.Implementacao.Repositorio.Inclusao;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bebidas.Implementacao.Servico
{
    public class CervejaServico : ICervejaServico
    {
        private readonly IInclusaoCerveja _inclusaoCerveja;

        public CervejaServico(IInclusaoCerveja inclusaoCerveja)
        {
            _inclusaoCerveja = inclusaoCerveja;
        }


        public void IncluirCerveja(CervejaDto cerveja)
        {
            _inclusaoCerveja.Inserir(cerveja);
            
        }


    }
}
