using Sportshall.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{
    public record ExpensesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; } 
        public string? Note { get; set; } 

        public Expensestype Type { get; set; } 

        public string TypeName { get; set; }


        //public int TypeId { get; set; } 
        //public string TypeName { get; set; } = string.Empty;
    }


    public record AddExpensesDTO
    {
        public string Name { get; set; } 
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string? Note { get; set; } 

        public Expensestype Type { get; set; }


        public string TypeName => Type.ToString();
    }

    public record UpdateExpensesDTO : AddExpensesDTO
    {
      
        public Expensestype Type { get; set; }

    }




    }
