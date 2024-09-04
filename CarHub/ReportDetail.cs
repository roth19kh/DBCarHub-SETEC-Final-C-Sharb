using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub
{
    public class ReportDetail
    {
        public int No { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double Amount { get { return Price * Qty; } }

        public ReportDetail()
        {

        }
        public ReportDetail(int no, string productName, double price, int qty)
        {
            No = no;
            ProductName = productName;
            Price = price;
            Qty = qty;
        }
    }
}
