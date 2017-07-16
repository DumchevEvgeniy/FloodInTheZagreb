using System;

namespace FloodInTheZagreb {
    class Wall {
        public Int32 ID { get; set; }
        public Node FirstNode { get; private set; }
        public Node SecondNode { get; private set; }

        public Wall(Node firstNode, Node secondNode) {
            FirstNode = firstNode;
            SecondNode = secondNode;
        }

        public Boolean IsHorizontal => FirstNode.Y == SecondNode.Y;
        public Boolean IsVertical => FirstNode.X == SecondNode.X;

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
            return FirstNode.OnOrLeftOf(SecondNode) ? FirstNode : SecondNode;
        }
        public Node GetRightNode() {
            if(IsVertical)
                return null;
            return FirstNode.OnOrRightOf(SecondNode) ? FirstNode : SecondNode;
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
            else {
                if(FirstNode.X != wall.FirstNode.X)
                    return false;
                var bottomNode = GetBottomNode();
                var topNode = GetTopNode();
                return wall.FirstNode.BetweenTwoNodeByY(bottomNode, topNode) &&
                    wall.SecondNode.BetweenTwoNodeByY(bottomNode, topNode);
            }
        }
        
        public override Boolean Equals(Object obj) {
            if(obj == null)
                return false;
            var wall = obj as Wall;
            if(wall == null)
                return false;
            return FirstNode.Equals(wall.FirstNode) && SecondNode.Equals(wall.SecondNode) ||
                FirstNode.Equals(wall.SecondNode) && SecondNode.Equals(wall.FirstNode);
        }
        public override Int32 GetHashCode() => ID;
    }
}
