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
    internal class ObjectSorter
    {
        private List<Object3D> _originalList, _nameSortedList, _sameTypeList;
        private List<Object3D> _finalSorted = new List<Object3D>();
        private List<Object3D> _remaining = new List<Object3D>();

        public List<Object3D> SortByNameAndDistance(List<Object3D> list)
        {
            if (list.Count == 0) return list;

            _originalList = list;
            _nameSortedList = list.OrderBy(c => c.getObjectComboName(), StringComparison.OrdinalIgnoreCase.WithNaturalSort()).ToList();
            _remaining = new List<Object3D>(_nameSortedList);

            while (_remaining.Count > 0)
            {
                GetSameTypeList();
                ProcessSameType();
            }

            return _finalSorted;
        }

        private void GetSameTypeList()
        {
            _sameTypeList = new List<Object3D>();

            var first = _remaining.First();
            _sameTypeList.Add(first);

            

            for (int i = 1; i < _remaining.Count; i++)
            {
                var next = _remaining[i];
                /*MessageBox.Show("remaining: " + _remaining.Count +
                "\nfirst behavior: " + first.Behavior +
                "\nnext behavior: " + next.Behavior);*/

                if (first.Behavior == next.Behavior)
                {
                    _sameTypeList.Add(next);
                }
                else
                {
                    break;
                }
            }

            //MessageBox.Show("sametype length: " + _sameTypeList.Count);

            for (int i = 0; i < _sameTypeList.Count; i++)
            {
                _remaining.Remove(_sameTypeList[i]);
            }
        }

        private void ProcessSameType()
        {
            List<Object3D> sameTypeRemaining = new List<Object3D>(_sameTypeList);

            var corner = new Vector3(short.MinValue, 0, short.MinValue);
            var previous = GetClosestToPoint(corner, sameTypeRemaining);
            _finalSorted.Add(previous);
            sameTypeRemaining.Remove(previous);

            while (sameTypeRemaining.Count > 0)
            {
                var previousPosition = new Vector3(previous.xPos, previous.yPos, previous.zPos);
                Object3D nearest = GetClosestToPoint(previousPosition, sameTypeRemaining);

                _finalSorted.Add(nearest);
                sameTypeRemaining.Remove(nearest);
                previous = nearest;
            }
        }

        private Object3D GetClosestToPoint(Vector3 point, List<Object3D> list)
        {
            float lowestDistance = float.MaxValue;
            Object3D nearest = null;

            foreach (var obj in list)
            {
                var position = new Vector3(obj.xPos, obj.yPos, obj.zPos);
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