using Quad64.Scripts;
using Quad64.src.LevelInfo;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src
{
    class CustomAreaInfo
    {
        public string Name { get; set; }
        public int AreaIndex { get; set; }
        public int[] CoinCountByAct { get; set; } = new int[6];
        public List<CustomObjectInfo> Objects { get; set; } = new List<CustomObjectInfo>();

        public string GetCoinCountPerAct()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Name);

            if (AllActsHaveSameCoinCount())
            {
                sb.AppendLine("all acts: " + CoinCountByAct[0]);
            }
            else
            {
                for (int i = 0; i < CoinCountByAct.Length; i++)
                {
                    string text = "act " + (i + 1) + ": " + CoinCountByAct[i];
                    sb.AppendLine(text);
                }
            }

            sb.AppendLine();
            return sb.ToString();
        }

        public string GetObjectList()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Name);

            for (int i = 0; i < Objects.Count; i++)
            {
                var obj = Objects[i];
                sb.AppendLine(obj.ToString());
            }

            sb.AppendLine();
            return sb.ToString();
        }

        public bool AllActsHaveSameCoinCount()
        {
            for (int i = 1; i < CoinCountByAct.Length; i++)
            {
                if (CoinCountByAct[0] != CoinCountByAct[i])
                {
                    return false;
                }
            }

            return true;
        }

        public void CalculateCoinCountByAct()
        {
            CoinCountByAct = new int[6];

            for (int i = 0; i < Objects.Count; i++)
            {
                var obj = Objects[i];

                if (obj.CoinValue > 0)
                {
                    if (obj.Act1 || obj.AllActs) CoinCountByAct[0] += obj.CoinValue;
                    if (obj.Act2 || obj.AllActs) CoinCountByAct[1] += obj.CoinValue;
                    if (obj.Act3 || obj.AllActs) CoinCountByAct[2] += obj.CoinValue;
                    if (obj.Act4 || obj.AllActs) CoinCountByAct[3] += obj.CoinValue;
                    if (obj.Act5 || obj.AllActs) CoinCountByAct[4] += obj.CoinValue;

                    bool first5Acts = obj.Act1 && obj.Act2 && obj.Act3 && obj.Act4 && obj.Act5;
                    if (first5Acts || obj.Act6 || obj.AllActs) CoinCountByAct[5] += obj.CoinValue;
                }
            }
        }
    }
}

