using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quad64.custom.Sorter
{
    internal class TagGroup
    {
        public List<Object3D> Items { get; } = new List<Object3D>();
        public string Tag { get; }

        public TagGroup(string tag)
        {
            Tag = tag;
        }

    }
}
