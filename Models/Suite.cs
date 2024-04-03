using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Projeto___Sistema_de_Hospedagem_de_um_Hotel.Models
{
    public class Suite
    {
        private static string _localArquivo = "Files/Suites.json";
        public Suite()
        {
        }

        public Suite(int numero, string tipoSuite, int capacidade, decimal valorDiaria)
        {
            TipoSuite = tipoSuite;
            Capacidade = capacidade;
            ValorDiaria = valorDiaria;
            Numero = numero;
        }

        public int Numero { get; set; }
        public string TipoSuite { get; set; } = "";
        public int Capacidade { get; set; }
        public decimal ValorDiaria { get; set; }


        public static IEnumerable<Suite> ListarSuites()
        {

            string conteudoDoArquivo = File.ReadAllText(_localArquivo);
            IEnumerable<Suite> lista = JsonConvert.DeserializeObject<IEnumerable<Suite>>(conteudoDoArquivo);

            return lista;

        }
        //-----------------------------------------------------------------------------
        public static Suite PegaSuites(int numero)
        {

            string conteudoDoArquivo = File.ReadAllText(_localArquivo);
            IEnumerable<Suite> lista = JsonConvert.DeserializeObject<IEnumerable<Suite>>(conteudoDoArquivo);

            foreach (var item in lista)
            {
                if(item.Numero == numero)
                {
                    return item;
                }
            }

            return new Suite();

        }
        //-----------------------------------------------------------------------------

        public static void ApresentaListaDeSuites(int menu)
        {
            IEnumerable<Suite> lista = ListarSuites();

            StringBuilder listaStringDeOpcao = new StringBuilder();

            if (menu != 3) { listaStringDeOpcao.AppendLine(">>> ESCOLHA UMA SUITE DIGITANDO O NÚMERO DO QUARTO <<<"); }

            foreach (var item in lista)
            {
                listaStringDeOpcao.AppendLine($"{item.Numero} - {item.TipoSuite} - {item.ValorDiaria.ToString("R$ 0.00")} - Capacidade para {item.Capacidade} hospede(s) ");
            }

            System.Console.WriteLine(listaStringDeOpcao);

        }

    }
}