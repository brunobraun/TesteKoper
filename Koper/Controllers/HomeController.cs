using Koper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koper.Controllers
{
    public class HomeController : Controller
    {
        KoperEntities db = new KoperEntities();

        /// <summary>
        /// Tela inicial do sistema.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {

            return View();
        }


        /// <summary>
        /// [GET]
        /// Dados para análise do usuário no DashBoard.
        /// </summary>
        /// <returns>
        /// JSON
        /// {
        ///     sucesso: [bool] caso a operação tenha sido realizada corretamente
        ///     unidadesDisponiveis: [int] quantidade de unidades disponíveis
        ///     unidadesIndisponiveis: [int] quantidade de unidades indisponíveis
        ///     totalEmpreendimentos: [int] quantidade de empreendimentos
        ///     totalReserva: [int] quantidade de reservas
        ///     valorTotalReserva: [double] valor total em reservas
        ///     valorTotalEmpreendimentos: [double] valor total de unidades de empreendimentos cadastrados
        /// }
        /// </returns>
        public ActionResult DashBoard()
        {
            var unidades = db.Unidade.ToList();

            var empreendimentos = db.Empreendimento.ToList();

            var reservas = db.Reserva.ToList();

            var unidadesDisponiveis = unidades.Where(x => x.idStatus_Unidade == 1).Count();

            var unidadesIndisponiveis = unidades.Where(a => a.idStatus_Unidade != 1).Count();

            var totalEmpreendimentos = empreendimentos.Count();

            var totalReserva = reservas.Count();

            var valorTotalReserva = unidades.Where(x => x.idStatus_Unidade == 3).Sum(a => a.valor);


            var valorTotalEmpreendimentos = unidades.Sum(a => a.valor);

            return Json(new { sucesso = true, unidadesDisponiveis, unidadesIndisponiveis, totalEmpreendimentos, totalReserva, valorTotalReserva, valorTotalEmpreendimentos }, JsonRequestBehavior.AllowGet);
        }

    }
}