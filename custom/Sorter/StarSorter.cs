using NaturalSort.Extension;
using OpenTK;
using Quad64.src.LevelInfo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace Quad64.custom.Sorter
{
    internal class StarSorter
    {
        public List<Object3D> Sort(List<Object3D> list)
        {
            if (list.Count <= 1) return list;

            List<List<Object3D>> groups = new List<List<Object3D>>();
            var remaining = new List<Object3D>(list);

            for (int starNumber = 1; starNumber <= 6; starNumber++)
            {
                var group = new List<Object3D>();

                foreach (var obj in remaining)
                {
                    if (obj.CompanionInfo != null && obj.CompanionInfo.StarNumber == starNumber)
                    {
                        group.Add(obj);
                    }
                }

                group = new DistanceSorter().SortByDistance(group);

                foreach (var obj in group)
                {
                    remaining.Remove(obj);
                }

                groups.Add(group);
            }

            var finalList = new List<Object3D>();
            foreach (var group in groups)
            {
                finalList.AddRange(group);
            }

            finalList.AddRange(remaining);
            return finalList;
        }
    }
}