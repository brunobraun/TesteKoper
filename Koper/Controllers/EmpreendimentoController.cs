using Koper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koper.Controllers
{
    public class EmpreendimentoController : Controller
    {
        KoperEntities db = new KoperEntities();
        
        /// <summary>
        /// [GET]
        /// Lista Empreendimentos.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            ViewBag.empreendimentos = db.Empreendimento.ToList();

            return View();
        }

        /// <summary>
        /// [GET]
        /// View para cadastrar um novo Empreendimento.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Criar()
        {
            ViewBag.idTipo_Empreendimento = new SelectList(db.Tipo_Empreendimento, "id", "tipo");

            return View();
        }

        /// <summary>
        /// [POST]
        /// Efetua o cadastro de um Empreendimento.
        /// </summary>
        /// <param name="empreendimento">[Models.Empreendimento] objeto Empreendimento.</param>
        /// <param name="area_total_text">[string] area total para ser convertido para Double</param>
        /// <returns>View Index</returns>
        [HttpPost]
        public ActionResult Criar(Empreendimento empreendimento, string area_total_text)
        {
            empreendimento.area_total = Convert.ToDouble(area_total_text, System.Globalization.CultureInfo.InvariantCulture);

            try
            {
                db.Empreendimento.Add(empreendimento);
                db.SaveChanges();
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
            }            

            return RedirectToAction("Index", new { idEmpreendimento = empreendimento.id });
        }


        /// <summary>
        /// [GET]
        /// Exclui um Empreendimento.
        /// </summary>
        /// <param name="id">[int] id do Empreendimento</param>
        /// <returns>View Index</returns>
        public ActionResult Excluir(int id)
        {
            Empreendimento empreendimento = db.Empreendimento.Find(id);

            // TODO
            // REMOVER CASCADE

            db.Empreendimento.Remove(empreendimento);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// [POST]
        /// Lista de objeto Bloco dado um Empreendimento.
        /// </summary>
        /// <param name="idEmpreendimento">[int] id da tabela Empreendimento</param>
        /// <returns>
        /// JSON
        /// {
        ///     sucesso: [bool] indica se operação foi realizada corretamente.
        ///     blocos: [List[Bloco]] lista de objeto Bloco
        /// }
        /// </returns>
        [HttpPost]
        public ActionResult BlocosPorEmpreendimento(int idEmpreendimento)
        {
            try
            {
                var blocos = db.Bloco.Where(x => x.idEmpreendimento == idEmpreendimento).Select(a => new { a.id, a.nome }).ToList();
                
                return Json(new { sucesso = true, blocos = blocos });
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
                return Json(new { sucesso = false });
            }
            
        }


    }
}