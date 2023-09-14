using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazun
{
    public interface IEmployee
    {
       string EmployeeId { get; set; }
       string Name { get; set; }
       string Position { get; set; }
       double HourlyRate { get; set; }
    }
}
