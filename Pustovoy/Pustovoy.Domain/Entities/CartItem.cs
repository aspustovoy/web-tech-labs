using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustovoy.Domain.Entities
{
	public class CartItem
    {
        public Dish Item { get; set; }
        public int Qty { get; set; }
    }
}
