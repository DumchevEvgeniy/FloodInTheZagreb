using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace FloodInTheZagreb {
    static class CollectionExtensions {
        public static IEnumerable<Node> GetAllNodes(this IEnumerable<Wall> walls) {
            var result = new List<Node>();
            foreach(var wall in walls) {
                if(!result.Contains(wall.FirstNode))
                    result.Add(wall.FirstNode);
                if(!result.Contains(wall.SecondNode))
                    result.Add(wall.SecondNode);
            }
            return result;
        }

        public static Boolean IsEmpty<T>(this IEnumerable<T> collection) => collection.Count() == 0;
    }

    static class LineSpliterExtensions {
        public static Boolean TryGetTwoInt32Value(this String inputLine, out Int32 first, out Int32 second) {
            first = default(Int32);
            second = default(Int32);
            var strInfos = inputLine.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if(strInfos.Length != 2)
                return false;
            if(!Int32.TryParse(strInfos[0], out first) || !Int32.TryParse(strInfos[1], out second))
                return false;
            return true;
        }
    }

    static class ChartDrawingExtensions {
        public static void Draw(this Node node, Chart chart, Color color, Int32 size = 5) {
            var series = new Series() {
                ChartType = SeriesChartType.Point,
                MarkerSize = size,
                Color = color,
                MarkerStyle = MarkerStyle.Circle,
            };
            series.Points.AddXY(node.X, node.Y);
            chart.Series.Add(series);
        }
        public static void Draw(this Wall wall, Chart chart, Color color, Int32 borderWidth = 3) {
            var series = new Series() {
                ChartType = SeriesChartType.Line,
                BorderWidth = borderWidth,
                Color = color,
            };
            series.Points.AddXY(wall.FirstNode.X, wall.FirstNode.Y);
            series.Points.AddXY(wall.SecondNode.X, wall.SecondNode.Y);
            chart.Series.Add(series);
        }
        public static void Draw(this List<Node> nodes, Chart chart, Color color, Int32 size = 5) =>
            nodes.ForEach(node => node.Draw(chart, color, size));
        public static void Draw(this List<Wall> walls, Chart chart, Color color, Int32 borderWidth = 3) =>
            walls.ForEach(wall => wall.Draw(chart, color, borderWidth));
        public static void Draw(this City city, Chart chart, Color nodeColor, Color wallColor, Int32 nodeSize = 10, Int32 wallWidth = 3) {
            city.Walls.Draw(chart, wallColor, wallWidth);
            city.Walls.GetAllNodes().ToList().Draw(chart, nodeColor, nodeSize);
        }
    }
}
