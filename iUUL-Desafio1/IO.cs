/********************************************************************/
/* Classe IO                                                        */
/* Responsável por imprimir as mensagens e pegar o input do usuário */
/********************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;

namespace iUUL_Desafio1
{
    public class IO
    {
        public string CPF { get; private set; }
        public string Nome { get; private set; }
        public string DataNasc { get; private set; }
        public string DataConsulta { get; private set; }
        public string HoraInicial { get; private set; }
        public string HoraFinal { get; private set; }

        //Lê input do usuário até ele fazer uma escolha válida no menu
        public int LerEscolha(int max)
        {
            int escolha;
            bool correto = false;

            do
            {
                if (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 1 || escolha > max)
                {
                    MensagemErro("Faça uma escolha válida");
                } else
                {
                    correto = true;
                }
            } while (!correto);

            return escolha;
        }

        public void LerCPF()
        {
            Console.Write("CPF: ");
            CPF = Console.ReadLine();
        }

        public void LerNome()
        {
            Console.Write("Nome: ");
            Nome = Console.ReadLine();
        }

        public void LerDataNasc()
        {
            Console.Write("Data de nascimento: ");
            DataNasc = Console.ReadLine();
        }

        public void LerDataConsulta()
        {
            Console.Write("Data da consulta: ");
            DataConsulta = Console.ReadLine();
        }

        public void LerHoraInicial()
        {
            Console.Write("Hora inicial: ");
            HoraInicial = Console.ReadLine();
        }

        public void LerHoraFinal()
        {
            Console.Write("Hora final: ");
            HoraFinal = Console.ReadLine();
        }

        public void MensagemErro(string erro)
        {
            Console.WriteLine();
            Console.WriteLine("Erro: " + erro);
            Console.WriteLine();
        }

        public void MensagemSucessoCadastro()
        {
            Console.WriteLine();
            Console.WriteLine("Paciente cadastrado com sucesso!");
            Console.ReadKey();
        }

        public void MensagemSucessoExclusao()
        {
            Console.WriteLine();
            Console.WriteLine("Paciente excluído com sucesso!");
            Console.ReadKey();
        }

        public void MensagemSucessoAgendamento()
        {
            Console.WriteLine();
            Console.WriteLine("Agendamento realizado com sucesso!");
            Console.ReadKey();
        }

        public void MensagemSucessoCancelamento()
        {
            Console.WriteLine();
            Console.WriteLine("Agendamento cancelado com sucesso!");
            Console.ReadKey();
        }

        public void ImprimirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1-Cadastro de pacientes");
            Console.WriteLine("2-Agenda");
            Console.WriteLine("3-Fim");
            Console.WriteLine();
        }

        public void ImprimirMenuCadastro()
        {
            Console.Clear();
            Console.WriteLine("Menu do Cadastro de Pacientes");
            Console.WriteLine("1-Cadastrar novo paciente");
            Console.WriteLine("2-Excluir paciente");
            Console.WriteLine("3-Listar pacientes (ordenado por CPF)");
            Console.WriteLine("4-Listar pacientes (ordenado por nome)");
            Console.WriteLine("5-Voltar p/ menu principal");
            Console.WriteLine();
        }

        public void ImprimirMenuAgenda()
        {
            Console.Clear();
            Console.WriteLine("Agenda");
            Console.WriteLine("1-Agendar consulta");
            Console.WriteLine("2-Cancelar agendamento");
            Console.WriteLine("3-Listar agenda");
            Console.WriteLine("4-Voltar p/ menu principal");
            Console.WriteLine();
        }

        //Imprime lista de pacientes formatada
        public void ListarPacientes(List<Paciente> pacientes)
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("{0,-11} {1,-32} {2,-10} {3,-3}", "CPF", "Nome", "Dt.Nasc.", "Idade");
            Console.WriteLine("-------------------------------------------------------------");
            foreach (Paciente paciente in pacientes)
            {
                Console.WriteLine("{0,-11} {1,-32} {2,-10}  {3,-3}", paciente.CPF, paciente.Nome, 
                    paciente.DataNasc.ToShortDateString(), DateTime.Today.Year - paciente.DataNasc.Year);
                if(paciente.Consulta != null)
                {
                    Console.WriteLine("{0,-11} {1,-32}", " ", "Agendado para: " + paciente.Consulta.DataConsulta.ToShortDateString());
                    Console.WriteLine("{0,-11} {1,-32}", " ", paciente.Consulta.HoraInicial + " às " + paciente.Consulta.HoraFinal);
                }
            }
            Console.ReadKey();
        }

        //Lê input do usuário até ele fazer uma escolha válida de como a lista será apresentada
        public void ListarAgenda(List<Consulta> consultas)
        {
            Console.Write("Apresentar a agenda T-Toda ou P-Periodo: ");
            string escolha = Console.ReadLine();
            bool correto = false;

            while (!correto)
            {
                if (escolha != "T" && escolha != "P")
                {
                    MensagemErro("Faça uma escolha válida (T ou P)");
                }
                else
                {
                    correto = true;
                }
            }

            if(escolha == "P")
            {
                ListarAgendaP(consultas);
            } else
            {
                ListarAgendaT(consultas);
            }
        }

        //Lê input do usuário até ele digitar os dados corretamente e em seguida imprime a lista
        public void ListarAgendaP(List<Consulta> consultas)
        {
            Console.Write("Data inicial: ");
            string dataInicial = Console.ReadLine();
            DateTime dataInicialValida, dataFinalValida;
            bool correto = false;

            do
            {
                if (!DateTime.TryParseExact(dataInicial, "dd/MM/yyyy", new CultureInfo("pt-BR"),
                DateTimeStyles.None, out dataInicialValida))
                {
                    MensagemErro("Data da consulta deve ter o formato DD/MM/AAAA.");
                }
                else
                {
                    correto = true;
                }
            } while (!correto);

            Console.Write("Data final: ");
            string dataFinal = Console.ReadLine();
            correto = false;

            do
            {
                if (!DateTime.TryParseExact(dataFinal, "dd/MM/yyyy", new CultureInfo("pt-BR"),
                DateTimeStyles.None, out dataFinalValida))
                {
                    MensagemErro("Data da consulta deve ter o formato DD/MM/AAAA.");
                }
            } while (!correto);

            foreach (Consulta consulta in consultas)
            {
                if (consulta.DataConsulta >= dataInicialValida || consulta.DataConsulta <= dataFinalValida)
                {
                    ListaConsulta(consulta);
                }
            }
            Console.ReadKey();
        }

        public void ListarAgendaT(List<Consulta> consultas)
        {
            foreach (Consulta consulta in consultas)
            {
                ListaConsulta(consulta);
            }
            Console.ReadKey();
        }

        //Imprime lista de consultas formatada
        public void ListaConsulta(Consulta consulta)
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("   {0,-10} {1,-5} {2,-5} {3,-5} {4,-32} {5,-10}",
                "Data", "H.Ini", "H.Fim", "Tempo", "Nome", "Dt.Nasc");
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("   {0,-10} {1,-5} {2,-5} {3,-5} {4,-32} {5,-10}",
                consulta.DataConsulta.ToShortDateString(), consulta.HoraInicial.ToString(@"hh\:mm"),
                consulta.HoraFinal.ToString(@"hh\:mm"), (consulta.HoraFinal - consulta.HoraInicial).ToString(@"hh\:mm"),
                consulta.Paciente.Nome, consulta.Paciente.DataNasc.ToShortDateString());
        }
    }
}
