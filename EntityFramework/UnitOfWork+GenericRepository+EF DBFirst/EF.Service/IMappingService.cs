using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Service
{
    public interface IMappingService
    {
        TR Map<TS, TR>(TS source);
    }
}
