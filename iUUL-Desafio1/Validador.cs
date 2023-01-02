/************************************************/
/* Classe Validador                             */
/* Responsável por validar os inputs do usuário */
/************************************************/
using System;
using System.Globalization;

namespace iUUL_Desafio1
{
    public class Validador
    {
        public Cadastro Cadastro { get; private set; }

        public Validador(Cadastro cadastro)
        {
            Cadastro = cadastro;
        }

        //Valida CPF do Paciente
        public string ValidarCPF(string cpf)
        {
            if (!long.TryParse(cpf, out long cpfValido) || cpf.Length != 11)
            {
                return "CPF deve ter 11 digitos.";
            }
            else if (!cpfValido.IsValidCPF())
            {
                return "CPF inválido.";
            }
            else if (PacienteExiste(cpfValido))
            {
                return "Não podem existir dois pacientes com o mesmo CPF.";
            }

            return null;
        }

        //Valida nome do Paciente
        public string ValidarNome(string nome)
        {
            if (nome.Length < 5)
                return "Nome deve conter pelo menos 5 caracteres.";

            return null;
        }

        //Valida data de Nascimento do Paciente
        public string ValidarDataNasc(string dataNasc)
        {
            if (!DateTime.TryParseExact(dataNasc, "dd/MM/yyyy", new CultureInfo("pt-BR"),
                DateTimeStyles.None, out DateTime dataValida))
            {
                return "Data de nascimento deve ter o formato DD/MM/AAAA.";
            }
            else
            {
                DateTime dataAtual = DateTime.Today;
                int idade = dataAtual.Year - dataValida.Year;

                if (idade < 13)
                    return "O paciente deve ter pelo menos 13 anos.";
            }

            return null;
        }

        //Valida se o paciente existe
        public string ValidarPaciente(string cpf)
        {
            if (!long.TryParse(cpf, out long cpfValido) || cpf.Length != 11)
                return "CPF deve ter 11 digitos.";
            else if (!PacienteExiste(cpfValido))
                return "O paciente não existe.";
            return null;
        }

        //Valida se o paciente existe e tem consulta
        public string ValidarPacienteComConsulta(string cpf)
        {
            if (ValidarPaciente(cpf) != null)
                return ValidarPaciente(cpf);
            else if (!PacienteTemConsulta(long.Parse(cpf)))
                return "O paciente não possui consulta agendada.";
            else
                return null;
        }

        //Valida se o paciente existe e não tem consulta
        public string ValidarPacienteSemConsulta(string cpf)
        {
            if (ValidarPaciente(cpf) != null)
                return ValidarPaciente(cpf);
            else if (PacienteTemConsulta(long.Parse(cpf)))
                return "O paciente possui consulta agendada.";
            else
                return null;
        }

        //Valida data da Consulta
        public string ValidarDataConsulta(string dataConsulta)
        {
            if (!DateTime.TryParseExact(dataConsulta, "dd/MM/yyyy", new CultureInfo("pt-BR"),
            DateTimeStyles.None, out DateTime dataValida))
            {
                return "Data da consulta deve ter o formato DD/MM/AAAA.";
            }
            else if (dataValida < DateTime.Today)
            {
                return "A data da consulta deve ser para um período futuro.";
            }

            return null;
        }

        //Valida hora inicial da Consulta
        public string ValidarHoraInicial(string dataConsulta, string horaInicial)
        {
            var dataValida = DateTime.ParseExact(dataConsulta, "dd/MM/yyyy", new CultureInfo("pt-BR"), DateTimeStyles.None);

            if (!TimeSpan.TryParseExact(horaInicial, "hhmm", CultureInfo.InvariantCulture, out TimeSpan horaInicialValida))
            {
                return "Hora inicial deve ter o formato HHMM.";
            }
            else if (!horaInicialValida.IsValidHora())
            {
                return "Hora inicial inválida.";
            }
            else if (!(dataValida > DateTime.Today || (dataValida == DateTime.Today && horaInicialValida > DateTime.Now.TimeOfDay)))
            {
                return "A hora da consulta deve ser para um período futuro.";
            }

            return null;
        }

        //Valida hora final da Consulta
        public string ValidarHoraFinal(string horaInicial, string horaFinal)
        {
            var horaInicialValida = TimeSpan.ParseExact(horaInicial, "hhmm", CultureInfo.InvariantCulture);

            if (!TimeSpan.TryParseExact(horaFinal, "hhmm", CultureInfo.InvariantCulture, out TimeSpan horaFinalValida))
            {
                return "Hora final deve ter o formato HHMM.";
            }
            else if (!horaFinalValida.IsValidHora())
            {
                return "Hora final inválida.";
            }
            else if (horaFinalValida <= horaInicialValida)
            {
                return "A consulta deve terminar depois de começar.";
            }

            return null;
        }

        //Valida se há sobreposição de horários nas consultas
        public string ValidarSobreposicao(string dataConsulta, string horaInicial, string horaFinal)
        {
            var dataValida = DateTime.ParseExact(dataConsulta, "dd/MM/yyyy", new CultureInfo("pt-BR"), DateTimeStyles.None);
            var horaInicialValida = TimeSpan.ParseExact(horaInicial, "hhmm", CultureInfo.InvariantCulture);
            var horaFinalValida = TimeSpan.ParseExact(horaFinal, "hhmm", CultureInfo.InvariantCulture);

            foreach (Consulta c in Cadastro.Consultas)
            {
                if ((dataValida == c.DataConsulta) && (horaInicialValida < c.HoraFinal && c.HoraInicial < horaFinalValida))
                {
                    return "Já existe uma consulta agendada nesse horário.";
                }
            }

            return null;
        }

        //Valida o cancelamento de uma consulta
        public string ValidarCancelamento(string cpf, string dataConsulta, string horaInicial)
        {
            long cpfValido = long.Parse(cpf);

            if (!DateTime.TryParseExact(dataConsulta, "dd/MM/yyyy", new CultureInfo("pt-BR"),
            DateTimeStyles.None, out DateTime dataValida))
                return "Data da consulta deve ter o formato DD/MM/AAAA.";
            else if (!TimeSpan.TryParseExact(horaInicial, "hhmm", CultureInfo.InvariantCulture, out TimeSpan horaInicialValida))
                return "Hora inicial deve ter o formato HHMM.";
            else if (Cadastro.Pacientes.Find(i => i.CPF == cpfValido).Consulta.DataConsulta != dataValida ||
                Cadastro.Pacientes.Find(i => i.CPF == cpfValido).Consulta.HoraInicial != horaInicialValida)
                return "Agendamento não encontrado.";
            return null;
        }

        public bool PacienteExiste(long cpf)
        {
            var paciente = Cadastro.Pacientes.Find(i => i.CPF == cpf);
            return paciente != null;
        }

        public bool PacienteTemConsulta(long cpf)
        {
            var consulta = Cadastro.Pacientes.Find(i => i.CPF == cpf).Consulta;
            return consulta != null;
        }
    }
}