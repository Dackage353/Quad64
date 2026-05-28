using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quad64.src.LevelInfo
{
    class AreaBackgroundInfo
    {
        public uint address = 0;
        public ushort id_or_color = 0;
        public bool isEndCakeImage = false;
        public uint romLocation = 0;
        public bool usesFog = false;
        public Color fogColor = Color.White;
        public List<uint> fogColor_romLocation = new List<uint>();
    }
}
