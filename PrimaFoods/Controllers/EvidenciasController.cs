using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntidadesDAL;
using Neat.Procedure;
using PagedList;

namespace PrimaFoods.Controllers
{
    public class EvidenciasController : Controller
    {
        private PrimaModel db = new PrimaModel();

        // GET: Evidencias
        public ActionResult Index(int? pagina)
        {
            int pageSize = 10;
            int pageNumber = pagina ?? 1;
            List<TabAnimais> animal = new List<TabAnimais>();
            List<Evidencias> evidencia = new List<Evidencias>();
            List<EvidenciaAnimais> lstEvidenciaAnimais = new List<EvidenciaAnimais>();
            EvidenciaAnimais gridView;
            int totalAnimaisAbatidos = 0;
            int totalFemeasAbatidas = 0;
            int totalMachosAbatidos = 0;
            int totalAnimaisAgio = 0;
            int totalAnimaisDesagio = 0;

            try
            {
                evidencia = db.Evidencias.ToList();
                animal = db.TabAnimais.ToList();

                var myObject = animal.Join(evidencia, animais => animais.Animal, evidencias => evidencias.Animal,
                    ((animais, evidencias) => new { animais, evidencias })).ToList();

                foreach (var item in myObject)
                {
                    gridView = new EvidenciaAnimais();

                    gridView.Animal = item.animais.Animal;
                    gridView.CodPedido = item.animais.CodPedido;
                    gridView.Peso = item.animais.Peso;
                    gridView.Sexo = item.animais.Sexo;
                    gridView.ValorBase = item.evidencias.ValorBase;
                    gridView.CalculoTotalDentes = item.evidencias.CalculoTotalDentes;
                    gridView.Motivo = item.evidencias.Motivo;
                    gridView.Fornecedor = item.animais.TabPedido.Fornecedor;



                    if (item.animais.Sexo == "M")
                    {
                        totalMachosAbatidos++;
                    }
                    if (item.animais.Sexo == "F")
                    {
                        totalFemeasAbatidas++;
                    }
                    if (item.evidencias.Motivo.Contains("acréscimo"))
                    {
                        totalAnimaisAgio++;
                    }
                    if (item.evidencias.Motivo.Contains("desconto"))
                    {
                        totalAnimaisDesagio++;
                    }
                    totalAnimaisAbatidos++;
                    lstEvidenciaAnimais.Add(gridView);
                }
                
                decimal percentMachos =  CalculaPercentual(totalAnimaisAbatidos, totalMachosAbatidos);
                decimal percentFemeas =  CalculaPercentual(totalAnimaisAbatidos, totalFemeasAbatidas);
                decimal percentAgio =  CalculaPercentual(totalAnimaisAbatidos, totalAnimaisAgio);
                decimal percentDesagio =  CalculaPercentual(totalAnimaisAbatidos, totalAnimaisDesagio);

                Session["PercentMachos"] = percentMachos;
                Session["PercentFemeas"] = percentFemeas;
                Session["PercentAgio"] = percentAgio;
                Session["PercentDesagio"] = percentDesagio;
                Session["QtdAnimaisAbatidos"] = totalAnimaisAbatidos;
                Session["QtdMachosAbatidos"] = totalMachosAbatidos;
                Session["QtdFemeasAbatidas"] = totalFemeasAbatidas;
                Session["QtdAnimaisAgio"] = totalAnimaisAgio;
                Session["QtdAnimaisDesagio"] = totalAnimaisDesagio;
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
            return View((lstEvidenciaAnimais.OrderBy(r => r.CodPedido)).ToPagedList(pageNumber, pageSize));
        }

        public decimal CalculaPercentual (int totalAbatidos, int totalCalcular)
        {
            decimal animasAbatidos = totalAbatidos;
            decimal calcularAbatidos = totalCalcular;

            decimal div = ( calcularAbatidos/animasAbatidos )*100;
            decimal percentual = Math.Round(div, 1);

            return percentual;
        }

        [HttpGet]
        public ActionResult Calcular()
        {
            SqlConnection conexao = null;
            try
            {
                conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PrimaModel"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_CALCULO_AGIO_DESAGIO_ANIMAIS", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexao.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
