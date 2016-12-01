using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectMappedCache
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
            //addressBits = new int[16];
            //for (int i = 0; i < 16; ++i)
            //{
            //    addressBits[i] = address % 2;
            //    address = address / 2;
            //}
            tag = address / 64;
            row = (address / 16) % 4;
            offset = address % 16;
            value = address;

            //tag = address / 128;
            //row = (address / 16) % 8;
            //offset = address % 16;
            //value = address;

            //            𝑡𝑎𝑔 = 𝑎𝑑𝑑𝑟𝑒𝑠𝑠/ 64
            //𝑟𝑜𝑤 = (𝑎𝑑𝑑𝑟𝑒𝑠𝑠 / 16) mod 4
            //𝑜𝑓𝑓𝑠𝑒𝑡 = 𝑎𝑑𝑑𝑟𝑒𝑠𝑠 𝑚𝑜𝑑 16


            //offset = (char)(addressBits[15] + addressBits[14] * 2 + addressBits[13] * 4);
        }
    }
}
