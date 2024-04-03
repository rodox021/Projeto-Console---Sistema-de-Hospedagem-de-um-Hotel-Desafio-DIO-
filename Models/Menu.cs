using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace Projeto___Sistema_de_Hospedagem_de_um_Hotel.Models
{
    public static class Menu
    {
       //private static string _arquivoDeHospedes = "Files/Reservas.json";
        public static void Opcoes()
        {


            int opcaoMenu = -1;

            do
            {
                Console.Clear();

                System.Console.WriteLine(
                    ">>>SELECIONE UM OPÇÂO OU DIGITE 0 PARA SAIR<<<\n\n"+
                    "\n1 - >>> Cadastrar Reserva <<<"
                    + "\n2 - >>> Listas Reservas <<<"
                    + "\n3 - >>> Consulta Suites <<<"
                    + "\n4 - >>> CheckOut <<<"

                );


                if (int.TryParse(Console.ReadLine(), out int valor))
                {
                    opcaoMenu = valor;
                    if (opcaoMenu == 0) { break; }
                }
                else
                {
                    opcaoMenu = -1;
                    System.Console.WriteLine("Opção Invalida, aperta qualquer tecla para continuar");
                    Console.ReadLine();
                    continue;

                }


                
                switch (opcaoMenu)
                {
                    case 1:
                        Console.Clear();
                        CadastrarReserva();
                        break;
                    case 2:
                        Console.Clear();
                        System.Console.WriteLine( Reserva.ListarReservas());
                        Console.ReadLine();
                        break;
                    case 3:
                         Console.Clear();
                        Suite.ApresentaListaDeSuites(3);
                        Console.ReadLine();
                        break;
                    case 4:
                        FazerCheckOut();
                        break;

                    default:
                        System.Console.WriteLine("Opção Invalida, aperta qualquer tecla para continuar");
                        Console.ReadLine();

                        break;
                }

            } while (opcaoMenu != 0);





        }

        //---------------------------------------------------------------------------------------------------------

        private static void CadastrarReserva()
        {
            int quantidadeDeHospedes = 0;
            int dias = 0;
            bool conversaoFuncionou = false;
            Suite suite = new Suite();
            List<Pessoa> hospedes = new List<Pessoa>();
            


            do
            {
                System.Console.WriteLine("Quantos hospedes?");
                conversaoFuncionou = int.TryParse(Console.ReadLine(), out int valor);
                if (conversaoFuncionou)
                {
                    quantidadeDeHospedes = valor;
                }
                else
                {
                    System.Console.WriteLine( "Número invalido!!, presione qualque tecla para continuar.");
                    Console.ReadLine();
                    Console.Clear();
                }
                        
            } while (!conversaoFuncionou);
            Console.Clear();
            //Preenche os dados dos hospedes

            for (int i = 0; i < quantidadeDeHospedes; i++) 
            {
                System.Console.WriteLine($"<<< DADOS OS HOSPEDES >>>\n\n");
                string nome, sobrenome;
                System.Console.WriteLine($"Digite o primeiro nome do {i+1}º hospede");
                nome = Console.ReadLine();
                System.Console.WriteLine($"Digite o sobrenome nome do {i+1}º hospede");
                sobrenome = Console.ReadLine();

               
                hospedes.Add( new Pessoa(nome, sobrenome));
                Console.Clear();
            };

            // escolhe a quantidade de dias
            System.Console.WriteLine("Digite o a quantidade de dias");
            dias = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            // seleciona o quarto
            System.Console.WriteLine("Escolha um quarto");
            Suite.ApresentaListaDeSuites(1);

            int suiteEscolhida = Convert.ToInt32(Console.ReadLine());
            Suite suiteDoHospede = Suite.PegaSuites(suiteEscolhida);

            try
            {
                //Cria  reserva
                Reserva reserva = new Reserva();
    
                reserva.Suite = suiteDoHospede;
                reserva.DiasReservados = dias;
                reserva.CadastrarHospedes(hospedes);
    
               (int id, bool sucesso) = reserva.CadastrarReserva(reserva);
                 Console.Clear();
                Console.WriteLine(sucesso? "Reserva efetuada com sucesso!" : "Erro inesperado ao gravar");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                
                System.Console.WriteLine(ex.Message + ", preessione qualquer tecla para voltar");
                Console.ReadLine();
                return;
            }
        }

          //------------------------------------------------------------------------------------

        public static void FazerCheckOut()
        {
            Console.Clear();
            Console.WriteLine(Reserva.ListarReservas());
            Console.WriteLine("Qual reserva deseja fazer o CheckOut?");
           Reserva reservaSelecionada =  Reserva.ObterSuiteSelecionada(Convert.ToInt32( Console.ReadLine()));
            reservaSelecionada.CheckOut();
            System.Console.WriteLine("Checkou efetuado com sucesso!, pressione qualquer tecla para volta ao menu");
            Console.ReadLine();





        }
    }
}