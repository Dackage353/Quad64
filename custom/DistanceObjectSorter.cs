using NaturalSort.Extension;
using OpenTK;
using Quad64.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quad64.custom
{
    internal class DistanceObjectSorter
    {
        private List<CustomObjectInfo> _nameSortedList, _sameTypeList;
        private List<CustomObjectInfo> _finalSorted = new List<CustomObjectInfo>();
        private List<CustomObjectInfo> _remaining = new List<CustomObjectInfo>();

        public List<CustomObjectInfo> SortByNameAndDistance(List<CustomObjectInfo> list)
        {
            if (list.Count == 0) return list;

            _nameSortedList = list.OrderBy(c => c.Name, StringComparison.OrdinalIgnoreCase.WithNaturalSort()).ToList();
            _remaining = new List<CustomObjectInfo>(_nameSortedList);
            while (_remaining.Count > 0)
            {
                GetSameTypeList();
                ProcessSameType();
            }

            return _finalSorted;
        }

        private void GetSameTypeList()
        {
            _sameTypeList = new List<CustomObjectInfo>();

            var first = _remaining.First();
            _sameTypeList.Add(first);

            for (int i = 1; i < _remaining.Count; i++)
            {
                var next = _remaining[i];

                if (first.BehaviorAddress == next.BehaviorAddress)
                {
                    _sameTypeList.Add(next);
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < _sameTypeList.Count; i++)
            {
                _remaining.Remove(_sameTypeList[i]);
            }
        }

        private void ProcessSameType()
        {
            List<CustomObjectInfo> sameTypeRemaining = new List<CustomObjectInfo>(_sameTypeList);

            var corner = new Vector3(short.MinValue, 0, short.MinValue);
            var previous = GetClosestToPoint(corner, sameTypeRemaining);
            _finalSorted.Add(previous);
            sameTypeRemaining.Remove(previous);

            while (sameTypeRemaining.Count > 0)
            {
                var previousPosition = new Vector3(previous.XPosition, previous.YPosition, previous.ZPosition);
                CustomObjectInfo nearest = GetClosestToPoint(previousPosition, sameTypeRemaining);

                _finalSorted.Add(nearest);
                sameTypeRemaining.Remove(nearest);
                previous = nearest;
            }
        }

        private CustomObjectInfo GetClosestToPoint(Vector3 point, List<CustomObjectInfo> list)
        {
            float lowestDistance = float.MaxValue;
            CustomObjectInfo nearest = null;

            foreach (var obj in list)
            {
                var position = new Vector3(obj.XPosition, obj.YPosition, obj.ZPosition);
                var distance = Vector3.DistanceSquared(position, point);

                if (distance < lowestDistance)
                {
                    lowestDistance = distance;
                    nearest = obj;
                }
            }

            return nearest;
        }
    }
}