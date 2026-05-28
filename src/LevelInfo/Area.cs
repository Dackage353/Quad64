using OpenTK;
using Quad64.src.Scripts;
using Quad64.src.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quad64.src.LevelInfo
{
    class Area
    {
        private Level parent;
        private ushort areaID;
        public ushort AreaID { get { return areaID; } }
        private uint geoLayoutPointer;
        public uint GeometryLayoutPointer { get { return geoLayoutPointer; } }
        public AreaBackgroundInfo bgInfo = new AreaBackgroundInfo();

        public Model3D AreaModel = new Model3D();
        public CollisionMap collision = new CollisionMap();

        public List<Object3D> Objects = new List<Object3D>();
        public List<Object3D> MacroObjects = new List<Object3D>();
        public List<Object3D> SpecialObjects = new List<Object3D>();
        public List<Warp> Warps = new List<Warp>();
        public List<Warp> PaintingWarps = new List<Warp>();
        public List<WarpInstant> InstantWarps = new List<WarpInstant>();

        public Area(ushort areaID, uint geoLayoutPointer, Level parent)
        {
            this.areaID = areaID;
            this.geoLayoutPointer = geoLayoutPointer;
            this.parent = parent;
        }

        private readonly Vector3 boundOff = new Vector3(25f, 25f, 25f);

        private bool isObjectSelected(int list, int obj)
        {
            if (!Globals.isMultiSelected)
            {
                if (list == Globals.list_selected && obj == Globals.item_selected)
                    return true;
            }
            else
            {
                foreach (int selectedIndex in Globals.multi_selected_nodes[list])
                {
                    if (selectedIndex == obj)
                        return true;
                }
            }
            return false;
        }

        public void drawPicking()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Object3D obj = Objects[i];
                Vector3 scale = Vector3.One;
                Quaternion rotation = new Quaternion(obj.xRotation, obj.yRotation, obj.zRotation, 1.0f);
                Vector3 position = new Vector3(obj.xPosition, obj.yPosition, obj.zPosition);
                if (obj.ModelID != 0)
                {
                    if (parent.ModelIDs.ContainsKey(obj.ModelID))
                    {
                        Model3D model = parent.ModelIDs[obj.ModelID];
                        BoundingBox.draw_solid(scale, rotation, position,
                            System.Drawing.Color.FromArgb(i % 256, i / 256, 1),
                            model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                    }
                }
                else
                {
                    BoundingBox.draw_solid(scale, rotation, position,
                        System.Drawing.Color.FromArgb(i % 256, i / 256, 1),
                        new Vector3(150.0f, 150.0f, 150.0f),
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
            for (int i = 0; i < MacroObjects.Count; i++)
            {
                Object3D obj = MacroObjects[i];
                Vector3 scale = Vector3.One;
                Quaternion rotation = new Quaternion(obj.xRotation, obj.yRotation, obj.zRotation, 1.0f);
                Vector3 position = new Vector3(obj.xPosition, obj.yPosition, obj.zPosition);
                if (obj.ModelID != 0)
                {
                    if (parent.ModelIDs.ContainsKey(obj.ModelID))
                    {
                        Model3D model = parent.ModelIDs[obj.ModelID];
                        BoundingBox.draw_solid(scale, rotation, position,
                            System.Drawing.Color.FromArgb(i % 256, i / 256, 2),
                            model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                    }
                }
                else
                {
                    BoundingBox.draw_solid(scale, rotation, position,
                        System.Drawing.Color.FromArgb(i % 256, i / 256, 2),
                        new Vector3(150.0f, 150.0f, 150.0f),
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
            for (int i = 0; i < SpecialObjects.Count; i++)
            {
                Object3D obj = SpecialObjects[i];
                Vector3 scale = Vector3.One;
                Quaternion rotation = new Quaternion(obj.xRotation, obj.yRotation, obj.zRotation, 1.0f);
                Vector3 position = new Vector3(obj.xPosition, obj.yPosition, obj.zPosition);
                if (obj.ModelID != 0)
                {
                    if (parent.ModelIDs.ContainsKey(obj.ModelID))
                    {
                        Model3D model = parent.ModelIDs[obj.ModelID];
                        BoundingBox.draw_solid(scale, rotation, position,
                            System.Drawing.Color.FromArgb(i % 256, i / 256, 3),
                            model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                    }
                }
                else
                {
                    BoundingBox.draw_solid(scale, rotation, position,
                        System.Drawing.Color.FromArgb(i % 256, i / 256, 3),
                        new Vector3(150.0f, 150.0f, 150.0f),
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
        }

        public void drawEverything()
        {
            if (Globals.renderCollisionMap)
                collision.drawCollisionMap(false);
            else
                AreaModel.drawModel(Vector3.One, Quaternion.Identity, Vector3.Zero);

            for (int i = 0; i < Objects.Count; i++)
            {
                Object3D obj = Objects[i];
                Vector3 scale = Vector3.One;
                // Need to slighting increase the model's size, just in-case of overlapping bounding boxes.
                if (isObjectSelected(0, i))
                    scale = new Vector3(1.001f, 1.001f, 1.001f);
                Quaternion rotation = new Quaternion(obj.xRotation, obj.yRotation, obj.zRotation, 1.0f);
                Vector3 position = new Vector3(obj.xPosition, obj.yPosition, obj.zPosition);
                if (obj.ModelID != 0 && parent.ModelIDs.ContainsKey(obj.ModelID))
                {
                    Model3D model = parent.ModelIDs[obj.ModelID];
                    if (Globals.drawObjectModels)
                        model.drawModel(scale, rotation, position);
                    BoundingBox.draw(scale, rotation, position,
                        isObjectSelected(0, i) ? Globals.SelectedObjectColor : Globals.ObjectColor,
                        model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                }
                else
                {
                    BoundingBox.draw(scale, rotation, position,
                        isObjectSelected(0, i) ? Globals.SelectedObjectColor : Globals.ObjectColor,
                        new Vector3(150.0f, 150.0f, 150.0f),
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
            for (int i = 0; i < MacroObjects.Count; i++)
            {
                Object3D obj = MacroObjects[i];
                Vector3 scale = Vector3.One;
                Quaternion rotation = new Quaternion(obj.xRotation, obj.yRotation, obj.zRotation, 1.0f);
                Vector3 position = new Vector3(obj.xPosition, obj.yPosition, obj.zPosition);
                if (obj.ModelID != 0 && parent.ModelIDs.ContainsKey(obj.ModelID))
                {
                    Model3D model = parent.ModelIDs[obj.ModelID];
                    if (Globals.drawObjectModels)
                        model.drawModel(scale, rotation, position);
                    BoundingBox.draw(scale, rotation, position,
                        isObjectSelected(1, i) ? Globals.SelectedObjectColor : Globals.MacroObjectColor,
                        model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                }
                else
                {
                    BoundingBox.draw(scale, rotation, position,
                        isObjectSelected(1, i) ? Globals.SelectedObjectColor : Globals.MacroObjectColor,
                        new Vector3(150.0f, 150.0f, 150.0f),
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
            for (int i = 0; i < SpecialObjects.Count; i++)
            {
                Object3D obj = SpecialObjects[i];
                Vector3 scale = Vector3.One;
                Quaternion rotation = new Quaternion(obj.xRotation, obj.yRotation, obj.zRotation, 1.0f);
                Vector3 position = new Vector3(obj.xPosition, obj.yPosition, obj.zPosition);
                if (obj.ModelID != 0 && parent.ModelIDs.ContainsKey(obj.ModelID))
                {
                    Model3D model = parent.ModelIDs[obj.ModelID];
                    if (Globals.drawObjectModels)
                        model.drawModel(scale, rotation, position);
                    BoundingBox.draw(scale, rotation, position,
                        isObjectSelected(2, i) ? Globals.SelectedObjectColor : Globals.SpecialObjectColor,
                        model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                }
                else
                {
                    BoundingBox.draw(scale, rotation, position,
                        isObjectSelected(2, i) ? Globals.SelectedObjectColor : Globals.SpecialObjectColor,
                        new Vector3(150.0f, 150.0f, 150.0f),
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
        }
    }
}
