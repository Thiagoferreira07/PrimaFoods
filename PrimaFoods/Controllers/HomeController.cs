using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using EntidadesDAL;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Web.UI.WebControls;




namespace PrimaFoods.Controllers
{
    public class HomeController : Controller
    {
        private PrimaModel db = new PrimaModel();
        public ActionResult Index()
        {
            List<TabAnimais> Animais = new List<TabAnimais>();
            Animais = db.TabAnimais.OrderBy(u => u.Animal).ToList();
            int qtdDentes0 = 0;
            int qtdDentes6 = 0;
            int qtdDentes4 = 0;
            
            foreach (var animal in Animais)
            {
                if (animal.QtdDentes == 0)
                {
                    qtdDentes0++;
                }
                if (animal.QtdDentes >= 6)
                {
                    qtdDentes6++;
                }
                if (animal.QtdDentes == 2 || animal.QtdDentes == 4)
                {
                    qtdDentes4++;
                }
            }
            Highcharts columnChart = new Highcharts("columnchart");
            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Column,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.AliceBlue),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.LightBlue,
                BorderRadius = 0,
                BorderWidth = 2
            });
            columnChart.SetTitle(new Title()
            {
                Text = "0 Dentes - 6 ou mais Dentes - 2 ou 4 Dentes",

            });
            columnChart.SetSubtitle(new Subtitle()
            {
                Text = "Quantidade de animais abatidos de acordo com a quantidade de dentes"
            });

            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Text = "Quantidade de dentes", Style = "fontWeight: 'bold', fontSize: '17px'" },
                Categories = new[] { "Animais com " },


            });
            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "Volume",
                    Style = "fontWeight: 'bold', fontSize: '17px'"
                },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0,
                TickInterval = 50,
            });
            columnChart.SetLegend(new Legend
            {
                Enabled = true,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFADD8E6"))
            });
            columnChart.SetSeries(new Series[]
            {
                new Series{
                    Name = "0 Dentes",
                    Data = new Data(new object[] { qtdDentes0 }),
                    Color = Color.Red

                },
                new Series()
                {
                    Name = "6 ou mais Dentes",
                    Data = new Data(new object[] { qtdDentes6 }),
                    Color = Color.Yellow
                },
                new Series()
                {
                    Name = "2 ou 4 Dentes",
                    Data = new Data(new object[] { qtdDentes4 }),
                    Color = Color.Green
                }
            }
            );
            return View(columnChart);
        }
    }
}