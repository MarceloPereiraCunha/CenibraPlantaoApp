using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PlantaoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo cul = CultureInfo.CurrentCulture;
            DataTable tabelaTurnos = new DataTable();
            tabelaTurnos = RetornaTurno();          
        }

        public static DataTable RetornaTurno()
        {

            DataTable table = new DataTable();
            table.Columns.Add("NumeroSemana", typeof(int));
            table.Columns.Add("DataInicioSemana", typeof(DateTime));
            table.Columns.Add("DataFimSemana", typeof(DateTime));

            for (int numeroSemana = 1; numeroSemana < 53; numeroSemana++)
            {
                DateTime firstDayOfWeek = FirstDateOfWeek(DateTime.Now.Year, numeroSemana, CultureInfo.CurrentCulture);
                // 11/12/2012  
                //DateTime firstDayOfLastYearWeek = FirstDateOfWeek(2012, thisWeekNumber, CultureInfo.CurrentCulture);
                DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);
                table.Rows.Add(numeroSemana, firstDayOfWeek, lastDayOfWeek);
            }
           
            return table;
        }


        // this method is borrowed from http://stackoverflow.com/a/11155102/284240
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }
    }
}
