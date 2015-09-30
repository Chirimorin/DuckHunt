using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.exceptions
{
    public class LevelNot0Exception : Exception
    {
        public LevelNot0Exception() : base(String.Format("Missing partner token")) { }
    }
}
