/************************************************************************/
/* Classe Extensions                                                    */
/* Contém os métodos de extensão que são utilizados na classe Validador */
/************************************************************************/
using System;

namespace iUUL_Desafio1
{
    public static class Extensions
    {
        //Valida o CPF de acordo com o Anexo A da lista de exercícios 2
        public static bool IsValidCPF(this long cpf)
        {
            if (cpf > 99999999999 || cpf < 11111111111)
                return false;
            else if (cpf == long.Parse(new string((char)((cpf % 10) + 48), 11)))
                return false;
            else
            {
                long cpf_sem_dv = cpf / 100;

                long soma = 0;
                for (int valor = 2; valor <= 11; valor++)
                {
                    soma += cpf_sem_dv % 10 * valor;
                    cpf_sem_dv /= 10;
                }

                int dv1 = (int)(11 - (soma % 11));

                if (dv1 > 9)
                    dv1 = 0;

                cpf_sem_dv = cpf / 10;

                soma = 0;
                for (int valor = 2; valor <= 12; valor++)
                {
                    soma += cpf_sem_dv % 10 * valor;
                    cpf_sem_dv /= 10;
                }

                int dv2 = (int)(11 - (soma % 11));

                if (dv2 > 9)
                    dv2 = 0;

                return dv1 == cpf % 100 / 10 && dv2 == cpf % 10;
            }
        }

        //Valida a hora de acordo com as regras de negócio do consultório odontológico
        public static bool IsValidHora(this TimeSpan hora)
        {
            if (hora.Hours < 8 || hora.Hours > 19)
                return false;
            else
                return (hora.Minutes == 0 || hora.Minutes == 15 || hora.Minutes == 30 || hora.Minutes == 45);
        }
    }
}
