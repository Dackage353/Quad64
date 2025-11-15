using Quad64.Scripts;
using Quad64.src.LevelInfo;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src
{
    class CustomObjectInfo
    {
        public string Name { get; set; }
        public int CoinValue { get; set; }
        public string Address { get; set; }
        public string BehaviorName { get; set; }
        public string BehaviorAddress { get; set; }
        public string ModelID { get; set; }
        public int Param1 { get; set; }
        public int Param2 { get; set; }
        public int Param3 { get; set; }
        public int Param4 { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public int XRotation { get; set; }
        public int YRotation { get; set; }
        public int ZRotation { get; set; }
        public bool AllActs { get; set; }
        public bool Act1 { get; set; }
        public bool Act2 { get; set; }
        public bool Act3 { get; set; }
        public bool Act4 { get; set; }
        public bool Act5 { get; set; }
        public bool Act6 { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            string[] items =
            {
                Name,
                CoinValue.ToString(),
                Address,
                ModelID,
                XPosition.ToString(),
                YPosition.ToString(),
                ZPosition.ToString(),
                XRotation.ToString(),
                YRotation.ToString(),
                ZRotation.ToString(),
                BehaviorAddress,
                BehaviorName,
                Param1.ToString(),
                Param2.ToString(),
                Param3.ToString(),
                Param4.ToString(),
                AllActs.ToString(),
                Act1.ToString(),
                Act2.ToString(),
                Act3.ToString(),
                Act4.ToString(),
                Act5.ToString(),
                Act6.ToString(),
            };

            for (int i = 0; i < items.Length; i++)
            {
                sb.Append(Helper.WithQuotesIfNeeded(items[i]));

                if (i < items.Length - 1)
                {
                    sb.Append(",");
                }
            }

            return sb.ToString();
        }
    }
}

