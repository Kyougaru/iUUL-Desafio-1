/*******************************************************/
/* Classe Cadastro                                     */
/* Responsável por armazenar os pacientes e consultas  */
/* Além de realizar as operações de cadastro e exclusão*/
/*******************************************************/
using System.Collections.Generic;

namespace iUUL_Desafio1
{
    public class Cadastro
    {
        public List<Paciente> Pacientes { get; private set; }
        public List<Consulta> Consultas { get; private set; }

        public Cadastro()
        {
            Pacientes = new List<Paciente>();
            Consultas = new List<Consulta>();
        }

        public void Cadastrar(string cpf, string nome, string dataNasc)
        {
            var paciente = new Paciente(cpf, nome, dataNasc);
            Pacientes.Add(paciente);
        }

        public void Excluir(string cpf)
        {
            long cpfValido = long.Parse(cpf);
            var paciente = Pacientes.Find(i => i.CPF == cpfValido);
            Pacientes.Remove(paciente);
        }

        public void Agendar(string cpf, string dataConsulta, string horaInicial, string horaFinal)
        {
            long cpfValido = long.Parse(cpf);
            var paciente = Pacientes.Find(i => i.CPF == cpfValido);
            var consulta = new Consulta(paciente, dataConsulta, horaInicial, horaFinal);

            Consultas.Add(consulta);
        }

        public void CancelarAgendamento(string cpf)
        {
            long cpfValido = long.Parse(cpf);
            var paciente = Pacientes.Find(i => i.CPF == cpfValido);
            var consulta = paciente.Consulta;

            Consultas.Remove(consulta);
            paciente.Consulta = null;
        }
    }
}
