﻿using System.Collections.Generic;
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

            graphics.DrawString(Program.form.graph.GetNameOfVertexById(vertex.id), font, brush, point); 
        }

        public void drawEdge(Vertex V1, Vertex V2, Edge E, string nameOfEdge)
        {
            if (E.v1 == E.v2)
            {
                graphics.DrawArc(penEdge, (V1.x - 2 * rOfVertex), (V1.y - 2 * rOfVertex), 2 * rOfVertex, 2 * rOfVertex, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * rOfVertex), V1.y - (int)(2.75 * rOfVertex));
                graphics.DrawString(nameOfEdge, font, brush, point);
                drawVertex(V1);
            }
            else
            {
                graphics.DrawLine(penEdge, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                graphics.DrawString(nameOfEdge, font, brush, point);
                drawVertex(V1);
                drawVertex(V2);
            }
        }

        public void drawALLGraph(Graph graph)
        { 
            List<Vertex> V = graph.V;
            List<Edge> E = graph.E;

            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].v1 == E[i].v2)
                {
                    graphics.DrawArc(penEdge, (V[E[i].v1].x - 2 * rOfVertex), (V[E[i].v1].y - 2 * rOfVertex), 2 * rOfVertex, 2 * rOfVertex, 90, 270);
                    point = new PointF(V[E[i].v1].x - (int)(2.75 * rOfVertex), V[E[i].v1].y - (int)(2.75 * rOfVertex));
                    graphics.DrawString(((char)('a' + i)).ToString(), font, brush, point);
                }
                else
                {
                    graphics.DrawLine(penEdge, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
                    point = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
                    graphics.DrawString(((char)('a' + i)).ToString(), font, brush, point);
                }
            }
            //рисуем вершины
            for (int i = 0; i < V.Count; i++)
            {
                drawVertex(V[i]);
            }
        }
    }
}