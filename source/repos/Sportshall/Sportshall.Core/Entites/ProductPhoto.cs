using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Entites
{
    public class ProductPhoto : BaseEntity<int>
    {
        public string ImageName { get; set; }

        [ForeignKey(nameof(ProductID))]
        public int ProductID { get; set; }
        public virtual Products Product { get; set; }
    }
}
