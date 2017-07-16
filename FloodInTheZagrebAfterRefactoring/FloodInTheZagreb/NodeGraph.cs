using System;
using System.Collections.Generic;

namespace FloodInTheZagreb {
    class NodeGraph : Node {
        protected readonly Graph ownerGraph;
        protected readonly List<Int32> indexesOfLinkedNodes;

        public NodeGraph(Node current, Graph ownerGraph) 
            : base(current) {
            this.ownerGraph = ownerGraph;
            indexesOfLinkedNodes = new List<Int32>();
        }

        public void LinkedWith(Int32 indexNode) => indexesOfLinkedNodes.Add(indexNode);

        public Boolean FindCycle(NodeGraph startNode, ref List<NodeGraph> listWithNodesByOrderMoving,
            ref StatusNode[] statusNode, NodeGraph previousNode) {
            statusNode[ID] = StatusNode.Visited;
            listWithNodesByOrderMoving.Add(this);
            foreach(var indexRelatedNode in indexesOfLinkedNodes) {
                var nodeInfo = ownerGraph[indexRelatedNode];
                if(nodeInfo.Equals(previousNode))
                    continue;
                if(statusNode[nodeInfo.ID] == StatusNode.NotVisited &&
                    nodeInfo.FindCycle(startNode, ref listWithNodesByOrderMoving, ref statusNode, this))
                    return true;
                if(statusNode[nodeInfo.ID] == StatusNode.Visited && nodeInfo.Equals(startNode))
                    return true;
            }
            statusNode[ID] = StatusNode.NotRelatedCycleWithStartNode;
            listWithNodesByOrderMoving.Remove(this);
            return false;
        }
    }
}
