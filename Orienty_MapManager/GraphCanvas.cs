using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Orienty_MapManager
{
    public class GraphCanvas
    {
        Image backgrImage = Image.FromFile("../../Resources/grid.png");
        Bitmap bitmap;
        Graphics graphics;

        Font font;

        Color colorEdges = Color.FromArgb(245, 205, 159);
        Color colorText = Color.FromArgb(117, 124, 168);
        Color colorPavilion = Color.FromArgb(117, 124, 168);
        Color colorExit = Color.FromArgb(115, 176, 139);

        Brush brushText;
        Brush brushPavilion = Brushes.White;
        Brush brushJunktion;
        Brush brushExit;

        Pen penVertex;
        Pen penVertexSelected;
        Pen penEdge;
        Pen penEdgeHovered;

        public int rOfPavilion = 10;
        public int rOfJunktion = 6;
        public int rOfExit = 6;

        PointF point;

        // Walls
        Color wallsColor = Color.FromArgb(180, 180, 180);
        Color buildingBackgroundColor = Color.FromArgb(230, 230, 230);
        Color pavColor = Color.FromArgb(149, 149, 149);
        Color PavBorderColor = Color.FromArgb(49, 49, 49);
        Pen penWalls, penPav;
        public Polygon outerWall = new Polygon();


        public List<Polygon> Pavilions { get; set; } = new List<Polygon>();

       

        public GraphCanvas(int width, int height)
        {
            bitmap = new Bitmap(width, height);

            graphics = Graphics.FromImage(bitmap);
            clearSheet();

            penVertex = new Pen(Color.Black, 2);
            penVertexSelected = new Pen(Color.Red, 2);
            penEdge = new Pen(colorEdges, 2);
            penEdgeHovered = new Pen(colorEdges, 3);
            penWalls = new Pen(wallsColor, 3);
            penPav = new Pen(PavBorderColor, 3);

            font = new Font("Arial", 10);

            brushJunktion = new SolidBrush(colorEdges);
            brushText = new SolidBrush(colorText);
            brushExit = new SolidBrush(colorExit);
            brushPavilion = new SolidBrush(colorPavilion);
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void clearSheet()
        {
            graphics.Clear(Color.Transparent);
        }

        public void drawVertex(Vertex vertex, bool isHovered = false, bool isSelected = false)
        {
            int x = vertex.x;
            int y = vertex.y;
            int rOfVertex = GetRadiusOfVertex(vertex) + (isHovered ? 1 : 0);

            graphics.FillEllipse(GetBrushOfVertex(vertex), 
                (x - rOfVertex), (y - rOfVertex), 2 * rOfVertex, 2 * rOfVertex);

            graphics.DrawEllipse(isSelected ? penVertexSelected : penVertex, (x - rOfVertex), (y - rOfVertex), 2 * rOfVertex, 2 * rOfVertex);

            if (vertex.type == E_NodeType.Pavilion)
            {
                point = new PointF(x + rOfVertex + 2, y - font.Height / 2);
                graphics.DrawString(vertex.name, font, brushText, point);
            }
        }

        

        private void DrawALLGraph(Graph graph, int vertexHovered = -1, Edge edgeHovered = null, List<int> selectedV = null)
        { 
            List<Vertex> V = graph.V;
            List<Edge> E = graph.E;

            //рисуем ребра
            foreach (Edge edge in E)
            {
                DrawEdge(edge.v1, edge.v2, edgeHovered == edge || vertexHovered == edge.v1 || vertexHovered == edge.v2);
            }

            //рисуем вершины
            if (selectedV == null) {
                for (int i = 0; i < V.Count; i++)
                {
                    drawVertex(V[i], i == vertexHovered);
                }
            } else
            {
                for (int i = 0; i < V.Count; i++)
                {
                    drawVertex(V[i], i == vertexHovered, selectedV.Contains(i));
                }
            }

            void DrawEdge(int v1, int v2, bool hovered)
            {
                graphics.DrawLine(hovered ? penEdgeHovered : penEdge, graph.V[v1].x, graph.V[v1].y, graph.V[v2].x, graph.V[v2].y);
            }
        }
        public void DrawEdge(Point v1, Point v2)
        {
            graphics.DrawLine(penEdge, v1, v2);
        }

        private void DrawPolygonOfWalls(Polygon polygon, Pen pen, Color brush)
        {
            for (int i = polygon.isFinished ? 0 : 1; i < polygon.points.Count; i++)
            {
                int j = (i - 1 + polygon.points.Count) % polygon.points.Count;
                graphics.DrawLine(pen, polygon.points[i], polygon.points[j]);
            }

            if (polygon.isFinished)
            {
                graphics.FillPolygon(new SolidBrush(brush), polygon.points.ToArray());
            }
        }

        public void DrawEverything(Graph graph, int vertexHovered, Edge edgeHovered, List<int> selectedV = null, List<PairPoints> extraLines = null)
        {
            clearSheet();

            DrawPolygonOfWalls(outerWall, penWalls, buildingBackgroundColor);

            //draw pavilions
            foreach(var pav in Pavilions)
            {
                DrawPolygonOfWalls(pav, penPav, pavColor);
            }

            DrawALLGraph(graph, vertexHovered, edgeHovered, selectedV);

            if (extraLines != null)
            {
                foreach (var line in extraLines)
                {
                    DrawEdge(line.p1, line.p2);
                }
            }
        }

        public int GetRadiusOfVertex(Vertex vertex)
        {
            switch (vertex.type)
            {
                case E_NodeType.Junktion:
                    return rOfJunktion;
                case E_NodeType.Pavilion:
                    return rOfPavilion;
                case E_NodeType.Exit:
                    return rOfExit;
                default: 
                    return rOfPavilion;
            }
        }

        public Brush GetBrushOfVertex(Vertex vertex)
        {
            switch (vertex.type)
            {
                case E_NodeType.Junktion:
                    return brushJunktion;
                case E_NodeType.Pavilion:
                    return brushPavilion;
                case E_NodeType.Exit:
                    return brushExit;
                default:
                    return brushPavilion;
            }
        }
    }

    public class PairPoints
    {
        public Point p1;
        public Point p2;

        public PairPoints(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
    }
}
