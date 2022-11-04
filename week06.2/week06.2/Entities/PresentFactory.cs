using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06._2.Abstractions;

namespace week06._2.Entities
{
    public class PresentFactory : IToyFactory
    {
        public Toy CreateNew()
        {
            return new Present();

        }
    }
}
