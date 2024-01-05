using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    internal class TCCompare : IComparer
    {
        public int Compare(object x, object y)
        {
            ListViewItem itemX = (ListViewItem)x;
            ListViewItem itemY = (ListViewItem)y;
            int yearOfX = CalculateYearsOfWork(itemX.SubItems[3].Text);
            int yearOfY = CalculateYearsOfWork(itemY.SubItems[3].Text);
            int compareYears = yearOfX.CompareTo(yearOfY);
            ChuaBenhThuCung cbtc = Form1.db.ChuaBenhThuCungs.Find(itemX.SubItems[0].Text);
            ChuaBenhThuCung cbtc2 = Form1.db.ChuaBenhThuCungs.Find(itemY.SubItems[0].Text);
            int canNangX = cbtc.CanNang;
            int canNangY = cbtc2.CanNang;
            if(compareYears == 0)
            {
                return canNangX.CompareTo(canNangY);
            }
            return compareYears;
        }
        private int CalculateYearsOfWork(string startDate)
        {
            DateTime dt;

            if (DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                DateTime currentDate = DateTime.Now;
                int yearsOfWork = currentDate.Year - dt.Year;

                if (currentDate < dt.AddYears(yearsOfWork))
                {
                    yearsOfWork--;
                }

                return yearsOfWork;
            }

            return 0;
        }
    }
}
