using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public class Expenses : BaseEntity<int>
    {

        public Expenses() { }

        public Expenses(Expensestype type,string name,decimal totalPrice, DateTime date,string note) {

            Type = type;
            Name = name;
            TotalPrice = totalPrice;
            Date = date;
            Note = note;

        }

        public Expensestype Type { get; set; } = Expensestype.oneTime;  
        public string Name { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime Date { get; set; } = DateTime.Today;

        public string? Note { get; set; }







    }
}
