using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Orienty_MapManager
{
    public class GraphCanvas
    {
        Bitmap bitmap;
        Graphics graphics;

        Font font;
        Brush brushText = Brushes.Black;

        Color colorEdges = Color.FromArgb(0, 100, 100);

        Brush brushPavilion = Brushes.White;
        Brush brushJunktion;
        Brush brushExit = Brushes.Aquamarine;

        Pen penVertex;
        Pen penVertexSelected;
        Pen penEdge;

        public int rOfPavilion = 10;
        public int rOfJunktion = 6;
        public int rOfExit = 6;

        PointF point;

        // Walls
        Color wallsColor = Color.FromArgb(180, 180, 180);
        Color buildingBackgroundColor = Color.FromArgb(230, 230, 230);
        Pen penWalls;
        public Polygon outerWall = new Polygon();

        public GraphCanvas(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            graphics = Graphics.FromImage(bitmap);
            clearSheet();
            penVertex = new Pen(Color.Black, 2);
            penVertexSelected = new Pen(Color.Red, 2);
            penEdge = new Pen(colorEdges, 2);
            penWalls = new Pen(wallsColor, 5);
            font = new Font("Arial", 10);
            brushJunktion = new SolidBrush(colorEdges);
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void clearSheet()
        {
            graphics.Clear(Color.White);
        }

        public void drawVertex(Vertex vertex, bool isSelected = false)
        {
            int x = vertex.x;
            int y = vertex.y;
            int rOfVertex = GetRadiusOfVertex(vertex);

            graphics.FillEllipse(GetBrushOfVertex(vertex), 
                (x - rOfVertex), (y - rOfVertex), 2 * rOfVertex, 2 * rOfVertex);

            if (isSelected)
            {
                graphics.DrawEllipse(penVertexSelected, (x - rOfVertex), (y - rOfVertex), 2 * rOfVertex, 2 * rOfVertex);
            }
            else
            {
                graphics.DrawEllipse(penVertex, (x - rOfVertex), (y - rOfVertex), 2 * rOfVertex, 2 * rOfVertex);
            }

            if (vertex.type == E_NodeType.Pavilion)
            {
                point = new PointF(x + rOfVertex + 2, y - font.Height / 2);
                graphics.DrawString(vertex.name, font, brushText, point);
            }
        }

        

        private void DrawALLGraph(Graph graph, List<int> selectedV = null)
        { 
            List<Vertex> V = graph.V;
            List<Edge> E = graph.E;

            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                DrawEdge(E[i].v1, E[i].v2);
            }

            //рисуем вершины
            if (selectedV == null) {
                for (int i = 0; i < V.Count; i++)
                {
                    drawVertex(V[i]);
                }
            } else
            {
                for (int i = 0; i < V.Count; i++)
                {
                    drawVertex(V[i], selectedV.Contains(i));
                }
            }

            void DrawEdge(int v1, int v2)
            {
                graphics.DrawLine(penEdge, graph.V[v1].x, graph.V[v1].y, graph.V[v2].x, graph.V[v2].y);
            }
        }

        private void DrawPolygonOfWalls(Polygon polygon)
        {
            for (int i = polygon.isFinished ? 0 : 1; i < polygon.points.Count; i++)
            {
                int j = (i - 1 + polygon.points.Count) % polygon.points.Count;
                graphics.DrawLine(penWalls, polygon.points[i], polygon.points[j]);
            }

            if (polygon.isFinished)
            {
                graphics.FillPolygon(new SolidBrush(buildingBackgroundColor), polygon.points.ToArray());
            }
        }

        public void DrawEverything(Graph graph, List<int> selectedV = null)
        {
            DrawPolygonOfWalls(outerWall);
            DrawALLGraph(graph, selectedV);
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
}
