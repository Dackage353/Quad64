using Quad64.Scripts;
using Quad64.src.LevelInfo;

namespace Quad64.src
{
    internal class ObjectCoinInfo
    {
        public string Name { get; set; }
        public string BehaviorAddress { get; set; }
        public int Param2 { get; set; } = -1;
        public int CoinValue { get; set; }
    }
}