using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub
{
    class InvoiceDetail
    {
        private int qty;

        public int Qty { get => qty; set => qty = value; }
        public int PID { get; set; }

        public InvoiceDetail(int qty, int pID)
        {
            Qty = qty;
            PID = pID;
        }
    }
    
}
