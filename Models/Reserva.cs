using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto___Sistema_de_Hospedagem_de_um_Hotel.Models
{
    public class Reserva
    {
        public List<Pessoa> Hospedes { get; set; }
        public Suite Suite { get; set; }
        public int DiasReservados { get; set; }


        //-----------------------------------------------------------------------------
        public void CadastrarHospedes(List<Pessoa> hospedes)
        {

            Hospedes = hospedes;

        }

        //-----------------------------------------------------------------------------
        public void CadastrarSuite(Suite suite)
        {

        }

        //-----------------------------------------------------------------------------
        public int ObterQuantidadeHospedes()
        {

            return 0;
        }

        //-----------------------------------------------------------------------------
        public decimal CalcularValorDiaria()
        {
            return 0.0M;
        }

        //-----------------------------------------------------------------------------

        public StringBuilder Apresentacao ()
        {
            StringBuilder apresentaHospedes = new StringBuilder();

             apresentaHospedes.AppendLine("<<<Hospedes>>");
            foreach (var item in Hospedes)
            {
                apresentaHospedes.AppendLine($"Nome: {item.Nome} {item.Sobrenome}");
                
            }


            return apresentaHospedes;
        }

    }
}