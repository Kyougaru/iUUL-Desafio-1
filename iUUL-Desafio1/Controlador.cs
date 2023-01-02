/****************************************************/
/* Classe Controlador                               */
/* Responsável por gerenciar os objetos da aplicação*/
/****************************************************/
using System.Linq;

namespace iUUL_Desafio1
{
    public class Controlador
    {
        static Cadastro cadastro = new Cadastro();
        static Validador validador = new Validador(cadastro);
        static IO io = new IO();

        /******************************* Menus *********************************/
        /* Se o usuário digitar um número diferente do valor esperado,         */
        /* um erro é exibido e é pedido para ele digitar novamente o comando   */
        /* A aplicação ficará em loop até o usuário digitar 3 no Menu Principal*/
        /***********************************************************************/

        public static void Start()
        {
            MenuPrincipal();
        }

        public static void MenuPrincipal()
        {
            int escolha;

            do
            {
                io.ImprimirMenuPrincipal();
                escolha = io.LerEscolha(3);
                switch (escolha)
                {
                    case 1:
                        MenuCadastro();
                        break;
                    case 2:
                        MenuAgenda();
                        break;
                }
            } while (escolha != 3);
        }

        public static void MenuCadastro()
        {
            io.ImprimirMenuCadastro();
            int escolha = io.LerEscolha(5);

            switch (escolha)
            {
                case 1:
                    Cadastrar();
                    break;
                case 2:
                    Excluir();
                    break;
                case 3:
                    io.ListarPacientes(cadastro.Pacientes.OrderBy(x => x.CPF).ToList());
                    break;
                case 4:
                    io.ListarPacientes(cadastro.Pacientes.OrderBy(x => x.Nome).ToList());
                    break;
                case 5:
                    break;
            }
        }

        public static void MenuAgenda()
        {
            io.ImprimirMenuAgenda();
            int escolha = io.LerEscolha(4);

            switch (escolha)
            {
                case 1:
                    Agendar();
                    break;
                case 2:
                    Cancelar();
                    break;
                case 3:
                    io.ListarAgenda(cadastro.Consultas);
                    break;
                case 4:
                    break;
            }
        }

        /********************** Funcionalidades dos Menus **********************/
        /* Lê o input do usuário                                               */
        /* Caso ele esteja incorreto, é exibido uma mensagem de erro           */
        /* O input será lido até ele digitar os dados corretamente             */
        /* Após isso, é executado a funcionalidade escolhida pelo usuário      */
        /* É exibido uma mensagem de sucesso caso funcione corretamente        */
        /***********************************************************************/

        public static void Cadastrar()
        {
            string erro;

            do
            {
                io.LerCPF();
                erro = validador.ValidarCPF(io.CPF);
                if (erro != null)
                    io.MensagemErro(erro);
            } while (erro != null);
            do
            {
                io.LerNome();
                erro = validador.ValidarNome(io.Nome);
                if (erro != null)
                    io.MensagemErro(erro);
            } while (erro != null);
            do
            {
                io.LerDataNasc();
                erro = validador.ValidarDataNasc(io.DataNasc);
                if (erro != null)
                    io.MensagemErro(erro);
            } while (erro != null);

            cadastro.Cadastrar(io.CPF, io.Nome, io.DataNasc);
            io.MensagemSucessoCadastro();
        }
        
        public static void Excluir()
        {
            string erro;

            do
            {
                io.LerCPF();
                erro = validador.ValidarPaciente(io.CPF);
                if (erro != null)
                    io.MensagemErro(erro);
            } while (erro != null);

            cadastro.Excluir(io.CPF);
            io.MensagemSucessoExclusao();
        }

        public static void Agendar()
        {
            string erro;

            do
            {
                io.LerCPF();
                erro = validador.ValidarPacienteSemConsulta(io.CPF);
                if (erro != null)
                    io.MensagemErro(erro);
            } while (erro != null);

            do
            {
                do
                {
                    io.LerDataConsulta();
                    erro = validador.ValidarDataConsulta(io.DataConsulta);
                    if (erro != null)
                        io.MensagemErro(erro);
                } while (erro != null);

                do
                {
                    io.LerHoraInicial();
                    erro = validador.ValidarHoraInicial(io.DataConsulta, io.HoraInicial);
                    if (erro != null)
                        io.MensagemErro(erro);
                } while (erro != null);

                do
                {
                    io.LerHoraFinal();
                    erro = validador.ValidarHoraFinal(io.HoraInicial, io.HoraFinal);
                    if (erro != null)
                        io.MensagemErro(erro);
                } while (erro != null);

                erro = validador.ValidarSobreposicao(io.DataConsulta, io.HoraInicial, io.HoraFinal);
                if (erro != null)
                    io.MensagemErro(erro);
            } while (erro != null);

            cadastro.Agendar(io.CPF, io.DataConsulta, io.HoraInicial, io.HoraFinal);
            io.MensagemSucessoAgendamento();
        }

        public static void Cancelar()
        {
            string erro;
            do
            {
                do
                {
                    io.LerCPF();
                    erro = validador.ValidarPacienteComConsulta(io.CPF);
                    if (erro != null)
                        io.MensagemErro(erro);
                } while (erro != null);

                io.LerDataConsulta();
                io.LerHoraInicial();

                erro = validador.ValidarCancelamento(io.CPF, io.DataConsulta, io.HoraInicial);
                if (erro != null)
                    io.MensagemErro(erro);

            } while (erro != null);

            cadastro.CancelarAgendamento(io.CPF);
            io.MensagemSucessoCancelamento();
        }
    }
}
