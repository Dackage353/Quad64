using Collada141;
using NaturalSort.Extension;
using Newtonsoft.Json;
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

namespace Quad64.src
{
    internal class GameInfoBuilder
    {
        public List<Level> levels { get; } = new List<Level>();

        private Dictionary<string, CustomObjectInfo> customObjectInfo = new Dictionary<string, CustomObjectInfo>();

        private StringBuilder simpleSB = new StringBuilder();
        private StringBuilder detailedSB = new StringBuilder();

        private CustomGameInfo gameInfo;

        private int[] _levelCoinCountByAct = new int[6];
        private int[] _areaCoinCountByAct = new int[6];

        private static GameInfoBuilder builder = null;

        public static CustomGameInfo GetGameInfo()
        {
            if (builder == null)
            {
                builder = new GameInfoBuilder();
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
            LoadLevels();

            var json = File.ReadAllText("./custom/CustomObjectInfo.json");
            var objectInfo = JsonConvert.DeserializeObject<List<CustomObjectInfo>>(json);
            
            for (int i = 0; i < objectInfo.Count; i++)
            {
                var info = objectInfo[i];
                customObjectInfo.Add(GetKey(info.BehaviorAddress, info.Param1, info.Param2, info.Param3, info.Param4), info);
            }

            MakeGameInfo();
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
            gameInfo = new CustomGameInfo();

            for (int i = 0; i < levels.Count; i++)
            {
                var level = levels[i];
                var levelInfo = new CustomLevelInfo();
                levelInfo.Name = Helper.LevelIDToName(level.LevelID);
                levelInfo.LevelID = level.LevelID;

                for (int areaIndex = 1; areaIndex <= 8; areaIndex++)
                {
                    if (level.hasArea((ushort)areaIndex))
                    {
                        var area = level.Areas[areaIndex - 1];
                        var areaInfo = new CustomAreaInfo();
                        areaInfo.Name = levelInfo.Name + "-" + areaIndex;
                        areaInfo.Objects.AddRange(Object3DListToCustom(area.Objects, false));
                        areaInfo.Objects.AddRange(Object3DListToCustom(area.MacroObjects, true));
                        areaInfo.Objects.AddRange(Object3DListToCustom(area.SpecialObjects, true));
                        areaInfo.CalculateCoinCountByAct();

                        levelInfo.Areas[areaIndex] = areaInfo;
                    }
                }

                levelInfo.CalculateCoinCountByAct();
                gameInfo.Levels[levelInfo.LevelID] = levelInfo;
            }


            //gameInfo.Levels = gameInfo.Levels.OrderBy(c => c.Value.Name, StringComparison.OrdinalIgnoreCase.WithNaturalSort()).ToList();

        }

        private List<CustomObjectInfo> Object3DListToCustom(List<Object3D> list, bool forceAllActs)
        {
            var newList = new List<CustomObjectInfo>();

            foreach (Object3D obj in list)
            {
                var objectInfo = Object3DToCustom(obj, forceAllActs);

                newList.Add(objectInfo);
            }

            return newList;
        }

        public CustomObjectInfo Object3DToCustom(Object3D obj, bool forceAllActs = false)
        {
            var objectInfo = new CustomObjectInfo();

            var coinInfo = GetCustomObjectInfo(obj.Behavior, obj.BehaviorParameter1, obj.BehaviorParameter2, obj.BehaviorParameter3, obj.BehaviorParameter4);
            if (coinInfo != null)
            {
                objectInfo.Name = coinInfo.Name;
                objectInfo.CoinValue = coinInfo.CoinValue;
            }
            else
            {
                objectInfo.Name = obj.getObjectComboName();
            }

            objectInfo.Address = obj.Address;
            objectInfo.ModelID = obj.ModelID;
            objectInfo.BehaviorName = obj.Behavior_Name;

            objectInfo.BehaviorAddress = obj.Behavior;
            objectInfo.Param1 = obj.BehaviorParameter1;
            objectInfo.Param2 = obj.BehaviorParameter2;
            objectInfo.Param3 = obj.BehaviorParameter3;
            objectInfo.Param4 = obj.BehaviorParameter4;
            objectInfo.XPosition = obj.xPos;
            objectInfo.YPosition = obj.yPos;
            objectInfo.ZPosition = obj.zPos;
            objectInfo.XRotation = obj.xRot;
            objectInfo.YRotation = obj.yRot;
            objectInfo.ZRotation = obj.zRot;
            objectInfo.Act1 = obj.Act1;
            objectInfo.Act2 = obj.Act2;
            objectInfo.Act3 = obj.Act3;
            objectInfo.Act4 = obj.Act4;
            objectInfo.Act5 = obj.Act5;
            objectInfo.Act6 = obj.Act6;
            objectInfo.AllActs = obj.AllActs;

            if (forceAllActs) objectInfo.AllActs = true;

            return objectInfo;
        }

        private CustomObjectInfo GetCustomObjectInfo(string behaviorAddress, int param1, int param2, int param3, int param4)
        {
            string specificKey = GetKey(behaviorAddress, param1, param2, param3, param4);
            string genericKey = GetKey(behaviorAddress, -1, -1, -1, -1);
            CustomObjectInfo info = null;

            if (customObjectInfo.ContainsKey(specificKey))
            {
                info = customObjectInfo[specificKey];
            }
            else if (customObjectInfo.ContainsKey(genericKey))
            {
                info = customObjectInfo[genericKey];
            }

            return info;
        }

        private string GetKey(string behaviorAddress, int param1, int param2, int param3, int param4)
        {
            string p1 = param1 < 0 ? "x" : param1.ToString();
            string p2 = param2 < 0 ? "x" : param2.ToString();
            string p3 = param3 < 0 ? "x" : param3.ToString();
            string p4 = param4 < 0 ? "x" : param4.ToString();

            string s =  behaviorAddress + "_" + "_" + p1 + "_" + p2 + "_" + p3 + "_" + p4;

            //MessageBox.Show(s);
            return s;
        }
    }
}
