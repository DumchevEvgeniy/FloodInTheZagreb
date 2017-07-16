using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FloodInTheZagreb {
    class City {
        public List<Node> Nodes { get; private set; }
        public Walls Walls { get; private set; }

        public City(IEnumerable<Node> nodes, IEnumerable<Wall> walls) {
            Nodes = nodes.ToList();
            Walls = new Walls(walls);
        }

        public void DestroyByFlood() {
            var area = GetArea();
            var field = new CityField(area);
            while(true) {
                Boolean wasFlood = false;
                for(Int32 x = area.Left; x < area.Left + area.Width; x++) {
                    for(Int32 y = area.Y - area.Height; y < area.Y; y++) {
                        if(field[x, y])
                            continue;
                        if(field.CanFill(x, y, Walls)) {
                            field.FillCell(x, y);
                            wasFlood = true;
                        }
                    }
                }
                if(!wasFlood) {
                    var walls = FindAllFloodedWalls(field);
                    if(walls == null || walls.Count == 0)
                        return;
                    var nodes = walls.GetAllNodes().ToList();
                    var nodeInfoList = new NodeInfoList(nodes.Select(n => new NodeInfo(n, walls)));
                    var wallsOfEnclosedSpace = nodeInfoList.FindWallsOfEnclosedSpace().ToList();
                    if(wallsOfEnclosedSpace.Count == 0)
                        return;
                    wallsOfEnclosedSpace.ForEach(w => Walls.Remove(w));
                }
            }
        }

        private Walls FindAllFloodedWalls(CityField field) =>
            new Walls(Walls.FindAll(wall => field.IsFlooded(wall)));

        public Rectangle GetArea() {
            var left = Nodes.Min(n => n.X);
            var right = Nodes.Max(n => n.X);
            var bottom = Nodes.Min(n => n.Y);
            var top = Nodes.Max(n => n.Y);
            return new Rectangle(left, top, right - left, top - bottom);
        }
    }

    class CityField {
        private Boolean[,] field;
        private Int32 left;
        private Int32 bottom;

        public CityField(Rectangle area) {
            field = new Boolean[area.Height + 2, area.Width + 2];
            left = area.Left;
            bottom = area.Y - area.Height;
            FloodOutsideCells();
        }

        private void FloodOutsideCells() {
            for(Int32 indexRow = 0; indexRow < field.GetLength(0); indexRow++) {
                field[indexRow, 0] = true;
                field[indexRow, field.GetLength(1) - 1] = true;
            }
            for(Int32 indexColumn = 0; indexColumn < field.GetLength(1); indexColumn++) {
                field[0, indexColumn] = true;
                field[field.GetLength(0) - 1, indexColumn] = true;
            }
        }

        public Boolean this[Int32 x, Int32 y] {
            get => field[y - bottom + 1, x - left + 1];
        }

        public void FillCell(Int32 x, Int32 y) => field[y - bottom + 1, x - left + 1] = true;

        public Boolean CanFill(Int32 x, Int32 y, List<Wall> walls) {
            if(GetValueTopCell(x, y) && !walls.Exists(w => w.Contains(new Wall(new Node(x, y + 1), new Node(x + 1, y + 1)))))
                return true;
            if(GetValueBottomCell(x, y) && !walls.Exists(w => w.Contains(new Wall(new Node(x, y), new Node(x + 1, y)))))
                return true;
            if(GetValueLeftCell(x, y) && !walls.Exists(w => w.Contains(new Wall(new Node(x, y), new Node(x, y + 1)))))
                return true;
            if(GetValueRightCell(x, y) && !walls.Exists(w => w.Contains(new Wall(new Node(x + 1, y), new Node(x + 1, y + 1)))))
                return true;
            return false;
        }

        private Boolean GetValueTopCell(Int32 x, Int32 y) => this[x, y + 1];
        private Boolean GetValueBottomCell(Int32 x, Int32 y) => this[x, y - 1];
        private Boolean GetValueLeftCell(Int32 x, Int32 y) => this[x - 1, y];
        private Boolean GetValueRightCell(Int32 x, Int32 y) => this[x + 1, y];

        public Boolean IsFlooded(Wall wall) {
            if(wall.IsHorizontal) {
                var leftNode = wall.GetLeftNode();
                var rightNode = wall.GetRightNode();
                if(IsFlooded(leftNode.X, rightNode.X, x => this[x, leftNode.Y]))
                    return true;
                if(IsFlooded(leftNode.X, rightNode.X, x => this[x, leftNode.Y - 1]))
                    return true;
            }
            else {
                var bottomNode = wall.GetBottomNode();
                var topNode = wall.GetTopNode();
                if(IsFlooded(bottomNode.Y, topNode.Y, y => this[bottomNode.X, y]))
                    return true;
                if(IsFlooded(bottomNode.Y, topNode.Y, y => this[bottomNode.X - 1, y]))
                    return true;
            }
            return false;
        }
        private Boolean IsFlooded(Int32 firstBound, Int32 secondBound, Predicate<Int32> match) {
            for(Int32 value = firstBound; value < secondBound; value++)
                if(!match(value))
                    return false;
            return true;
        }

        public Int32 Height => field.GetLength(0) - 2;
        public Int32 Width => field.GetLength(1) - 2; 
    }
}
