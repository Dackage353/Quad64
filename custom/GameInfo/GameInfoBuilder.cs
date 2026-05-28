using Collada141;
using NaturalSort.Extension;
using Newtonsoft.Json;
using Quad64.custom.GameInfo;
using Quad64.Scripts;
using Quad64.src.LevelInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quad64.custom.GameInfo
{
    internal class GameInfoBuilder
    {
        public List<Level> levels { get; } = new List<Level>();

        private Dictionary<string, List<ObjectInfo>> behaviorLists = new Dictionary<string, List<ObjectInfo>>();

        private StringBuilder simpleSB = new StringBuilder();
        private StringBuilder detailedSB = new StringBuilder();

        private GameInfo gameInfo;

        private int[] _levelCoinCountByAct = new int[6];
        private int[] _areaCoinCountByAct = new int[6];

        private static GameInfoBuilder builder = null;

        public static GameInfo GetGameInfo()
        {
            if (builder == null)
            {
                builder = new GameInfoBuilder();
            }
            if (builder.gameInfo == null)
            {
                builder.MakeGameInfo();
            }

            return builder.gameInfo;
        }

        public static GameInfoBuilder GetBuilder()
        {
            if (builder == null)
            {
                builder = new GameInfoBuilder();
            }

            return builder;
        }


        public static void ClearGameInfo()
        {
            builder = null; 
        }

        private GameInfoBuilder()
        {
            LoadObjectInfo();
        }

        private void LoadObjectInfo()
        {
            var json = File.ReadAllText("./custom/ObjectInfo.json");
            var objectInfo = JsonConvert.DeserializeObject<List<ObjectInfo>>(json);

            for (int i = 0; i < objectInfo.Count; i++)
            {
                var info = objectInfo[i];

                if (!behaviorLists.ContainsKey(info.BehaviorAddress))
                {
                    behaviorLists[info.BehaviorAddress] = new List<ObjectInfo>();
                }

                behaviorLists[info.BehaviorAddress].Add(info);
            }
        }

        private void LoadLevels()
        {
            levels.Clear();

            foreach (var pair in ROM.Instance.levelIDs)
            {
                var id = pair.Value;
                var level = new Level(id, 1);

                level.sortAndAddNoModelEntries();
                LevelScripts.parse(ref level, 0x15, 0);

                if (level.Areas.Count > 0)
                {
                    level.CurrentAreaID = level.Areas[0].AreaID;
                    levels.Add(level);
                }
            }
        }

        private void MakeGameInfo()
        {
            gameInfo = new GameInfo();
            LoadLevels();

            for (int i = 0; i < levels.Count; i++)
            {
                var level = levels[i];
                var levelInfo = new LevelInfo();
                levelInfo.Name = Helper.LevelIDToName(level.LevelID);
                levelInfo.LevelID = level.LevelID;

                for (int areaIndex = 1; areaIndex <= 8; areaIndex++)
                {
                    if (level.hasArea((ushort)areaIndex))
                    {
                        var area = level.Areas[areaIndex - 1];
                        var areaInfo = new AreaInfo();
                        areaInfo.Name = levelInfo.Name + "-" + areaIndex;
                        areaInfo.Objects.AddRange(area.Objects);
                        areaInfo.Objects.AddRange(area.MacroObjects);
                        areaInfo.Objects.AddRange(area.SpecialObjects);
                        areaInfo.CalculateCoinCountByAct();

                        levelInfo.Areas[areaIndex] = areaInfo;
                    }
                }

                levelInfo.CalculateCoinCountByAct();
                gameInfo.Levels[levelInfo.LevelID] = levelInfo;
            }
        }

        public ObjectInfo GetCustomObjectInfo(string behaviorAddress, int param1, int param2, int param3, int param4, int modelID)
        {
            if (behaviorLists.ContainsKey(behaviorAddress))
            {
                var infos = behaviorLists[behaviorAddress];

                foreach (var info in infos)
                {
                    if ((info.Param1 == param1 || info.Param1 < 0) &&
                        (info.Param2 == param2 || info.Param2 < 0) &&
                        (info.Param3 == param3 || info.Param3 < 0) &&
                        (info.Param4 == param4 || info.Param4 < 0) &&
                        (info.ModelID == modelID || info.ModelID < 0))
                    {
                        return info;
                    }
                }
            }

            return null;
        }
    }
}
