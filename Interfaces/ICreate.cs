using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICreate
    {
        object Create(Type type);
        bool Exist(Type type);
    }
}
