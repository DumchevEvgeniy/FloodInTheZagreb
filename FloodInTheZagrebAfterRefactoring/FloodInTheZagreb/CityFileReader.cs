using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace FloodInTheZagreb {
    class CityFileReader : IDisposable {
        private const Int32 minCountNodes = 2;
        private const Int32 maxCountNodes = 100000;
        private const Int32 minCountWalls = 1;
        private const Int32 maxCountWalls = 2 * maxCountNodes;

        StreamReader streamReader;

        public CityFileReader(String fileName) {
            streamReader = new StreamReader(fileName);
        }

        public City TryLoadCity() {
            var nodes = TryReadNodes();
            if(nodes == null)
                return null;
            var walls = TryReadWalls(nodes);
            if(walls == null)
                return null;
            return new City(walls);
        }
        
        private Boolean TryReadCount(out Int32 value) {
            value = default(Int32);
            try { value = Int32.Parse(streamReader.ReadLine()); }
            catch(Exception) { return false; }
            return true;
        }
        private List<Node> TryReadNodes() {
            if(!TryReadCount(out Int32 nodesCount))
                return null;
            if(nodesCount < minCountNodes || nodesCount > maxCountNodes)
                return null;
            var nodes = new List<Node>();
            for(Int32 i = 0; i < nodesCount; i++) {
                String line = String.Empty;
                try { line = streamReader.ReadLine(); }
                catch(Exception) { return null; }
                var node = new NodeEntryParser().TryGetNode(i + 1, line);
                if(node == null)
                    return null;
                nodes.Add(node);
            }
            return nodes;
        }
        private List<Wall> TryReadWalls(List<Node> nodes) {
            if(!TryReadCount(out Int32 wallsCount))
                return null;
            if(wallsCount < minCountWalls || wallsCount > maxCountWalls)
                return null;
            var walls = new List<Wall>();
            for(Int32 i = 0; i < wallsCount; i++) {
                String line = String.Empty;
                try { line = streamReader.ReadLine(); }
                catch(Exception) { return null; }
                var wall = new WallEntryParser().TryGetWall(i + 1, nodes, line);
                if(wall == null)
                    return null;
                walls.Add(wall);
            }
            return walls;
        }

        public void Dispose() => streamReader.Close();
    }

    class NodeEntryParser {
        private readonly Int32 minCoordinate = 0;
        private readonly Int32 maxCoordinate = 100000;

        public Node TryGetNode(Int32 idNode, String inputLine) {
            if(String.IsNullOrEmpty(inputLine))
                return null;
            if(!inputLine.TryGetTwoInt32Value(out Int32 x, out Int32 y))
                return null;
            if(!CoordinateIsValid(x) || !CoordinateIsValid(y))
                return null;
            return new Node(new Point(x, y)) { ID = idNode };
        }
        private Boolean CoordinateIsValid(Int32 coordinate) =>
            coordinate >= minCoordinate && coordinate <= maxCoordinate;
    }

    class WallEntryParser {
        private readonly Int32 minId = 1;
        private readonly Int32 maxId = 100000;

        public Wall TryGetWall(Int32 idWall, List<Node> allNode, String inputLine) {
            if(String.IsNullOrEmpty(inputLine))
                return null;
            if(!inputLine.TryGetTwoInt32Value(out Int32 idFirstNode, out Int32 idSecondNode))
                return null;
            if(idFirstNode == idSecondNode)
                return null;
            if(!IdNodeIsValid(idFirstNode) || !IdNodeIsValid(idSecondNode))
                return null;
            var firstNode = allNode.FirstOrDefault(n => n.ID == idFirstNode);
            if(firstNode == null)
                return null;
            var secondNode = allNode.FirstOrDefault(n => n.ID == idSecondNode);
            if(secondNode == null)
                return null;
            return new Wall(firstNode, secondNode) { ID = idWall };
        }
        private Boolean IdNodeIsValid(Int32 id) => id >= minId && id <= maxId;
    }    
}
