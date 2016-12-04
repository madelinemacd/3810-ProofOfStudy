using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectMappedCache
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize cache
            CacheRow[] directMappedCache = new CacheRow[4];
            for (int i = 0; i < 4; i++)
            {
                directMappedCache[i] = new CacheRow();
            }

            //create list of addresses
            Address[] addresses = new Address[27];
            addresses[0] = new Address(4);
            addresses[1] = new Address(8);
            addresses[2] = new Address(20);
            addresses[3] = new Address(24);
            addresses[4] = new Address(28);
            addresses[5] = new Address(36);
            addresses[6] = new Address(44);
            addresses[7] = new Address(20);
            addresses[8] = new Address(24);
            addresses[9] = new Address(28);
            addresses[10] = new Address(36);
            addresses[11] = new Address(40);
            addresses[12] = new Address(44);
            addresses[13] = new Address(68);
            addresses[14] = new Address(72);
            addresses[15] = new Address(92);
            addresses[16] = new Address(96);
            addresses[17] = new Address(100);
            addresses[18] = new Address(104);
            addresses[19] = new Address(108);
            addresses[20] = new Address(112);
            addresses[21] = new Address(100);
            addresses[22] = new Address(112);
            addresses[23] = new Address(116);
            addresses[24] = new Address(120);
            addresses[25] = new Address(128);
            addresses[26] = new Address(140);

            //perform lookups and collect data
            int totalCycles = 0;
            int totalLookups = 0;
            int misses = 0;

            for (int i = 0; i < 27; i++)
            {
                performLookup(directMappedCache, addresses[i], ref misses);
            }

            for (int numLoops = 30; numLoops > 0; numLoops--)
            {
                misses = 0;
                for(int i = 0; i < 27;i++)
                {
                    totalCycles += performLookup(directMappedCache, addresses[i], ref misses);
                    totalLookups++;
                }
            }
            Console.ReadLine();
        }

        private static int performLookup(CacheRow[] directMappedCache, Address currAdd, ref int misses)
        {
            //check if there was a hit
            if (directMappedCache[currAdd.row].vBit)
            {
                if (directMappedCache[currAdd.row].tag == currAdd.tag)
                {
                    return 1;
                }
            }
            //in case of miss update row
            misses++;
            directMappedCache[currAdd.row].vBit = true;
            directMappedCache[currAdd.row].tag = currAdd.tag;
            return 66;
        }
    }
    //stores information about a row in the cache
    class CacheRow
    {
        public int tag;
        public bool vBit;
        public CacheRow()
        {
            vBit = false;
            tag = 0;
        }
    }
    //stores information about the address
    class Address
    {
        public int tag;
        public int row;
        public int offset;
        public int value;

        public Address(int address)
        {
            tag = address / 64;
            row = (address / 16) % 4;
            offset = address % 16;
            value = address;
        }
    }
}
