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
            SetAssociativeCacheRow[] setAssociativeCache = new SetAssociativeCacheRow[8];
            for (int i = 0; i < 8; i++)
            {
                setAssociativeCache[i] = new SetAssociativeCacheRow();
            }

            #region address definitions
            //Address[] addresses = new Address[27];
            //addresses[0] = new Address(4);
            //addresses[1] = new Address(8);
            //addresses[2] = new Address(20);
            //addresses[3] = new Address(24);
            //addresses[4] = new Address(28);
            //addresses[5] = new Address(36);
            //addresses[6] = new Address(44);
            //addresses[7] = new Address(20);
            //addresses[8] = new Address(24);
            //addresses[9] = new Address(28);
            //addresses[10] = new Address(36);
            //addresses[11] = new Address(40);
            //addresses[12] = new Address(44);
            //addresses[13] = new Address(68);
            //addresses[14] = new Address(72);
            //addresses[15] = new Address(92);
            //addresses[16] = new Address(96);
            //addresses[17] = new Address(100);
            //addresses[18] = new Address(104);
            //addresses[19] = new Address(108);
            //addresses[20] = new Address(112);
            //addresses[21] = new Address(100);
            //addresses[22] = new Address(112);
            //addresses[23] = new Address(116);
            //addresses[24] = new Address(120);
            //addresses[25] = new Address(128);
            //addresses[26] = new Address(140);


            Address[] addresses = new Address[28];
            addresses[0] = new Address(16);
            addresses[1] = new Address(20);
            addresses[2] = new Address(24);
            addresses[3] = new Address(28);
            addresses[4] = new Address(32);
            addresses[5] = new Address(36);
            addresses[6] = new Address(60);
            addresses[7] = new Address(64);
            addresses[8] = new Address(56);
            addresses[9] = new Address(60);
            addresses[10] = new Address(64);
            addresses[11] = new Address(68);
            addresses[12] = new Address(56);
            addresses[13] = new Address(60);
            addresses[14] = new Address(64);
            addresses[15] = new Address(72);
            addresses[16] = new Address(76);
            addresses[17] = new Address(92);
            addresses[18] = new Address(96);
            addresses[19] = new Address(100);
            addresses[20] = new Address(104);
            addresses[21] = new Address(108);
            addresses[22] = new Address(112);
            addresses[23] = new Address(120);
            addresses[24] = new Address(124);
            addresses[25] = new Address(128);
            addresses[26] = new Address(144);
            addresses[27] = new Address(148);

            #endregion

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
            //outputCache(setAssociativeCache);
            Console.ReadLine();
        }
        //first use direct mapped cache code to determine a row
        //then go to another method to look through the options in each row and choose one

        private static int performLookup(SetAssociativeCacheRow[] setAssociativeCache, Address currAdd, ref int misses)
        {
            //Console.Write("Accessing {0}(tag {1}):", currAdd.value, currAdd.tag);
            CacheDataBlock[] currRowElements = setAssociativeCache[currAdd.row].rowElements;
            //have the row, not perform checks for validity

            //neither block is valid
            if (!currRowElements[0].vBit && !currRowElements[1].vBit)
            {
                return loadMemIntoBlock(currRowElements, true, currAdd.tag, ref misses);
            } 
            //first block is valid  
            else if (currRowElements[0].vBit && !currRowElements[1].vBit)
            {
                if (currRowElements[0].tag == currAdd.tag)
                {
                    currRowElements[0].LRUVal = 1;
                    currRowElements[1].LRUVal = 0;
                    return 1;
                }
                else
                {
                    return loadMemIntoBlock(currRowElements, false, currAdd.tag, ref misses);
                }
            }
            //second block is valid
            else if (!currRowElements[0].vBit && currRowElements[1].vBit)
            {
                if (currRowElements[1].tag == currAdd.tag)
                {
                    currRowElements[1].LRUVal = 1;
                    currRowElements[0].LRUVal = 0;
                    return 1;
                }
                else
                {
                    return loadMemIntoBlock(currRowElements, true, currAdd.tag, ref misses);
                }
            }
            //both blocks are valid
            else //both valid
            {
                return twoValidBlocksInSet(currRowElements, currAdd.tag,ref misses);
            }
        }

        private static int twoValidBlocksInSet(CacheDataBlock[] currRowElements, int tag, ref int misses)
        {
            //check both tags
            if (currRowElements[0].tag == tag)
            {
                currRowElements[0].LRUVal = 1;
                currRowElements[1].LRUVal = 0;
                return 1;
            }
            else if (currRowElements[1].tag == tag)
            {
                currRowElements[1].LRUVal = 1;
                currRowElements[0].LRUVal = 0;
                return 1;
            }
            if (currRowElements[0].LRUVal == 1)
            {
                return loadMemIntoBlock(currRowElements, false, tag, ref misses);
            }
            else
            {
                return loadMemIntoBlock(currRowElements, true, tag, ref misses);
            }
        }

        private static int loadMemIntoBlock(CacheDataBlock[] blocks, bool loadFirstBlock, int tag, ref int misses)
        {
            int loadPosition = loadFirstBlock ? 0 : 1;
            blocks[loadPosition].vBit = true;
            blocks[loadPosition].tag = tag;
            blocks[loadPosition].LRUVal = 1;
            blocks[(!loadFirstBlock ? 1 : 0)].LRUVal = 0;
            misses++;
            return 30;
        }

        //private static void outputCache(SetAssociativeCacheRow[] directMappedCache)
        //{
        //    Console.Write("Valid\tTag\tData");
        //    for (int i = 0; i < 4; i++)
        //    {
        //        Console.Write("\n" + (directMappedCache[i].vBit ? 1 : 0) + "\t"
        //            + directMappedCache[i].tag.ToString());
        //        //for (int j = 0; j < 16; j++)
        //        //{
        //        //    Console.Write('a'+i + "\t");
        //        //}
        //    }
        //}
    }

    class SetAssociativeCacheRow
    {
        public CacheDataBlock[] rowElements
        {
            get;
            set;
        }
        public SetAssociativeCacheRow()
        {
            rowElements = new CacheDataBlock[2];
            rowElements[0] = new CacheDataBlock();
            rowElements[1] = new CacheDataBlock();
        }
    }
    class CacheDataBlock
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
        public CacheDataBlock()
        {
            vBit = false;
            tag = 0;
            LRUVal = 0;
        }
    }
}
