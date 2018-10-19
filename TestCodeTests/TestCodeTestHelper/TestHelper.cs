using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCode.Models;

namespace TestCodeTests.TestCodeTestHelper
{
    public class TeamValueComparer : IEqualityComparer<TeamValue>
    {
        public bool Equals(TeamValue x, TeamValue y)
        {
            return x?.Id == y?.Id;
        }

        public int GetHashCode(TeamValue obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
