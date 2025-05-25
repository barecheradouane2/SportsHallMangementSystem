using AutoMapper;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Repositries
{
    public class RevenuesRepositry : GenericRepositry<Revenues>, IRevenuesRepositry
    {
        private IMapper mapper;
        private readonly AppDbContext context;

        public RevenuesRepositry(AppDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
            this.context = context;
        }


    }
}
