using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetAssociativeCache
{
    class Address
    {
        public int tag
        {
            get;
            set;
        }
        public int row
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
            tag = address / 64;
            row = (address / 16) % 4;
            offset = address % 16;
            value = address;
        }
    }
}
