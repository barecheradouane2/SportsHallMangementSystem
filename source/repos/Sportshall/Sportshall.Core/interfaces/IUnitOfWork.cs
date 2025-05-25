using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.interfaces
{
    public interface IUnitOfWork
    {
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
