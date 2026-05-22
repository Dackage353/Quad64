using NaturalSort.Extension;
using OpenTK;
using Quad64.custom;
using Quad64.src.LevelInfo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace Quad64.src
{
    internal class TypeObjectSorter
    {
        private readonly string[] typeOrder = new string[]
            {
                "Warp",
                "Star",
                "CoinFormation",
                "Coin",
                "Enemy"
            };


        public List<Object3D> SortByType(List<Object3D> list)
        {
            if (list.Count == 0) return list;

            List<Object3D>[] groups = new List<Object3D>[typeOrder.Length];
            var remaining = new List<Object3D>(list);

            for (int i = 0; i < typeOrder.Length; i++)
            {
                groups[i] = new List<Object3D>();
                var type = typeOrder[i];

                foreach (var obj in remaining)
                {
                    if (obj.CompanionInfo != null && obj.CompanionInfo.Types.Contains(type))
                    {
                        groups[i].Add(obj);
                    }
                }

                var d = new DistanceObjectSorter();
                groups[i] = d.SortByNameAndDistance(groups[i]);

                foreach (var obj in groups[i])
                {
                    remaining.Remove(obj);
                }
            }

            var finalList = new List<Object3D>();
            foreach (var group in groups)
            {
                finalList.AddRange(group);
            }

            return finalList;
        }
    }
}