using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyShop.Data;
using TrendyShop.Models;
using TrendyShop.Utility;
using TrendyShop.ViewModels;
//using Syncfusion.XlsIO;                              PAQUETE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
using System.IO;

namespace TrendyShop.Controllers
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class StatisticsController : Controller
    {
        private DataContext context;


        public StatisticsController(DataContext ctx)
        {
            context = ctx;
        }

        static List<YearStatisticsViewModel> allYearStatisitics;

        public IActionResult Index()
        {

            var lodgings = Statics.GetLodgings(context).GroupBy(l => l.Date.Year);
            List<int> years = new List<int>();

            foreach (var item in lodgings)
            {
                years.Add(item.Key);
            }
            years.Sort();

            allYearStatisitics = new List<YearStatisticsViewModel>();
            foreach (var year in years)
            {
                allYearStatisitics.Add(GetYearStatistics(year, context));
            }

            return View(allYearStatisitics);
        }


        //public IActionResult ExportToExcel(int year, YearStatisticsViewModel yearSs)
        //{
        //    List<YearStatistics> yearStatisticsTable = new List<YearStatistics>();
        //    YearStatisticsViewModel yearS = allYearStatisitics.Find(y => y.Year == year);

        //    for (int i = 1; i < 13; i++)
        //    {
        //        YearStatistics row = new YearStatistics
        //        {
        //            Month = yearS.Months[i],
        //            Lodgings = yearS.LodgingsPerMonth[0],
        //            DoubleLodgings = yearS.DoubleLodgingsPerMonth[i],
        //            DailyLodgings = yearS.DailyLodgingsPerMonth[i],
        //            RentIncome = yearS.RentIncomePerMoth[i],
        //            ConsumptionIncome = yearS.ConsumptionIncomePerMonth[i],
        //            ConsumptionProfit = yearS.ConsumptionProfitPerMonth[i],
        //            DamageIncome = yearS.DamageIncomePerMoth[i],
        //            GrossIncome = yearS.GrossIncomePerMonth[i],
        //            Salary = yearS.SalaryPerMoth[i],
        //            HouseExpenses = yearS.HouseExpensesPerMoth[i],
        //        };
        //        yearStatisticsTable.Add(row);
        //    }

        //    using (ExcelEngine excelEngine = new ExcelEngine())
        //    {
        //        IApplication application = excelEngine.Excel;
        //        application.DefaultVersion = ExcelVersion.Excel2013;

        //        //Create a new workbook
        //        IWorkbook workbook = application.Workbooks.Create(1);
        //        IWorksheet sheet = workbook.Worksheets[0];


        //        sheet.ImportData(yearStatisticsTable, 2, 1, false);

        //        sheet.Range["A1"].Text = year.ToString();
        //        sheet.Range["B1"].Text = "Reservas";
        //        sheet.Range["C1"].Text = "Reservas Dobles";
        //        sheet.Range["D1"].Text = "Reservas Diarias";
        //        sheet.Range["E1"].Text = "Ingreso x Renta";
        //        sheet.Range["F1"].Text = "Consumo";
        //        sheet.Range["G1"].Text = "Ganacia x Consumo";
        //        sheet.Range["H1"].Text = "Ingreso x Daños";
        //        sheet.Range["I1"].Text = "Ingreso Bruto";
        //        sheet.Range["J1"].Text = "Salario";
        //        sheet.Range["K1"].Text = "Otros Gastos";

        //        //Saving
        //        string name = "Estadísticas" + " " + year.ToString() + ".xlsx";
        //        string path = @"C:\Users\Sandra\Desktop\Mi Carreta  actual\" + name;

        //        FileStream file = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
        //        workbook.SaveAs(file);

        //        file.Dispose();
        //    }

        //    return RedirectToAction("Index", "Statistics");
        //}


        static YearStatisticsViewModel GetYearStatistics(int year, DataContext context)
        {
            int[] m = new int[] { 1, 3, 5, 7, 8, 10, 12 };

            int[] lodgingsPerMonth = new int[13];
            string[] dailyLodgingsPerMonth = new string[13];
            int[] doubleLodgingsPerMonth = new int[13];
            float[] consumptionIncomePerMonth = new float[13];
            float[] ConsumptionProfitPerMonth = new float[13];
            float[] salaryPerMonth = new float[13];
            float[] allHouseExpensesPerMonth = new float[13];
            float[] rentIncomePerMonth = new float[13];
            float[] damageIncomePerMonth = new float[13];
            float[] grossIncomePerMonth = new float[13];

            //To round
            string[] consumptionIncomePerMonthStr = new string[13];
            string[] ConsumptionProfitPerMonthStr = new string[13];
            string[] salaryPerMonthStr = new string[13];
            string[] allHouseExpensesPerMonthStr = new string[13];
            string[] rentIncomePerMonthStr = new string[13];
            string[] damageIncomePerMonthStr = new string[13];
            string[] grossIncomePerMonthStr = new string[13];


            for (int i = 1; i < lodgingsPerMonth.Length; i++)
            {
                DateTime initialDate = new DateTime(year, i, 1);
                DateTime finalDate;

                if (m.Contains(i))
                {
                    finalDate = new DateTime(year, i, 31);
                }
                else if (i == 2)
                {
                    if (Statics.IsLeapYear(year))
                    {
                        finalDate = new DateTime(year, i, 29);

                    }
                    else
                    {
                        finalDate = new DateTime(year, i, 28);
                    }
                }
                else
                {
                    finalDate = new DateTime(year, i, 30);
                }


                var lodgings = Statics.GetLodgings(context, initialDate, finalDate);
                lodgingsPerMonth[i] = lodgings.Count;                                       //

                dailyLodgingsPerMonth[i] = String.Format("{0:0.00}", ((float)lodgingsPerMonth[i] / (float)finalDate.Day));       //   Cambiado a string

                var doubleLodg = lodgings.FindAll(dl => dl.IsDouble == true);        //
                if (doubleLodg == null)
                {
                    doubleLodgingsPerMonth[i] = 0;
                }
                else { doubleLodgingsPerMonth[i] = doubleLodg.Count; }

                consumptionIncomePerMonth[i] = Statics.GetPurchasedProductsIncome(context, initialDate, finalDate);  //

                ConsumptionProfitPerMonth[i] = Statics.GetPurchasedProductsProfit(context, initialDate, finalDate);   //

                var employees = context.Employees.ToList();                        //
                foreach (Employee e in employees)
                {
                    salaryPerMonth[i] += Statics.GetEmployeeSalary(context, initialDate, finalDate, e.EmployeeId);
                }

                allHouseExpensesPerMonth[i] = Statics.GetMaintenanceExpenses(context, initialDate, finalDate) + Statics.GetHouseExpenses(context, initialDate, finalDate);    //

                rentIncomePerMonth[i] = (float)lodgings.Sum(l => l.RentCost);                  //

                damageIncomePerMonth[i] = Statics.GetIncomePerDamage(context, initialDate, finalDate);    //

                grossIncomePerMonth[i] = consumptionIncomePerMonth[i] + rentIncomePerMonth[i] + damageIncomePerMonth[i];    //

            }

            for (int i = 1; i < lodgingsPerMonth.Length; i++)
            {
                consumptionIncomePerMonthStr[i] = String.Format("{0:0.00}", consumptionIncomePerMonth[i]);
                ConsumptionProfitPerMonthStr[i] = String.Format("{0:0.00}", ConsumptionProfitPerMonth[i]);
                salaryPerMonthStr[i] = String.Format("{0:0.00}", salaryPerMonth[i]);
                allHouseExpensesPerMonthStr[i] = String.Format("{0:0.00}", allHouseExpensesPerMonth[i]);
                rentIncomePerMonthStr[i] = String.Format("{0:0.00}", rentIncomePerMonth[i]);
                damageIncomePerMonthStr[i] = String.Format("{0:0.00}", damageIncomePerMonth[i]);
                grossIncomePerMonthStr[i] = String.Format("{0:0.00}", grossIncomePerMonth[i]);

            }



            YearStatisticsViewModel ys = new YearStatisticsViewModel
            {
                Year = year,
                Months = new string[] { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" },
                LodgingsPerMonth = lodgingsPerMonth,
                AnnualLodgings = lodgingsPerMonth.Sum(),
                DoubleLodgingsPerMonth = doubleLodgingsPerMonth,
                AnnualDoubleLodgings = doubleLodgingsPerMonth.Sum(),
                DailyLodgingsPerMonth = dailyLodgingsPerMonth,
                RentIncomePerMoth = rentIncomePerMonthStr,
                AnnualRentIncome = String.Format("{0:0.00}", rentIncomePerMonth.Sum()),
                ConsumptionIncomePerMonth = consumptionIncomePerMonthStr,
                AnnualConsumptionIncome = String.Format("{0:0.00}", consumptionIncomePerMonth.Sum()),
                ConsumptionProfitPerMonth = consumptionIncomePerMonthStr,
                AnnualConsumptionProfit = String.Format("{0:0.00}", ConsumptionProfitPerMonth.Sum()),
                DamageIncomePerMoth = damageIncomePerMonthStr,
                AnnualDamageIncome = String.Format("{0:0.00}", damageIncomePerMonth.Sum()),
                GrossIncomePerMonth = grossIncomePerMonthStr,
                AnnualGrossIncome = String.Format("{0:0.00}", grossIncomePerMonth.Sum()),
                SalaryPerMoth = salaryPerMonthStr,
                HouseExpensesPerMoth = allHouseExpensesPerMonthStr,
                AnnualHouseExpenses = String.Format("{0:0.00}", allHouseExpensesPerMonth.Sum()),
                AnnualSalary = String.Format("{0:0.00}", salaryPerMonth.Sum()),
            };

            return ys;
        }



    }
}
