using Koper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koper.Controllers
{
    public class AndarController : Controller
    {

        KoperEntities db = new KoperEntities();


        /// <summary>
        /// [GET]
        /// Lista os Andares de um Bloco.
        /// </summary>
        /// <param name="idBloco">[int] id da tabela Bloco</param>
        /// <returns>View</returns>
        public ActionResult Index(int idBloco)
        {
            try
            {
                ViewBag.andares = db.Andar.Where(x => x.idBloco == idBloco).ToList();
                ViewBag.idBloco = idBloco;
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
            }



            return View();
        }

        /// <summary>
        /// [GET]
        /// View para cadastrar um novo Andar em um Bloco.
        /// </summary>
        /// <param name="idBloco">[int] id da tabela Bloco</param>
        /// <returns>View</returns>
        public ActionResult Criar(int idBloco)
        {
            ViewBag.idBloco = idBloco;

            return View();
        }


        /// <summary>
        /// [POST]
        /// Efetua o cadastro de um Andar.
        /// </summary>
        /// <param name="andar">[Models.Andar] objeto Andar</param>
        /// <returns>View Index</returns>
        [HttpPost]
        public ActionResult Criar(Andar andar)
        {
            try
            {
                db.Andar.Add(andar);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
            }
            
            return RedirectToAction("Index", new { idBloco = andar.idBloco });
        }


        /// <summary>
        /// Exclui um Andar
        /// </summary>
        /// <param name="id">[int] id do Andar</param>
        /// <returns>View Index</returns>
        public ActionResult Excluir(int id)
        {
            long idBloco = 0;

            try
            {
                Andar andar = db.Andar.Find(id);

                idBloco = andar.idBloco;

                db.Andar.Remove(andar);
                db.SaveChanges();
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
            }


            return RedirectToAction("Index", new { idBloco = idBloco });
        }
    }
}