using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullyAssociativeCache
{
    class Program
    {
        static void Main(string[] args)
        {
            CacheRow[] fullyAssociativeCache = new CacheRow[8];
            for (int i = 0; i < fullyAssociativeCache.Length; i++)
            {
                fullyAssociativeCache[i] = new CacheRow();
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
            int LRUCount = 0;

            for (int i = 0; i < addresses.Length; i++)
            {
                performLookup(fullyAssociativeCache, addresses[i], ref misses, i);
            }
            Console.WriteLine("\n\t\tStarting Second Loop\n");
            for (int numLoops = 6; numLoops > 0; numLoops--)
            {
                misses = 0;
                for (int i = 0; i < addresses.Length; i++)
                {
                    totalCycles += performLookup(fullyAssociativeCache, addresses[i], ref misses, i);
                    totalLookups++;
                }
            }
            Console.WriteLine("Misses: {0} \nHits: {1} \nInstructions: {2}", misses, addresses.Length - misses, addresses.Length);
            Console.WriteLine("Total Cycles: {0} \n Total Instructions: {1}", totalCycles, totalLookups);
            Console.WriteLine("Cycles per lookup = " + (double)(totalCycles) / (double)(totalLookups));
            outputCache(fullyAssociativeCache);
            Console.ReadLine();
        }

        private static int performLookup(CacheRow[] fullyAssociativeCache, Address currAdd, ref int misses, int sequenceNum)
        {
            Console.Write("Accessing {0}(tag {1}):", currAdd.value, currAdd.tag);
            for (int i = 0; i < fullyAssociativeCache.Length;i++)
            {
                CacheRow row = fullyAssociativeCache[i];
                if (currAdd.tag == row.tag && row.vBit)
                {
                    row.LRUVal = sequenceNum;
                    Console.WriteLine("hit from row {0}", i);
                    return 1;
                }
            }
            //now find the LRU
            Console.Write("miss");
            int lowestLRUPosition = 1;
            int lowestLRUSoFar = int.MaxValue;
            for (int i = 0; i < fullyAssociativeCache.Length; i++)
            {
                CacheRow row = fullyAssociativeCache[i];
                if (!row.vBit)
                {
                    Console.Write(" - found invalid row");
                    lowestLRUPosition = i;
                    break;
                }
                if (row.LRUVal < lowestLRUSoFar)
                {
                    lowestLRUSoFar = row.LRUVal;
                    lowestLRUPosition = i;
                }
            }
            Console.WriteLine(" - cached to row {0}", lowestLRUPosition);
            misses++;
            fullyAssociativeCache[lowestLRUPosition].vBit = true;
            fullyAssociativeCache[lowestLRUPosition].tag = currAdd.tag;
            fullyAssociativeCache[lowestLRUPosition].LRUVal = sequenceNum;
            return 42;
        }

        private static void outputCache(CacheRow[] fullyAssociativeCache)
        {
            Console.Write("Valid\tTag\tLRU");
            for (int i = 0; i < fullyAssociativeCache.Length; i++)
            {
                Console.Write("\n" + (fullyAssociativeCache[i].vBit ? 1 : 0) + "\t"
                    + fullyAssociativeCache[i].tag.ToString() + "\t"
                    + fullyAssociativeCache[i].LRUVal);
            }
        }
    }
}
