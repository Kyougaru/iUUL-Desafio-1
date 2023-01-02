/************************/
/* Classe IO            */
/* Modelo dos pacientes */
/************************/
using System;
using System.Collections.Generic;
using System.Globalization;

namespace iUUL_Desafio1
{
    public class Paciente
    {
        public long CPF { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataNasc { get; private set; }
        public List<Consulta> ConsultasAnteriores { get; private set; }
        private Consulta consulta;

        public Consulta Consulta { 
            get
            {
                return consulta;
            }
            set
            {
                if (consulta == null || value == null)
                    consulta = value;
                else
                    throw new Exception("O paciente possui consulta agendada");
            }
        }

        public Paciente(string cpf, string nome, string dataNasc)
        {
            consulta = null;
            CPF = long.Parse(cpf);
            Nome = nome;
            DataNasc = DateTime.ParseExact(dataNasc, "dd/MM/yyyy", new CultureInfo("pt-BR"));
        }
    }
}
