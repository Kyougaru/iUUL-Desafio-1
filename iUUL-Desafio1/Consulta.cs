/************************/
/* Classe IO            */
/* Modelo das consultas */
/************************/
using System;
using System.Globalization;

namespace iUUL_Desafio1
{
    public class Consulta
    {
        public Paciente Paciente { get; private set; }
        public DateTime DataConsulta { get; private set; }
        public TimeSpan HoraInicial { get; private set; }
        public TimeSpan HoraFinal { get; private set; }

        public Consulta(Paciente paciente, string dataConsulta, string horaInicial, string horaFinal)
        {
            DataConsulta = DateTime.ParseExact(dataConsulta, "dd/MM/yyyy", new CultureInfo("pt-BR"));
            HoraInicial = TimeSpan.ParseExact(horaInicial, "hhmm", CultureInfo.InvariantCulture);
            HoraFinal = TimeSpan.ParseExact(horaFinal, "hhmm", CultureInfo.InvariantCulture);

            Paciente = paciente;
            Paciente.Consulta = this;
        }
    }
}
