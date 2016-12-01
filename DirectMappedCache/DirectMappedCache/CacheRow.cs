using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace DirectMappedCache
{
    class CacheRow
    {
        public int tag
        {
            get;
            set;
        }
        public Int32[] data
        {
            get;
            set;
        }
        public bool vBit
        {
            get;
            set;
        }
        public CacheRow()
        {
            vBit = false;
            data = new Int32[4];
            tag = 0;
        }
    }
}
