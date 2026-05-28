using NaturalSort.Extension;
using OpenTK;
using Quad64.custom.Sorter;
using Quad64.src.LevelInfo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace Quad64.custom.Sorter
{
    internal class TagSorter
    {
        private readonly string[] TagOrder = new string[]
            {
                "WarpStart",
                "WarpUsable",
                "Star",
                "StarTrigger",
                "CapOrShellBox",
                "CapSwitch",
                "GateSwitch",
                "HiddenBox",
                "Dialog",
                "Helper",
                "BlueCoin",
                "CoinContainer",
                "CoinFormation",
                "Coin",
                "Enemy",
                "Obstacle",
                "Interactable",
                "Door",
                "Decoration",
                "Warp"
            };


        public List<Object3D> Sort(List<Object3D> list)
        {
            if (list.Count <= 1) return list;

            var groups = SortIntoGroups(list);

            var finalList = new List<Object3D>();
            foreach (var group in groups)
            {
                finalList.AddRange(group.Items);
            }

            return finalList;
        }

        public List<TagGroup> SortIntoGroups(List<Object3D> list)
        {
            List<TagGroup> groups = new List<TagGroup>();
            var remaining = new List<Object3D>(list);

            for (int i = 0; i < TagOrder.Length; i++)
            {
                var group = new TagGroup(TagOrder[i]);
                var type = TagOrder[i];

                foreach (var obj in remaining)
                {
                    if (obj.CompanionInfo != null && obj.CompanionInfo.Tags.Contains(type))
                    {
                        group.Items.Add(obj);
                    }
                }

                foreach (var obj in group.Items)
                {
                    remaining.Remove(obj);
                }

                groups.Add(group);
            }

            var uncategorized = new TagGroup("Uncategorized");
            foreach (var info in remaining)
            {
                if (info.getObjectComboName() != "Empty Object")
                {
                    uncategorized.Items.Add(info);
                }
            }
            groups.Add(uncategorized);

            return groups;
        }
    }
}