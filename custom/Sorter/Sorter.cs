using Quad64.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quad64.custom.Sorter
{
    internal class Sorter
    {
        public List<Object3D> SortByTypeThenNameThenDistance(List<Object3D> list)
        {
            if (list.Count <= 1) return list;

            var finalSorted = new List<Object3D>();

            var typeGroups = new TagSorter().SortIntoGroups(list);
            foreach (var typeGroup in typeGroups)
            {
                if (typeGroup.Tag == "Star")
                {
                    finalSorted.AddRange(new StarSorter().Sort(typeGroup.Items));
                }
                else
                {
                    var nameGroups = new NameSorter().SortIntoGroups(typeGroup.Items);
                    foreach (var nameGroup in nameGroups)
                    {
                        var distanceList = new DistanceSorter().SortByDistance(nameGroup);
                        finalSorted.AddRange(distanceList);
                    }
                }
            }

            return finalSorted;
        }
    }
}
