using Projeto___Sistema_de_Hospedagem_de_um_Hotel.Models;




//Criadno Reserva
Reserva reserva = new Reserva();

// Criando Hospedes
List<Pessoa> hospedes = new List<Pessoa>();
Pessoa p1 = new Pessoa("Rodolfo", "Braga");
Pessoa p2 = new Pessoa("Juliana", "Tobar");

hospedes.Add(p1);
hospedes.Add(p2);



//Cadastro dfe Suite
Suite suite = new Suite("Premium Casal", 2,150M );



//Setando valores da reserva
reserva.CadastrarSuite(suite);
reserva.CadastrarHospedes(hospedes);

Console.Clear();
Menu.Opcoes();

System.Console.WriteLine( reserva.Apresentacao());









