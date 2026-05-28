using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quad64.custom.GameInfo
{
    internal class ObjectInfo
    {
        public string Name { get; set; } = string.Empty;
        public string BehaviorAddress { get; set; } = string.Empty;
        public int ModelID { get; set; } = -1;
        public int CoinValue { get; set; } = 0;
        public int StarNumber { get; set; } = -1;
        public int Param1 { get; set; } = -1;
        public int Param2 { get; set; } = -1;
        public int Param3 { get; set; } = -1;
        public int Param4 { get; set; } = -1;
        public string[] Tags { get; set; } = new string[0];
    }
}
