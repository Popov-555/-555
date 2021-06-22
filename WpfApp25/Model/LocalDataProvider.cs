using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp25.Model
{
    public class LocalDataProvider : IDataProvider
    {

        public IEnumerable<Plane> GetPlanes()

        {
            return new Plane[] {
                new Plane{Name="Airbus A320neo",Price="1900",Fuel="700" },
                new Plane { Name = "Boeing 777X", Price = "3000", Fuel = "900" },
                new Plane { Name = "Boeing 737 MAX", Price = "2000", Fuel = "1100" }
            };
        }
        public IEnumerable<PlaneName> GetPlaneName()
        {
            return new PlaneName[] {
        new PlaneName{ Title="Airbus A320neo" },
        new PlaneName{ Title="Boeing 777X" },
        new PlaneName{ Title="Boeing 737 MAX" },
    };
        }
    }
}

