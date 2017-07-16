using System;
using System.Collections.Generic;
using System.Drawing;

namespace FloodInTheZagreb {
    class Wall {
        public Int32 ID { get; set; }
        public Node FirstNode { get; private set; }
        public Node SecondNode { get; private set; }

        public Wall(Node firstNode, Node secondNode) {
            FirstNode = firstNode;
            SecondNode = secondNode;
        }

        public Node GetOtherNode(Node node) {
            if(FirstNode.Equals(node))
                return SecondNode;
            if(SecondNode.Equals(node))
                return FirstNode;
            return null;
        }
        public Node GetLeftNode() {
            if(IsVertical)
                return null;
            return FirstNode.OnOrLeftThan(SecondNode) ? FirstNode : SecondNode;
        }
        public Node GetRightNode() {
            if(IsVertical)
                return null;
            return FirstNode.OnOrRightThan(SecondNode) ? FirstNode : SecondNode;
        }
        public Node GetTopNode() {
            if(IsHorizontal)
                return null;
            return FirstNode.OnOrHigherThan(SecondNode) ? FirstNode : SecondNode;
        }
        public Node GetBottomNode() {
            if(IsHorizontal)
                return null;
            return FirstNode.OnOrLessThan(SecondNode) ? FirstNode : SecondNode;
        }

        public Boolean ContainsNode(Node node) => FirstNode.Equals(node) || SecondNode.Equals(node);
        public Boolean Contains(Wall wall) {
            if(wall == null)
                return false;
            if(IsHorizontal != wall.IsHorizontal)
                return false;
            if(IsHorizontal) {
                if(FirstNode.Y != wall.FirstNode.Y)
                    return false;
                var leftNode = GetLeftNode();
                var rightNode = GetRightNode();
                return wall.FirstNode.BetweenTwoNodeByX(leftNode, rightNode) &&
                    wall.SecondNode.BetweenTwoNodeByX(leftNode, rightNode);
            }
            if(FirstNode.X != wall.FirstNode.X)
                return false;
            var bottomNode = GetBottomNode();
            var topNode = GetTopNode();
            return wall.FirstNode.BetweenTwoNodeByY(bottomNode, topNode) &&
                wall.SecondNode.BetweenTwoNodeByY(bottomNode, topNode);
        }
        public Boolean IsHorizontal => FirstNode.Y == SecondNode.Y;
        public Boolean IsVertical => FirstNode.X == SecondNode.X;

        public override Boolean Equals(Object obj) {
            if(obj == null)
                return false;
            var wall = obj as Wall;
            if(wall == null)
                return false;
            return FirstNode.Equals(wall.FirstNode) && SecondNode.Equals(wall.SecondNode) ||
                FirstNode.Equals(wall.SecondNode) && SecondNode.Equals(wall.FirstNode);
        }
        public override Int32 GetHashCode() => FirstNode.GetHashCode() | SecondNode.GetHashCode();
    }

    class Node {
        public Int32 ID { get; set; }
        protected Point coordinate;

        public Node(Point coordinate) {
            this.coordinate = coordinate;
        }
        public Node(Int32 x, Int32 y) : this(new Point(x, y)) { }

        public Boolean OnOrLeftThan(Node node) => node != null && X <= node.X;
        public Boolean OnOrRightThan(Node node) => node != null && X >= node.X;
        public Boolean OnOrHigherThan(Node node) => node != null && Y >= node.Y;
        public Boolean OnOrLessThan(Node node) => node != null && Y <= node.Y;
        public Boolean BetweenTwoNodeByX(Node leftNode, Node rightNode) =>
            OnOrRightThan(leftNode) && OnOrLeftThan(rightNode);
        public Boolean BetweenTwoNodeByY(Node bottomNode, Node topNode) =>
            OnOrHigherThan(bottomNode) && OnOrLessThan(topNode);

        public override Boolean Equals(Object obj) {
            if(obj == null)
                return false;
            var node = obj as Node;
            if(node == null)
                return false;
            return coordinate.Equals(node.coordinate);
        }
        public override Int32 GetHashCode() => X | Y;

        public Int32 X => coordinate.X;
        public Int32 Y => coordinate.Y;
    }

    class NodeInfo : Node {
        List<Wall> relatedWalls;

        public NodeInfo(Node node, IEnumerable<Wall> walls) 
            : base(node.X, node.Y) {
            ID = node.ID;
            relatedWalls = GetRelatedWalls(walls);
        }

        private List<Wall> GetRelatedWalls(IEnumerable<Wall> walls) {
            var relatedWalls = new List<Wall>();
            foreach(var wall in walls)
                if(wall.ContainsNode(this))
                    relatedWalls.Add(wall);
            return relatedWalls;
        }

        public List<Wall> RelatedWalls => relatedWalls;
    }
}
