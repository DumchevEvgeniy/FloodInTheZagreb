using System;
using System.Collections.Generic;
using System.Drawing;

namespace FloodInTheZagreb {
    class CellCityBackground {
        private readonly CityBackground ownerCity;
        private Point coordinate;
        public Boolean IsFlooded { get; set; }

        public CellCityBackground(CityBackground owner, Int32 x, Int32 y) {
            coordinate = new Point(x, y);
            ownerCity = owner;
            IsFlooded = false;
        }

        public Boolean CanFlooded(List<Wall> existingWalls) {
            return TopCellIsFloodedAndNotExistBarrier(existingWalls) || BottomCellIsFloodedAndNotExistBarrier(existingWalls)
                || LeftCellIsFloodedAndNotExistBarrier(existingWalls) || RightCellIsFloodedAndNotExistBarrier(existingWalls);
        }

        private Boolean TopCellIsFloodedAndNotExistBarrier(List<Wall> existingWalls) =>
            CellIsFloodedAndNotExistBarrier(GetTopCell(), existingWalls, CreateTopWall());
        private Boolean BottomCellIsFloodedAndNotExistBarrier(List<Wall> existingWalls) =>
            CellIsFloodedAndNotExistBarrier(GetBottomCell(), existingWalls, CreateBottomWall());
        private Boolean LeftCellIsFloodedAndNotExistBarrier(List<Wall> existingWalls) =>
            CellIsFloodedAndNotExistBarrier(GetLeftCell(), existingWalls, CreateLeftWall());
        private Boolean RightCellIsFloodedAndNotExistBarrier(List<Wall> existingWalls) =>
            CellIsFloodedAndNotExistBarrier(GetRightCell(), existingWalls, CreateRightWall());
        private Boolean CellIsFloodedAndNotExistBarrier(CellCityBackground cell, List<Wall> existingWalls, Wall barrier) =>
            cell.IsFlooded && !existingWalls.Exists(w => w.Contains(barrier));

        private CellCityBackground GetTopCell() => ownerCity[X, Y + 1];
        private CellCityBackground GetBottomCell() => ownerCity[X, Y - 1];
        private CellCityBackground GetLeftCell() => ownerCity[X - 1, Y];
        private CellCityBackground GetRightCell() => ownerCity[X + 1, Y];

        private Wall CreateTopWall() => new Wall(CreateTopLeftNode(), CreateTopRightNode());
        private Wall CreateBottomWall() => new Wall(CreateBottomLeftNode(), CreateBottomRightNode());
        private Wall CreateLeftWall() => new Wall(CreateBottomLeftNode(), CreateTopLeftNode());
        private Wall CreateRightWall() => new Wall(CreateBottomRightNode(), CreateTopRightNode());

        private Node CreateTopLeftNode() => new Node(X, Y + 1);
        private Node CreateTopRightNode() => new Node(X + 1, Y + 1);
        private Node CreateBottomLeftNode() => new Node(X, Y);
        private Node CreateBottomRightNode() => new Node(X + 1, Y);

        public Int32 X => coordinate.X;
        public Int32 Y => coordinate.Y;
    }
}
