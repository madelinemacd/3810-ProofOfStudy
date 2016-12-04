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
            //initialize the cache
            SetAssociativeCacheRow[] setAssociativeCache = new SetAssociativeCacheRow[8];
            for (int i = 0; i < 8; i++)
            {
                setAssociativeCache[i] = new SetAssociativeCacheRow();
            }

            //ommitted because it's a repeat of code from the direct mapped cache
            #region address definitions

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

            //perform lookups and collect data
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

            Console.ReadLine();
        }
        //first use direct mapped cache code to determine a row
        //then go to another method to look through the options in each row and choose one

        private static int performLookup(SetAssociativeCacheRow[] setAssociativeCache, Address currAdd, ref int misses)
        {
            //get the current row
            CacheDataBlock[] currRowElements = setAssociativeCache[currAdd.row].rowElements;

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
            else
            {
                return twoValidBlocksInSet(currRowElements, currAdd.tag,ref misses);
            }
        }

        private static int twoValidBlocksInSet(CacheDataBlock[] currRowElements, int tag, ref int misses)
        {
            //check both tags for if there was a hit
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
            //if there wasn't a hit load memory into the appropriate block
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
    }

    //stores information about a row in the cache
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
    //stores information about one entry in the set that makes up the row
    class CacheDataBlock
    {
        public int tag;
        public bool vBit;
        public int LRUVal;
        public CacheDataBlock()
        {
            vBit = false;
            tag = 0;
            LRUVal = 0;
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
            tag = address / 32;
            row = (address / 4) % 8;
            offset = address % 4;
            value = address;
        }
    }
}
