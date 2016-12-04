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
            //initialize cache
            CacheRow[] fullyAssociativeCache = new CacheRow[8];
            for (int i = 0; i < fullyAssociativeCache.Length; i++)
            {
                fullyAssociativeCache[i] = new CacheRow();
            }

            //ommitted because it's a repeat of the code from the direct mapped cache
            #region declaring address arrays
            Address[] ProofOfStudyAdresses = new Address[27];
            ProofOfStudyAdresses[0] = new Address(4);
            ProofOfStudyAdresses[1] = new Address(8);
            ProofOfStudyAdresses[2] = new Address(20);
            ProofOfStudyAdresses[3] = new Address(24);
            ProofOfStudyAdresses[4] = new Address(28);
            ProofOfStudyAdresses[5] = new Address(36);
            ProofOfStudyAdresses[6] = new Address(44);
            ProofOfStudyAdresses[7] = new Address(20);
            ProofOfStudyAdresses[8] = new Address(24);
            ProofOfStudyAdresses[9] = new Address(28);
            ProofOfStudyAdresses[10] = new Address(36);
            ProofOfStudyAdresses[11] = new Address(40);
            ProofOfStudyAdresses[12] = new Address(44);
            ProofOfStudyAdresses[13] = new Address(68);
            ProofOfStudyAdresses[14] = new Address(72);
            ProofOfStudyAdresses[15] = new Address(92);
            ProofOfStudyAdresses[16] = new Address(96);
            ProofOfStudyAdresses[17] = new Address(100);
            ProofOfStudyAdresses[18] = new Address(104);
            ProofOfStudyAdresses[19] = new Address(108);
            ProofOfStudyAdresses[20] = new Address(112);
            ProofOfStudyAdresses[21] = new Address(100);
            ProofOfStudyAdresses[22] = new Address(112);
            ProofOfStudyAdresses[23] = new Address(116);
            ProofOfStudyAdresses[24] = new Address(120);
            ProofOfStudyAdresses[25] = new Address(128);
            ProofOfStudyAdresses[26] = new Address(140);

            //76 92 96 100 104 108 112 120 124 128 144 148

            Address[] ExampleAddresses = new Address[28];
            ExampleAddresses[0] = new Address(16);
            ExampleAddresses[1] = new Address(20);
            ExampleAddresses[2] = new Address(24);
            ExampleAddresses[3] = new Address(28);
            ExampleAddresses[4] = new Address(32);
            ExampleAddresses[5] = new Address(36);
            ExampleAddresses[6] = new Address(60);
            ExampleAddresses[7] = new Address(64);
            ExampleAddresses[8] = new Address(56);
            ExampleAddresses[9] = new Address(60);
            ExampleAddresses[10] = new Address(64);
            ExampleAddresses[11] = new Address(68);
            ExampleAddresses[12] = new Address(56);
            ExampleAddresses[13] = new Address(60);
            ExampleAddresses[14] = new Address(64);
            ExampleAddresses[15] = new Address(72);
            ExampleAddresses[16] = new Address(76);
            ExampleAddresses[17] = new Address(92);
            ExampleAddresses[18] = new Address(96);
            ExampleAddresses[19] = new Address(100);
            ExampleAddresses[20] = new Address(104);
            ExampleAddresses[21] = new Address(108);
            ExampleAddresses[22] = new Address(112);
            ExampleAddresses[23] = new Address(120);
            ExampleAddresses[24] = new Address(124);
            ExampleAddresses[25] = new Address(128);
            ExampleAddresses[26] = new Address(144);
            ExampleAddresses[27] = new Address(148);

            #endregion

            Address[] addresses = ExampleAddresses;

            int totalCycles = 0;
            int totalLookups = 0;
            int misses = 0;

            //do the first loop so we don't get those numbers included in analysis
            for (int i = 0; i < addresses.Length; i++)
            {
                performLookup(fullyAssociativeCache, addresses[i], ref misses, i);
            }

            //perform lookups
            for (int numLoops = 10; numLoops > 0; numLoops--)
            {
                misses = 0;
                for (int i = 0; i < addresses.Length; i++)
                {
                    totalCycles += performLookup(fullyAssociativeCache, addresses[i], ref misses, i);
                    totalLookups++;
                }
            }
            Console.ReadLine();
        }

        private static int performLookup(CacheRow[] fullyAssociativeCache, Address currAdd, ref int misses, int sequenceNum)
        {
            //look through all rows for a matching tag
            for (int i = 0; i < fullyAssociativeCache.Length; i++)
            {
                CacheRow row = fullyAssociativeCache[i];
                if (currAdd.tag == row.tag && row.vBit)
                {
                    row.LRUVal = sequenceNum;
                    return 1;
                }
            }
            //if there was a miss find the row with the lowest LRU
            int lowestLRUPosition = 1;
            int lowestLRUSoFar = int.MaxValue;
            for (int i = 0; i < fullyAssociativeCache.Length; i++)
            {
                CacheRow row = fullyAssociativeCache[i];
                if (row.LRUVal < lowestLRUSoFar)
                {
                    lowestLRUSoFar = row.LRUVal;
                    lowestLRUPosition = i;
                }
            }
            //Load new info into the row with the lowest LRU
            misses++;
            fullyAssociativeCache[lowestLRUPosition].vBit = true;
            fullyAssociativeCache[lowestLRUPosition].tag = currAdd.tag;
            fullyAssociativeCache[lowestLRUPosition].LRUVal = sequenceNum;
            return 28;
        }
    }
    //stores information about a cache row
    class CacheRow
    {
        public int tag;
        public bool vBit;
        public int LRUVal;
        public CacheRow()
        {
            vBit = false;
            tag = 0;
            LRUVal = -1;
        }
    }
    //stores information about an address
    class Address
        {
            public int tag;
            public int offset;
            public int value;
            public Address(int address)
            {
                tag = address / 8;
                offset = address % 8;
                value = address;
            }
        }
}
