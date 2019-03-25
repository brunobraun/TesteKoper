using Koper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koper.Controllers
{
    public class ReservaController : Controller
    {

        KoperEntities db = new KoperEntities();

        /// <summary>
        /// [GET]
        /// Lista de reservas.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            try
            {
                var reservas = db.Reserva.ToList();

                List<ReservaAux> listaReserva = new List<ReservaAux>();



                foreach (var item in reservas)
                {
                    listaReserva.Add(new ReservaAux
                    {
                        id = item.id,
                        data_reserva = item.data_reserva.ToLongDateString(),
                        nome_cliente = item.Cliente.nome,
                        valor_total = new ReservaAux().ValorTotalReserva(item.id)
                    });
                }


                ViewBag.reservas = listaReserva;
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
            }

            return View();
        }


        /// <summary>
        /// [GET]
        /// View para Cadastro de uma nova Reserva
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Criar()
        {
            //var unidadeAux = new Unidade();

            ViewBag.idCliente = new SelectList(db.Cliente, "id", "nome");

            ViewBag.idEmpreendimento = new SelectList(db.Empreendimento, "id", "nome");

            return View();
        }


        /// <summary>
        /// [POST]
        /// Efetua o cadastro de uma Reserva
        /// </summary>
        /// <param name="reserva">[Models.Reserva] objeto Reserva</param>
        /// <returns>
        /// JSON
        /// {
        ///      sucesso: [bool] caso a operação tenha sido realizada corretamente.
        ///      idReserva: [int] id da Reserva criada
        /// }
        /// </returns>
        [HttpPost]
        public ActionResult Criar(Reserva reserva)
        {
            try{
                db.Reserva.Add(reserva);
                db.SaveChanges();

                return Json(new { sucesso = true, idReserva = reserva.id });
            } catch(Exception ex)
            {
                return Json(new { sucesso = false });
            }
            
        }


        /// <summary>
        /// [GET]
        /// Adiciona Lista de Unidades a uma Reserva
        /// </summary>
        /// <param name="lista">[List[string]] lista contendo os IDs de Unidades para reserva</param>
        /// <param name="idReserva">[int] id da tabela Reserva</param>
        /// <returns>
        /// {
        ///     sucesso: [bool] indica se operação foi realizada corretamente.
        /// }
        /// </returns>
        public ActionResult AdicionaUnidadesReserva(List<string> lista, int idReserva)
        {
            try
            {
                foreach (var item in lista)
                {
                    Unidade_Reserva unidadeReserva = new Unidade_Reserva();

                    unidadeReserva.idReserva = idReserva;
                    unidadeReserva.idUnidade = Convert.ToInt32(item);

                    db.Unidade_Reserva.Add(unidadeReserva);
                    db.SaveChanges();

                    // Altera status unidade para reservada
                    var unidade = db.Unidade.Find(Convert.ToInt32(item));
                    unidade.idStatus_Unidade = 3;
                    db.Unidade.Attach(unidade);
                    db.Entry(unidade).Property(x => x.idStatus_Unidade).IsModified = true;
                    db.SaveChanges();
                }

                return Json(new { sucesso = true });
            } catch(Exception ex )
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
                return Json(new { sucesso = false });
            }
            
        }

        /// <summary>
        /// [GET]
        /// Exclui Reserva tornando as Unidades disponíveis.
        /// </summary>
        /// <param name="idReserva">[int] id da tabela Reserva</param>
        /// <returns>View Index</returns>
        public ActionResult Excluir(int idReserva)
        {
            try
            {
                var reserva = db.Reserva.Find(idReserva);

                var unidadesReserva = db.Unidade_Reserva.Where(x => x.idReserva == idReserva).ToList();

                // torna unidades disponíveis
                foreach (var item in unidadesReserva)
                {
                    var unidade = db.Unidade.Find(item.idUnidade);
                    unidade.idStatus_Unidade = 1;
                    db.Unidade.Attach(unidade);
                    db.Entry(unidade).Property(x => x.idStatus_Unidade).IsModified = true;
                    db.SaveChanges();

                    db.Unidade_Reserva.Remove(item);
                    db.SaveChanges();

                }


                // exclui reserva
                db.Reserva.Remove(reserva);
                db.SaveChanges();

                
            } catch(Exception ex)
            {
                // TODO
                // Tratamento de Exceções (SALVAR LOG)
            }

            return RedirectToAction("Index");
        }

    }
}