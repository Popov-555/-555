using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp25.Model
{
    interface IDataProvider
    {
        IEnumerable<Plane> GetPlanes();
        IEnumerable<PlaneName> GetPlaneName();
        
    }
}
