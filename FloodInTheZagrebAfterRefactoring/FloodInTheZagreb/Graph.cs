using System;
using System.Collections.Generic;

namespace FloodInTheZagreb {
    class Graph {
        protected List<NodeGraph> graphView;

        public Graph(IEnumerable<Wall> collection) {
            InitializeGraphView(collection);
            RelateNodes(collection);
        }

        public NodeGraph this[Int32 index] => graphView[index];

        private void InitializeGraphView(IEnumerable<Wall> collection) {
            graphView = new List<NodeGraph>();
            foreach(var node in collection.GetAllNodes())
                graphView.Add(new NodeGraph(node, this));
        }
        private void RelateNodes(IEnumerable<Wall> collection) {
            foreach(var nodeGraph in graphView) {
                foreach(var wall in collection) {
                    if(wall.ContainsNode(nodeGraph)) {
                        var otherNode = wall.GetOtherNode(nodeGraph);
                        nodeGraph.LinkedWith(graphView.FindIndex(node => node.Equals(otherNode)));
                    }
                }
            }
        }

        public Int32 Count => graphView.Count;
    }
}
