using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FloodInTheZagreb {
    class CityBackground {
        private readonly CellCityBackground[,] cells;
        private readonly Int32 left;
        private readonly Int32 right;
        private readonly Int32 bottom;
        private readonly Int32 top;
        private const Int32 offsetOnLeft = 1;
        private const Int32 offsetOnRight = 1;
        private const Int32 offsetOnTop = 1;
        private const Int32 offsetOnBottom = 1;

        public CityBackground(Rectangle area) {
            var height = area.Height + offsetOnBottom + offsetOnTop;
            var width = area.Width + offsetOnLeft + offsetOnRight;
            cells = new CellCityBackground[height, width];
            left = area.Left;
            right = area.Right - 1;
            bottom = area.Top - area.Height;
            top = area.Top - 1;
            InitializeExternalCells();
            InitializeInternalCells();
        }

        private void InitializeExternalCells() {
            for(Int32 yCell = bottom - offsetOnBottom; yCell < top + offsetOnTop; yCell++) {
                var xLeftCell = left - offsetOnLeft;
                var xRightCell = right + offsetOnRight;
                this[xLeftCell, yCell] = new CellCityBackground(this, xLeftCell, yCell) { IsFlooded = true };
                this[xRightCell, yCell] = new CellCityBackground(this, xRightCell, yCell) { IsFlooded = true };
            }
            for(Int32 xCell = left - offsetOnLeft; xCell < right + offsetOnRight; xCell++) {
                var yBottomCell = bottom - offsetOnBottom;
                var yTopCell = top + offsetOnTop;
                this[xCell, yBottomCell] = new CellCityBackground(this, xCell, yBottomCell) { IsFlooded = true };
                this[xCell, yTopCell] = new CellCityBackground(this, xCell, yTopCell) { IsFlooded = true };
            }
        }
        private void InitializeInternalCells() {
            for(Int32 xCell = left; xCell <= right; xCell++)
                for(Int32 yCell = bottom; yCell <= top; yCell++)
                    this[xCell, yCell] = new CellCityBackground(this, xCell, yCell);
        }

        public CellCityBackground this[Int32 x, Int32 y] {
            get => cells[y - bottom + 1, x - left + 1];
            private set => cells[y - bottom + 1, x - left + 1] = value;
        }

        public void FloodedWherePossible(List<Wall> existingWalls) {
            Boolean wasFlood = true;
            while(wasFlood) {
                wasFlood = false;
                for(Int32 x = left; x <= right; x++) {
                    for(Int32 y = bottom; y <= top; y++) {
                        var currentCell = this[x, y];
                        if(currentCell.IsFlooded)
                            continue;
                        if(currentCell.CanFlooded(existingWalls)) {
                            currentCell.IsFlooded = true;
                            wasFlood = true;
                        }
                    }
                }
            }
        }

        public IEnumerable<Wall> FindAllFloodedWalls(IEnumerable<Wall> walls) =>
            walls.ToList().FindAll(wall => IsFlooded(wall));
        private Boolean IsFlooded(Wall wall) {
            if(wall.IsHorizontal) {
                var leftNode = wall.GetLeftNode();
                var rightNode = wall.GetRightNode();
                var aboveTheWall = leftNode.Y;
                var belowTheWall = leftNode.Y - 1;
                if(IsFloodedAllHorizontalCell(leftNode.X, rightNode.X, aboveTheWall))
                    return true;
                if(IsFloodedAllHorizontalCell(leftNode.X, rightNode.X, belowTheWall))
                    return true;
            }
            else {
                var bottomNode = wall.GetBottomNode();
                var topNode = wall.GetTopNode();
                var rightOfTheWall = bottomNode.X;
                var leftOfTheWall = bottomNode.X - 1;
                if(IsFloodedAllVerticalCell(rightOfTheWall, bottomNode.Y, topNode.Y))
                    return true;
                if(IsFloodedAllVerticalCell(leftOfTheWall, bottomNode.Y, topNode.Y))
                    return true;
            }
            return false;
        }
        private Boolean IsFloodedAllHorizontalCell(Int32 leftX, Int32 rightX, Int32 y) =>
            All(leftX, rightX, x => this[x, y].IsFlooded);
        private Boolean IsFloodedAllVerticalCell(Int32 x, Int32 bottomY, Int32 topY) =>
            All(bottomY, topY, y => this[x, y].IsFlooded);
        private Boolean All(Int32 firstBound, Int32 secondBound, Predicate<Int32> match) {
            for(Int32 value = firstBound; value < secondBound; value++)
                if(!match(value))
                    return false;
            return true;
        }
    }
}
