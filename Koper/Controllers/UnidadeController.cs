using Koper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koper.Controllers
{
    public class UnidadeController : Controller
    {
        KoperEntities db = new KoperEntities();


        /// <summary>
        /// [GET]
        /// Lista Unidades de um Empreendimento
        /// </summary>
        /// <param name="idEmpreendimento">[int] id da tabela Empreendimento</param>
        /// <returns>View</returns>
        public ActionResult Index(int idEmpreendimento)
        {
            var unidadeAux = new UnidadeAux();

            ViewBag.unidades = unidadeAux.ListaUnidades(idEmpreendimento);

            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEmpreendimento"></param>
        /// <param name="somenteDisponiveis"></param>
        /// <returns></returns>
        public ActionResult ListaUnidades(int idEmpreendimento = 0, bool somenteDisponiveis = false)
        {
            var unidadeAux = new UnidadeAux();

            return Json(new { sucesso = true, lista = unidadeAux.ListaUnidades(idEmpreendimento, somenteDisponiveis)  }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// [GET]
        ///  View para Cadastro de uma nova Unidade
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Criar()
        {

            ViewBag.idEmpreendimento = new SelectList(db.Empreendimento, "id", "nome");

            return View();
        }

        /// <summary>
        /// [POST]
        /// Efetua o cadastro de uma Unidade.
        /// </summary>
        /// <param name="unidade">[Models.Unidade] objeto Unidade</param>
        /// <param name="area_privativa_text">[string] string para ser convertida em Double</param>
        /// <param name="area_comum_text">[string] string para ser convertida em Double</param>
        /// <param name="vendavel_text">[string] string para ser convertida em Double</param>
        /// <param name="valor_text">[string] string para ser convertida em Double</param>
        /// <param name="valor_minimo_venda_text">[string] string para ser convertida em Double</param>
        /// <returns>View Index</returns>
        [HttpPost]
        public ActionResult Criar(Models.Unidade unidade, string area_privativa_text, string area_comum_text, string vendavel_text, string valor_text, string valor_minimo_venda_text)
        {
            unidade.area_privativa = Convert.ToDouble(area_privativa_text, System.Globalization.CultureInfo.InvariantCulture);
            unidade.area_comum = Convert.ToDouble(area_comum_text, System.Globalization.CultureInfo.InvariantCulture);
            unidade.vendavel = Convert.ToDouble(vendavel_text, System.Globalization.CultureInfo.InvariantCulture);
            unidade.valor = Convert.ToDouble(valor_text, System.Globalization.CultureInfo.InvariantCulture);
            unidade.valor_minimo_venda = Convert.ToDouble(valor_minimo_venda_text, System.Globalization.CultureInfo.InvariantCulture);
            unidade.idStatus_Unidade = 1;

            try
            {
                var empreendimento = db.Empreendimento.Find(unidade.idEmpreendimento);

                var soma_areas = unidade.area_comum + unidade.area_privativa;

                if (empreendimento.area_total < soma_areas) return Json(new { sucesso = false, msg = "A soma das areas não pode ser maior que a area total do empreendimento. OBS: TODO = TRATAR NO FRONT ESSA VALIDAÇÃO..." });
                
                db.Unidade.Add(unidade);
                db.SaveChanges();

                return RedirectToAction("Index", new { idEmpreendimento = unidade.idEmpreendimento });

            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
                return RedirectToAction("Index", new { idEmpreendimento = unidade.idEmpreendimento });
            }
            
        }

        /// <summary>
        /// [GET]
        /// Exclui uma Unidade caso não existam Reservas, Vendas ou Propostas relacionadas a ela.
        /// </summary>
        /// <param name="idUnidade">[int] id da Unidade</param>
        /// <returns>View Index</returns>
        public ActionResult Excluir(int idUnidade)
        {
            Models.Unidade unidade = db.Unidade.Find(idUnidade);

            try
            {
                var idEmpreendimento = unidade.idEmpreendimento;

                db.Unidade.Remove(unidade);
                db.SaveChanges();

                return RedirectToAction("Index", new { idEmpreendimento = idEmpreendimento });
            } catch(Exception ex)
            {
                return Json(new { sucesso = false, msg = "Não foi possível excluir pois existem registros relacionados a esta unidade. OBS: TODO = FALTOU TRATAR EXCEÇÃO NO FRONT." });
            }           
            
        }

    }
}