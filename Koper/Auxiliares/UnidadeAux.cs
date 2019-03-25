using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Koper.Models;

namespace Koper
{
    /// <summary>
    /// Classe auxiliar para retorno de dados de unidades em views.
    /// </summary>
    public class UnidadeAux
    {
        public long id { get; set; }
        public string nome { get; set; }
        public int idStatus_Unidade { get; set; }
        public string status { get; set; }
        public double area_privativa { get; set; }
        public double area_comum { get; set; }
        public double vendavel { get; set; }
        public double valor { get; set; }
        public double valor_minimo_venda { get; set; }
        public int pertence_a_tipo { get; set; }
        public Nullable<long> idAndar { get; set; }
        public Nullable<long> idBloco { get; set; }
        public Nullable<long> idEmpreendimento { get; set; }
        public string andar { get; set; }
        public string bloco { get; set; }
        public string empreendimento { get; set; }

        /// <summary>
        /// Lista unidades. Caso empreendimento seja definido, lista apenas unidades do empreendimento.
        /// </summary>
        /// <param name="idEmpreendimento">[int] id tabela Empreendimento</param>
        /// <param name="somenteDisponiveis">[bool] variável que define somente o retorno de unidades disponíveis</param>
        /// <returns>
        /// {
        ///     long id
        ///     string nome
        ///     int idStatus_Unidade
        ///     string status
        ///     double area_privativa
        ///     double area_comum
        ///     double vendavel
        ///     double valor
        ///     double valor_minimo_venda
        ///     int pertence_a_tipo
        ///     Nullable<long> idAndar
        ///     Nullable<long> idBloco
        ///     Nullable<long> idEmpreendimento
        ///     string andar
        ///     string bloco
        ///     string empreendimento
        /// }
        /// </returns>
        public List<UnidadeAux> ListaUnidades(int idEmpreendimento = 0, bool somenteDisponiveis = false)
        {
            try
            {
                using (KoperEntities db = new KoperEntities())
                {
                    List<UnidadeAux> listaUnidades = new List<UnidadeAux>();

                    var unidades = idEmpreendimento == 0 ? db.Unidade.ToList() : db.Unidade.Where(x => x.idEmpreendimento == idEmpreendimento).ToList();

                    if (somenteDisponiveis) unidades = unidades.Where(x => x.idStatus_Unidade == 1).ToList();

                    var listaStatus = db.Status_Unidade.ToList();

                    foreach (var unidade in unidades)
                    {
                        UnidadeAux novaUnidade = new UnidadeAux();

                        novaUnidade.id = unidade.id;
                        novaUnidade.status = listaStatus.Where(x => x.id == unidade.idStatus_Unidade).FirstOrDefault().status;
                        novaUnidade.nome = unidade.nome;
                        novaUnidade.area_comum = unidade.area_comum;
                        novaUnidade.area_privativa = unidade.area_privativa;
                        novaUnidade.bloco = unidade.idBloco != null ? unidade.Bloco.nome : " ";
                        novaUnidade.empreendimento = unidade.idEmpreendimento != null ? unidade.Empreendimento.nome : " ";
                        novaUnidade.andar = unidade.idAndar != null ? unidade.Andar.nome : " ";
                        novaUnidade.valor = unidade.valor;
                        novaUnidade.valor_minimo_venda = unidade.valor_minimo_venda;
                        novaUnidade.vendavel = unidade.vendavel;

                        listaUnidades.Add(novaUnidade);

                    }

                    return listaUnidades;
                }
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
                return new List<UnidadeAux>();
            }
                       
        }

    }
}