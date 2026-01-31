using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texon.Service.specfications
{
    public class GetProductByIdsSpec(List<int> ids) : BaseSpecfications<Product>(id => ids.Contains(id.Id));
    
}
