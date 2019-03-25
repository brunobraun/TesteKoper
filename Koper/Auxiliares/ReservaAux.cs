using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Koper
{
    /// <summary>
    /// Classe auxiliar para retorno de dados de reserva em views.
    /// </summary>
    public class ReservaAux
    {
        public long id { get; set; }
        public string nome_cliente { get; set; }
        public double valor_total { get; set; }
        public string data_reserva { get; set; }

        /// <summary>
        /// Método para retornar o valor total de uma reserva.
        /// </summary>
        /// <param name="idReserva">[int] id da tabela Reserva</param>
        /// <returns>
        /// [double]
        /// Valor total de uma reserva.
        /// </returns>
        public double ValorTotalReserva(long idReserva)
        {
            try
            {
                using (Koper.Models.KoperEntities db = new Models.KoperEntities())
                {
                    var reservas = db.Unidade_Reserva.Where(x => x.idReserva == idReserva).ToList();

                    var valorTotal = 0.0;

                    foreach (var item in reservas)
                    {
                        valorTotal += item.Unidade.valor;
                    }
                    return valorTotal;
                }
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
                return 0;
            }
            
        }

    }
}