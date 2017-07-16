using System;
using System.Collections.Generic;
using System.Linq;

namespace FloodInTheZagreb {
    class GraphCylceSeacher : Graph {
        private List<NodeGraph> listWithNodesByOrderMoving;
        private StatusNode[] statusNodes;

        public GraphCylceSeacher(IEnumerable<Wall> collection) : base(collection) {
            var offsetForCellWithZeroIndex = 1;
            var maxIdWall = collection.Max(w => Math.Max(w.FirstNode.ID, w.SecondNode.ID));
            statusNodes = new StatusNode[maxIdWall + offsetForCellWithZeroIndex];
            listWithNodesByOrderMoving = new List<NodeGraph>();
        }

        public List<Wall> FindAllCycleWalls() {
            var result = new List<Wall>();
            var allCycleByNodes = FindAllCycleByNodes();
            if(allCycleByNodes.IsEmpty())
                return result;
            foreach(var cycleByNodes in allCycleByNodes) {
                var cycleByWalls = ToCycleByWalls(cycleByNodes);
                foreach(var wall in cycleByWalls)
                    if(!result.Contains(wall))
                        result.Add(wall);
            }
            return result;
        }

        private IEnumerable<IEnumerable<Node>> FindAllCycleByNodes() {
            foreach(var graphNode in graphView) {
                for(Int32 indexNode = 0; indexNode < statusNodes.Length; indexNode++)
                    statusNodes[indexNode] = StatusNode.NotVisited;
                listWithNodesByOrderMoving.Clear();
                var existCycle = graphNode.FindCycle(graphNode, ref listWithNodesByOrderMoving, ref statusNodes, null);
                if(existCycle)
                    yield return listWithNodesByOrderMoving;
            }
        }

        private IEnumerable<Wall> ToCycleByWalls(IEnumerable<Node> nodes) {
            for(Int32 indexNode = 0; indexNode < nodes.Count() - 1; indexNode++)
                yield return new Wall(nodes.ElementAt(indexNode), nodes.ElementAt(indexNode + 1));
            yield return new Wall(nodes.First(), nodes.Last());
        }
    }

    enum StatusNode {
        Visited,
        NotVisited,
        NotRelatedCycleWithStartNode
    }
}
