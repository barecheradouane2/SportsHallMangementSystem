using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Sportshall.Core.Entites;
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

        private readonly  UserManager<AppUser> _userManager;

        private readonly IEmailService _emailService;

        private readonly IGenerateToken token;

        private readonly SignInManager<AppUser> _signInManager;






        public UnitOfWork(AppDbContext _context, IMapper mapper, IImageMangementService imageMangementService, UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, IGenerateToken token)
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
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            this.token = token;
            Auth = new AuthRepositry(_userManager, _emailService, _signInManager,token);
         
        }
        public IPhotoRepositry photoRepositry { get; }

        public IActivitiesRepositry ActivitiesRepositry { get; }

        public IOffersRepositry OffersRepositry { get; }

        public IMembersRepositry MembersRepositry { get; }

        public IAttendancesRepositry AttendancesRepositry { get; }

        public IProductsRepositry ProductsRepositry { get; }

        public IExpensesRepositry ExpensesRepositry { get; }

        public IRevenuesRepositry RevenuesRepositry { get; }

        public IAuth Auth { get; }
    }
}
