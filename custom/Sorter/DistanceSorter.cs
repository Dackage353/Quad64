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
    internal class DistanceSorter
    {
        public List<Object3D> SortByDistance(List<Object3D> list)
        {
            if (list.Count <= 1) return list;

            var sorted = new List<Object3D>();
            var remaining = new List<Object3D>(list);

            var corner = new Vector3(short.MinValue, 0, short.MinValue);
            var previous = GetClosestToPoint(corner, remaining);
            sorted.Add(previous);
            remaining.Remove(previous);

            while (remaining.Count > 0)
            {
                var previousPosition = new Vector3(previous.xPos, previous.yPos, previous.zPos);
                Object3D nearest = GetClosestToPoint(previousPosition, remaining);

                sorted.Add(nearest);
                remaining.Remove(nearest);
                previous = nearest;
            }

            return sorted;
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