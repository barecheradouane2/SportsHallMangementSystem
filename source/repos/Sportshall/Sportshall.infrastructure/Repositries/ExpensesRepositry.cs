using AutoMapper;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Repositries
{
    public class ExpensesRepositry : GenericRepositry<Expenses>, IExpensesRepositry
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public ExpensesRepositry(AppDbContext Context, IMapper mapper) : base(Context)
        {
            context = Context;
            this.mapper = mapper;
        }



    }
    
}
