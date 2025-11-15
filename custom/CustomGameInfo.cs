using Quad64.Scripts;
using Quad64.src.LevelInfo;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src
{
    class CustomGameInfo
    {
        public List<CustomLevelInfo> Levels { get; set; } = new List<CustomLevelInfo>();

        public string GetCoinReport()
        {
            var sb = new StringBuilder();

            for (int levelIndex = 0; levelIndex < Levels.Count; levelIndex++)
            {
                var level = Levels[levelIndex];

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

            for (int levelIndex = 0; levelIndex < Levels.Count; levelIndex++)
            {
                var level = Levels[levelIndex];

                if (level.GetAreaCount() > 0)
                {
                    sb.Append(level.GetObjectList());
                }
            }

            return sb.ToString();
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
                "behavior name",
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

