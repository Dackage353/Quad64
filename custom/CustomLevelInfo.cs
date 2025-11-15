using Quad64.Scripts;
using Quad64.src.LevelInfo;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src
{
    class CustomLevelInfo
    {
        public string Name { get; set; }
        public int[] CoinCountByAct { get; set; } = new int[6];
        public CustomAreaInfo[] Areas { get; set; } = new CustomAreaInfo[8];

        public int GetAreaCount()
        {
            int count = 0;

            for (int i = 0; i < Areas.Length; i++)
            {
                if (Areas[i] != null) count++;
            }

            return count;
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

        public string GetCoinCountPerAct()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Helper.Divider);
            sb.AppendLine(Name);
            sb.AppendLine(Helper.Divider);

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

            if (GetAreaCount() > 1)
            {
                for (int areaIndex = 0; areaIndex < 8; areaIndex++)
                {
                    var area = Areas[areaIndex];

                    if (area != null)
                    {
                        sb.Append(area.GetCoinCountPerAct());
                    }
                }
            }

            return sb.ToString();
        }

        public string GetObjectList()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Helper.Divider);
            sb.AppendLine(Name);
            sb.AppendLine(Helper.Divider);

            for (int areaIndex = 0; areaIndex < 8; areaIndex++)
            {
                var area = Areas[areaIndex];

                if (area != null)
                {
                    sb.Append(area.GetObjectList());
                }
            }

            return sb.ToString();
        }

        public void CalculateCoinCountByAct()
        {
            CoinCountByAct = new int[6];

            for (int areaIndex = 0; areaIndex < 8; areaIndex++)
            {
                var area = Areas[areaIndex];

                if (area != null)
                {
                    for (int actNum = 0; actNum < 6; actNum++)
                    {
                        CoinCountByAct[actNum] += area.CoinCountByAct[actNum];
                    }
                }
            }
        }
    }
}

