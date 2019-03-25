using Koper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koper.Controllers
{
    public class ClienteController : Controller
    {
        KoperEntities db = new KoperEntities();

        /// <summary>
        /// [GET]
        /// Lista de clientes.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            ViewBag.clientes = db.Cliente.ToList();

            return View();
        }

        /// <summary>
        /// [GET]
        /// View para cadastrar um novo Cliente.
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Criar()
        {
            return View();
        }

        /// <summary>
        /// [POST]
        /// Efetua o cadastro de um novo Cliente.
        /// </summary>
        /// <param name="cliente">[Models.Cliente] objeto Cliente</param>
        /// <param name="renda_text">[string] renda para ser convertida para Double.</param>
        /// <returns>View Index</returns>
        [HttpPost]
        public ActionResult Criar(Cliente cliente, string renda_text)
        {
            cliente.renda = Convert.ToDouble(renda_text, System.Globalization.CultureInfo.InvariantCulture);

            try
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// [GET]
        /// Exclui um Cliente.
        /// </summary>
        /// <param name="id">[int] id do Cliente</param>
        /// <returns>View Index</returns>
        public ActionResult Excluir(int id)
        {
            try
            {
                var cliente = db.Cliente.Find(id);

                db.Cliente.Remove(cliente);
                db.SaveChanges();
            } catch (Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
            }

            return RedirectToAction("Index");
        }


    }
}