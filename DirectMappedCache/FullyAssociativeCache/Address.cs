using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullyAssociativeCache
{
    class Address
    {
        public int tag
        {
            get;
            set;
        }
        public int offset
        {
            get;
            set;
        }
        public int value
        {
            get;
            set;
        }

        public Address(int address)
        {
            tag = address / 8;
            offset = address % 8;
            value = address;
        }
    }
}
