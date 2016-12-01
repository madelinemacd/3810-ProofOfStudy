using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace FullyAssociativeCache
{
    class CacheRow
    {
        public int tag
        {
            get;
            set;
        }
        public bool vBit
        {
            get;
            set;
        }
        public int LRUVal
        {
            get;
            set;
        }
        public CacheRow()
        {
            vBit = false;
            tag = 0;
            LRUVal = 0;
        }
    }
}
