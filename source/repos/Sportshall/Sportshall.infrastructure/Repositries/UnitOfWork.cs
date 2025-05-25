using AutoMapper;
using Sportshall.Core.interfaces;
using Sportshall.Core.Services;
using Sportshall.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;
        private readonly IImageMangementService _imageMangementService;

        public UnitOfWork(AppDbContext _context, IMapper mapper, IImageMangementService imageMangementService)
        {
            this._context = _context;
            _mapper = mapper;
            _imageMangementService = imageMangementService;
            photoRepositry = new PhotoRepositry(_context);
            ActivitiesRepositry = new ActivitiesRepositry(_context, _mapper, _imageMangementService);
            OffersRepositry = new OffersRepositry(_context, _mapper);
            MembersRepositry = new MembersRepositry(_context, _mapper);
            AttendancesRepositry = new AttendancesRepositry(_context, _mapper);
            ProductsRepositry = new ProductsRepositry(_context, _mapper, _imageMangementService);
            ExpensesRepositry = new ExpensesRepositry(_context, _mapper);
            RevenuesRepositry = new RevenuesRepositry(_context, _mapper);


        }
        public IPhotoRepositry photoRepositry { get; }

        public IActivitiesRepositry ActivitiesRepositry { get; }

        public IOffersRepositry OffersRepositry { get; }

        public IMembersRepositry MembersRepositry { get; }

        public IAttendancesRepositry AttendancesRepositry { get; }

        public IProductsRepositry ProductsRepositry { get; }

        public IExpensesRepositry ExpensesRepositry { get; }

        public IRevenuesRepositry RevenuesRepositry { get; }



    }
}
