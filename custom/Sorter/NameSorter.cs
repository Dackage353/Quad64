using NaturalSort.Extension;
using OpenTK;
using Quad64.src.LevelInfo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace Quad64.src
{
    internal class NameSorter
    {
        public List<Object3D> SortByName(List<Object3D> list)
        {
            if (list.Count <= 1) return list;

            return list.OrderBy(x => x.GetCustomName(), StringComparison.OrdinalIgnoreCase.WithNaturalSort()).ToList();
        }

        public List<List<Object3D>> SortIntoGroups(List<Object3D> list)
        {
            list = SortByName(list);
            List<List<Object3D>> groups = new List<List<Object3D>>();

            string lastName = null;

            foreach (var obj in list)
            {
                if (lastName == null || obj.GetCustomName() != lastName)
                {
                    lastName = obj.GetCustomName();
                    groups.Add(new List<Object3D>());
                }
                groups[groups.Count - 1].Add(obj);
            }

            return groups;
        }
    }
}