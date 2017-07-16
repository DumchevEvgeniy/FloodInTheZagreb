using System;
using System.Collections.Generic;
using System.Linq;

namespace FloodInTheZagreb {
    class NodeInfoList : List<NodeInfo> {
        public NodeInfoList() : base() { }
        public NodeInfoList(Int32 capacity) : base(capacity) { }
        public NodeInfoList(IEnumerable<NodeInfo> collection) : base(collection) { }

        public IEnumerable<Wall> FindWallsOfEnclosedSpace() {
            var tempNodeInfoList = new NodeInfoList(this);
            Boolean existSeparateWall = true;
            while(existSeparateWall) {
                existSeparateWall = false;
                foreach(var nodeInfo in tempNodeInfoList) {
                    if(nodeInfo.RelatedWalls.Count == 1) {
                        existSeparateWall = true;
                        var singleWall = nodeInfo.RelatedWalls.First();
                        var otherNode = singleWall.GetOtherNode(nodeInfo);
                        var otherNodeInfo = tempNodeInfoList.First(node => node.Equals(otherNode));
                        otherNodeInfo.RelatedWalls.Remove(singleWall);
                        tempNodeInfoList.Remove(nodeInfo);
                        if(otherNodeInfo.RelatedWalls.Count == 0)
                            tempNodeInfoList.Remove(otherNodeInfo);
                        break;
                    }
                }
            }
            var result = new List<Wall>();
            tempNodeInfoList.ForEach(n => n.RelatedWalls.ForEach(w => {
                if(!result.Contains(w))
                    result.Add(w);
            }));
            return result;
        }
    }

    class Walls : List<Wall> {
        public Walls() : base() { }
        public Walls(Int32 capacity) : base(capacity) { }
        public Walls(IEnumerable<Wall> collection) : base(collection) { }

        public IEnumerable<Node> GetAllNodes() {
            var result = new List<Node>();
            foreach(var wall in this) {
                if(!result.Contains(wall.FirstNode))
                    result.Add(wall.FirstNode);
                if(!result.Contains(wall.SecondNode))
                    result.Add(wall.SecondNode);
            }
            return result;
        }
    }
}
