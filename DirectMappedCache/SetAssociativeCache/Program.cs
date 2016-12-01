using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetAssociativeCache
{
    class Program
    {
        static void Main(string[] args)
        {
            CacheRow[] setAssociativeCache = new CacheRow[4];
            for (int i = 0; i < 4; i++)
            {
                setAssociativeCache[i] = new CacheRow();
            }

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

            //76 92 96 100 104 108 112 120 124 128 144 148

            //Address[] addresses = new Address[28];
            //addresses[0] = new Address(16);
            //addresses[1] = new Address(20);
            //addresses[2] = new Address(24);
            //addresses[3] = new Address(28);
            //addresses[4] = new Address(32);
            //addresses[5] = new Address(36);
            //addresses[6] = new Address(60);
            //addresses[7] = new Address(64);
            //addresses[8] = new Address(56);
            //addresses[9] = new Address(60);
            //addresses[10] = new Address(64);
            //addresses[11] = new Address(68);
            //addresses[12] = new Address(56);
            //addresses[13] = new Address(60);
            //addresses[14] = new Address(64);
            //addresses[15] = new Address(72);
            //addresses[16] = new Address(76);
            //addresses[17] = new Address(92);
            //addresses[18] = new Address(96);
            //addresses[19] = new Address(100);
            //addresses[20] = new Address(104);
            //addresses[21] = new Address(108);
            //addresses[22] = new Address(112);
            //addresses[23] = new Address(120);
            //addresses[24] = new Address(124);
            //addresses[25] = new Address(128);
            //addresses[26] = new Address(144);
            //addresses[27] = new Address(148);

            //make an array of addresses
            //iterate through the addresses
            //have a method called perform search, have it add up the number of cycles

            int totalCycles = 0;
            int totalLookups = 0;
            int misses = 0;

            for (int i = 0; i < 27; i++)
            {
                performLookup(setAssociativeCache, addresses[i], ref misses);
            }

            for (int numLoops = 30; numLoops > 0; numLoops--)
            {
                misses = 0;
                for (int i = 0; i < 27; i++)
                {
                    totalCycles += performLookup(setAssociativeCache, addresses[i], ref misses);
                    totalLookups++;
                }
            }
            Console.WriteLine("Misses: {0} \nHits: {1} \nInstructions: {2}", misses, addresses.Length - misses, addresses.Length);
            Console.WriteLine("Total Cycles: {0} \n Total Instructions: {1}", totalCycles, totalLookups);
            Console.WriteLine("Cycles per lookup = " + (double)(totalCycles) / (double)(totalLookups));
            outputCache(setAssociativeCache);
            Console.ReadLine();
        }

        private static int performLookup(CacheRow[] directMappedCache, Address currAdd, ref int misses)
        {
            Console.Write("Accessing {0}(tag {1}):", currAdd.value, currAdd.tag);
            if (directMappedCache[currAdd.row].vBit)
            {
                if (directMappedCache[currAdd.row].tag == currAdd.tag)
                {
                    Console.WriteLine("hit from row {0}", currAdd.row);
                    return 1;
                }
            }
            Console.WriteLine("miss - cached to row {0}", currAdd.row);
            misses++;
            directMappedCache[currAdd.row].vBit = true;
            directMappedCache[currAdd.row].tag = currAdd.tag;
            return 66;
        }

        private static void outputCache(CacheRow[] directMappedCache)
        {
            Console.Write("Valid\tTag\tData");
            for (int i = 0; i < 4; i++)
            {
                Console.Write("\n" + (directMappedCache[i].vBit ? 1 : 0) + "\t"
                    + directMappedCache[i].tag.ToString());
                //for (int j = 0; j < 16; j++)
                //{
                //    Console.Write('a'+i + "\t");
                //}
            }
        }
    }
}
