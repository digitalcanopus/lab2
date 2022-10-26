using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICreate
    {
        public object Create(Type type);
        public bool Exist(Type type);
    }
}
