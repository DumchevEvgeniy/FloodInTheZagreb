using System;
using System.Drawing;

namespace FloodInTheZagreb {
    class Node {
        public Int32 ID { get; set; }
        protected Point coordinate;
        public Int32 X => coordinate.X;
        public Int32 Y => coordinate.Y;

        public Node(Point coordinate) {
            this.coordinate = coordinate;
        }
        public Node(Int32 x, Int32 y) : this(new Point(x, y)) { }
        public Node(Node node) : this(node.X, node.Y) {
            ID = node.ID;
        }

        public Boolean OnOrLeftOf(Node node) => node != null && X <= node.X;
        public Boolean OnOrRightOf(Node node) => node != null && X >= node.X;
        public Boolean OnOrHigherThan(Node node) => node != null && Y >= node.Y;
        public Boolean OnOrLessThan(Node node) => node != null && Y <= node.Y;
        public Boolean BetweenTwoNodeByX(Node leftNode, Node rightNode) =>
            OnOrRightOf(leftNode) && OnOrLeftOf(rightNode);
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
    }
}
