using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Projeto___Sistema_de_Hospedagem_de_um_Hotel.Models
{
    public class Reserva
    {
        private const string _arquivoDeReservas = "Files/Reservas.json";
        public int Id { get; set; }
        public List<Pessoa> Hospedes { get; set; }
        public Suite Suite { get; set; }
        public int DiasReservados { get; set; }


        //-----------------------------------------------------------------------------
        /// <summary>
        /// Cadastrs a lista de hospedes
        /// </summary>
        /// <param name="hospedes">Lista de hospedes</param>
        /// <exception cref="Exception">Retornar se a a quantidade de hospede for maior que a capacidade do quarto</exception>
        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            bool capacidadeMaior = Suite.Capacidade >= hospedes.Count;


            if (capacidadeMaior)
            {
                
                Hospedes = hospedes;
            }
            else
            {
                throw new Exception("Quantidade de hospedes é maior que capacidade do quarto selecionado");
            }


        }

        //-----------------------------------------------------------------------------
        public void CadastrarSuite(Suite suite)
        {
            Suite = suite;
        }

        //-----------------------------------------------------------------------------
        public int ObterQuantidadeHospedes()
        {

            return Hospedes.Count;
        }

        //-----------------------------------------------------------------------------
        public decimal CalcularValorDiaria()
        {
            decimal valor = DiasReservados * Suite.ValorDiaria;


            if (DiasReservados>9)
            {
                valor = valor - (valor * 0.1M);
            }

            return valor;
        }

        //-----------------------------------------------------------------------------
        public (int Id, bool Sucesso) CadastrarReserva(Reserva reserva)
        {
            //Declaração de variaveis
            List< Reserva> reservas = new List<Reserva>();
            int idAux;
            //string conteudoDoArquivo;


            //Buscar a lista atual e atualiza com a nova reserva
            //conteudoDoArquivo =  File.ReadAllText(_arquivoDeReservas);
            reservas = ObterTodasAsReservas();


            if(reservas != null)
            {
                //Cria um Id Auxiliar e seta na nova reserva
                idAux =  reservas.Last().Id + 1;
                reserva.Id = idAux;

            }
            else
            {
                idAux = 1;
                reserva.Id = idAux;
                reservas = new List<Reserva>();
            }


            // Adicona a reserva atual na lista
            reservas.Add(reserva);


            // Transforma em json e grava novamente
            // string serializado = JsonConvert.SerializeObject(reservas, Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText(_arquivoDeReservas, serializado);
            AtualizarListaReserva(reservas);


            return (Id:idAux, Sucesso:true);


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
        //--------------------------------------------------------------------------

        public static StringBuilder ListarReservas()
        {
            List<Reserva> reservas = ObterTodasAsReservas();
            StringBuilder apresentaHospedes = new StringBuilder();

    
            foreach (var reserva in reservas)
            {   
                apresentaHospedes.AppendLine($"{reserva.Id} - RESERVA\n");
                foreach (var item in reserva.Hospedes)
                {
                    apresentaHospedes.AppendLine($"Nome: {item.Nome} {item.Sobrenome}");
                }
                apresentaHospedes.AppendLine("\n<< QUARTO >>");
                apresentaHospedes.AppendLine($"Suite Nº {reserva.Suite.Numero} - {reserva.Suite.TipoSuite}");
                apresentaHospedes.AppendLine($"Dias Reservados {reserva.DiasReservados}\n\n");

            }

            return apresentaHospedes;
        }
        //--------------------------------------------------------------------------
        public static List<Reserva> ObterTodasAsReservas()
        {
            List<Reserva> reservas = JsonConvert.DeserializeObject<List<Reserva>>(File.ReadAllText(_arquivoDeReservas));
            if(reservas != null)
            {
                return reservas;
            }
            else
            {
                return new List<Reserva>();
            }

        }
        //--------------------------------------------------------------------------
        public static Reserva ObterSuiteSelecionada(int id)
        {

            List<Reserva> todasReservas =  ObterTodasAsReservas();

            Reserva reservasFiltradas = (from r in todasReservas where r.Id == id select r).FirstOrDefault(); 


            return new Reserva()
            {
                Id = reservasFiltradas.Id,
                Hospedes = reservasFiltradas.Hospedes,
                Suite = reservasFiltradas.Suite,
                DiasReservados = reservasFiltradas.DiasReservados

            };
        }

         //--------------------------------------------------------------------------
         public void removerReserva()
         {
            List<Reserva> todasReservas =  ObterTodasAsReservas();
            int index = todasReservas.FindIndex(i => i.Id == this.Id);
            todasReservas.RemoveAt(index);
            AtualizarListaReserva(todasReservas);
             Console.Read();
         }

         public void CheckOut()
         {
           
            int totaDeHospedes = Hospedes.Count;
            string valorTotal = CalcularValorDiaria().ToString("R$ 0.00");
            string dataSaida = DateTime.Now.ToShortDateString();

            string ListaDeHopedes = "";
            foreach (var item in Hospedes)
            {
                
                ListaDeHopedes += $"{item.Nome} {item.Sobrenome}\n";
            }

            Console.Clear();
            System.Console.WriteLine($"Hospede(s):\n {ListaDeHopedes}\n Valor total a pagar: {valorTotal}\nData da Saída: {dataSaida}");


            Console.ReadLine();
            removerReserva();

         }
          //--------------------------------------------------------------------------
        private void AtualizarListaReserva(List<Reserva> reservas)
        {
            string serializado = JsonConvert.SerializeObject(reservas, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_arquivoDeReservas, serializado);
        }
        //--------------------------------------------------------------------------
    }
}