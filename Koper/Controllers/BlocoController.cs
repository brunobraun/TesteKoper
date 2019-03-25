using Koper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koper.Controllers
{
    public class BlocoController : Controller
    {
        KoperEntities db = new KoperEntities();


        /// <summary>
        /// [GET]
        /// Lista Blocos de um Empreendimento.
        /// </summary>
        /// <param name="idEmpreendimento">[int] id da tabela Empreendimento</param>
        /// <returns>View</returns>
        public ActionResult Index(int idEmpreendimento)
        {
            try
            {
                ViewBag.blocos = db.Bloco.Where(x => x.idEmpreendimento == idEmpreendimento).ToList();
                ViewBag.idEmpreendimento = idEmpreendimento;
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
            }
            
            return View();
        }


        /// <summary>
        /// [GET]
        /// View para Cadastro de um Bloco de um dado Empreedimento.
        /// </summary>
        /// <param name="idEmpreendimento">[int] id da tabela Empreendimento</param>
        /// <returns>View</returns>
        public ActionResult Criar(int idEmpreendimento)
        {
            
            ViewBag.idEmpreendimento = idEmpreendimento;

            return View();
        }

        /// <summary>
        /// [POST]
        /// Efetua o cadastro de um Bloco.
        /// </summary>
        /// <param name="bloco">[Models.Bloco] objeto Bloco</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Criar(Bloco bloco)
        {
            try
            {
                db.Bloco.Add(bloco);
                db.SaveChanges();
            }catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
            }

            return RedirectToAction("Index", new { idEmpreendimento = bloco.idEmpreendimento });
        }

        /// <summary>
        /// [GET]
        /// Exclui um Bloco
        /// </summary>
        /// <param name="id">[int] id da tabela Bloco</param>
        /// <returns>View Index</returns>
        public ActionResult Excluir(int id)
        {
            long idEmpreendimento = 0;
            try
            {
                Bloco bloco = db.Bloco.Find(id);

                idEmpreendimento = bloco.idEmpreendimento;

                db.Bloco.Remove(bloco);
                db.SaveChanges();
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
            }
            
            return RedirectToAction("Index", new { idEmpreendimento = idEmpreendimento });
        }

        /// <summary>
        /// [POST]
        /// Lista de objeto Andar dado um Bloco.
        /// </summary>
        /// <param name="idBloco">[int] id da tabela Bloco</param>
        /// <returns>
        /// JSON
        /// {
        ///     sucesso: [bool] indica se operação foi realizada corretamente.
        ///     andares: [List[Andar]] lista de objeto Andar
        /// }
        /// </returns>
        [HttpPost]
        public ActionResult AndaresPorBloco(int idBloco)
        {
            try
            {
                var andares = db.Andar.Where(x => x.idBloco == idBloco).Select(a => new { a.id, a.nome }).ToList();

                return Json(new { sucesso = true, andares = andares });
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
                return Json(new { sucesso = false, msg = ex.Message });
            }
            
        } 

    }
}