using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FloodInTheZagreb {
    class City {
        public List<Wall> Walls { get; private set; }

        public City(IEnumerable<Wall> walls) {
            Walls = walls.ToList();
        }

        public Rectangle GetArea() {
            var nodes = Walls.GetAllNodes();
            var left = nodes.Min(n => n.X);
            var right = nodes.Max(n => n.X);
            var bottom = nodes.Min(n => n.Y);
            var top = nodes.Max(n => n.Y);
            return new Rectangle(left, top, right - left, top - bottom);
        }

        public void DestroyByFlood() {
            var cityBackground = new CityBackground(GetArea());
            while(true) {
                cityBackground.FloodedWherePossible(Walls);
                var walls = cityBackground.FindAllFloodedWalls(Walls);
                if(walls.IsEmpty())
                    return;
                var cycleWalls = new GraphCylceSeacher(walls).FindAllCycleWalls();
                if(cycleWalls.IsEmpty())
                    return;
                cycleWalls.ForEach(w => Walls.Remove(w));
            }
        }
    }
}
