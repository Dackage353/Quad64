using Quad64.Scripts;
using Quad64.src.LevelInfo;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Quad64.custom.GameInfo
{
    internal class GameInfo
    {
        public Dictionary<int, LevelInfo> Levels { get; set; } = new Dictionary<int, LevelInfo>();

        public string GetCoinReport()
        {
            var sb = new StringBuilder();

            foreach (KeyValuePair<int, LevelInfo> pair in Levels)
            {
                var level = pair.Value;

                if (level.GetAreaCount() > 0)
                {
                    sb.Append(level.GetCoinCountPerAct());
                }
            }

            return sb.ToString();
        }

        public string GetObjectReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine(GetHeaderLine());

            foreach (KeyValuePair<int, LevelInfo> pair in Levels)
            {
                var level = pair.Value;

                if (level.GetAreaCount() > 0)
                {
                    sb.Append(level.GetObjectList());
                }
            }

            return sb.ToString();
        }

        public string GetCoinObjectReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine(GetHeaderLine());

            foreach (KeyValuePair<int, LevelInfo> pair in Levels)
            {
                var level = pair.Value;

                if (level.GetAreaCount() > 0)
                {
                    sb.Append(level.GetCoinObjectList());
                }
            }

            return sb.ToString();
        }

        public string GetObjectListForArea(int levelID, int areaIndex)
        {
            var level = Levels[levelID];
            var area = level.Areas[areaIndex];

            return area.GetCoinObjectList();
        }

        private string GetHeaderLine()
        {
            var sb = new StringBuilder();

            string[] items =
            {
                "name",
                "coin value",
                "address",
                "model id",
                "x",
                "y",
                "z",
                "rotation x",
                "rotation y",
                "rotation z",
                "behavior address",
                "behavior param 1",
                "behavior param 2",
                "behavior param 3",
                "behavior param 4",
                "all acts",
                "act 1",
                "act 2",
                "act 3",
                "act 4",
                "act 5",
                "act 6",
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

