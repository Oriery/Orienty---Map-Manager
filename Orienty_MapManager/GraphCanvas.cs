using System.Collections.Generic;
using System.Drawing;

namespace Orienty_MapManager
{
    public class GraphCanvas
    {
        Bitmap bitmap;
        Pen penVertex;
        Pen penVertexSelected;
        Pen penEdge;
        Graphics graphics;
        Font font;
        Brush brush;
        PointF point;
        public int rOfVertex = 20;

        public GraphCanvas(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            graphics = Graphics.FromImage(bitmap);
            clearSheet();
            penVertex = new Pen(Color.Black);
            penVertex.Width = 2;
            penVertexSelected = new Pen(Color.Red);
            penVertexSelected.Width = 2;
            penEdge = new Pen(Color.DarkGoldenrod);
            penEdge.Width = 2;
            font = new Font("Arial", 15);
            brush = Brushes.Black;
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

            graphics.FillEllipse(Brushes.White, (x - rOfVertex), (y - rOfVertex), 2 * rOfVertex, 2 * rOfVertex);
            if (isSelected)
            {
                graphics.DrawEllipse(penVertexSelected, (x - rOfVertex), (y - rOfVertex), 2 * rOfVertex, 2 * rOfVertex);
            }
            else
            {
                graphics.DrawEllipse(penVertex, (x - rOfVertex), (y - rOfVertex), 2 * rOfVertex, 2 * rOfVertex);
            }
            point = new PointF(x - rOfVertex + 2, y - font.Height / 2);

            graphics.DrawString(vertex.name, font, brush, point); 
        }

        public void drawEdge(Vertex V1, Vertex V2)
        {
            graphics.DrawLine(penEdge, V1.x, V1.y, V2.x, V2.y);
            drawVertex(V1);
            drawVertex(V2);
        }

        public void drawALLGraph(Graph graph, List<int> selectedV = null)
        { 
            List<Vertex> V = graph.V;
            List<Edge> E = graph.E;

            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                graphics.DrawLine(penEdge, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
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
        }
    }
}
