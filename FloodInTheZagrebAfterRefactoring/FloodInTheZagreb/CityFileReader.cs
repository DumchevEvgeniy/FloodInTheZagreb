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
            var nodes = TryReadElements<Node>(minCountNodes, maxCountNodes);
            if(nodes == null)
                return null;
            var walls = TryReadElements<Wall>(minCountWalls, maxCountWalls, nodes);
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

        private List<T> TryReadElements<T>(Int32 minCount, Int32 maxCount, List<Node> nodes = null) where T : class {
            if(!TryReadCount(out Int32 elementsCount))
                return null;
            if(elementsCount < minCount || elementsCount > maxCount)
                return null;
            var elements = new List<T>();
            for(Int32 i = 0; i < elementsCount; i++) {
                String line = String.Empty;
                try { line = streamReader.ReadLine(); }
                catch(Exception) { return null; }
                var element = Parser<T>.Create().TryGetElement(i + 1, line, nodes);
                if(element == null)
                    return null;
                elements.Add(element);
            }
            return elements;
        }

        public void Dispose() => streamReader.Close();
    }

    abstract class Parser<T> where T : class {
        public static Parser<T> Create() {
            if(typeof(T) == typeof(Node))
                return new NodeEntryParser() as Parser<T>;
            if(typeof(T) == typeof(Wall))
                return new WallEntryParser() as Parser<T>;
            return null;
        }

        public abstract T TryGetElement(Int32 idNode, String inputLine, List<Node> nodes = null);
        protected abstract Boolean DataIsValid(Int32 value);
    }

    class NodeEntryParser : Parser<Node> {
        private readonly Int32 minCoordinate = 0;
        private readonly Int32 maxCoordinate = 100000;

        public NodeEntryParser() : base() { }

        protected override Boolean DataIsValid(Int32 value) =>
            value >= minCoordinate && value <= maxCoordinate;

        public override Node TryGetElement(Int32 idNode, String inputLine, List<Node> nodes = null) {
            if(String.IsNullOrEmpty(inputLine))
                return null;
            if(!inputLine.TryGetTwoInt32Value(out Int32 x, out Int32 y))
                return null;
            if(!DataIsValid(x) || !DataIsValid(y))
                return null;
            return new Node(new Point(x, y)) { ID = idNode };
        }
    }

    class WallEntryParser : Parser<Wall> {
        private readonly Int32 minId = 1;
        private readonly Int32 maxId = 100000;

        public WallEntryParser() : base() { }

        protected override Boolean DataIsValid(Int32 value) =>
            value >= minId && value <= maxId;

        public override Wall TryGetElement(Int32 idWall, String inputLine, List<Node> nodes = null) {
            if(String.IsNullOrEmpty(inputLine))
                return null;
            if(!inputLine.TryGetTwoInt32Value(out Int32 idFirstNode, out Int32 idSecondNode))
                return null;
            if(idFirstNode == idSecondNode)
                return null;
            if(!DataIsValid(idFirstNode) || !DataIsValid(idSecondNode))
                return null;
            var firstNode = nodes.FirstOrDefault(n => n.ID == idFirstNode);
            if(firstNode == null)
                return null;
            var secondNode = nodes.FirstOrDefault(n => n.ID == idSecondNode);
            if(secondNode == null)
                return null;
            return new Wall(firstNode, secondNode) { ID = idWall };
        }
    }
}
