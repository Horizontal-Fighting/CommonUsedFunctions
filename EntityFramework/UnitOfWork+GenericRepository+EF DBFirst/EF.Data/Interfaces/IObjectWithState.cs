using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Data.Interfaces
{
    public interface IObjectWithState
    {
        State State { get; set; }
    }
}
