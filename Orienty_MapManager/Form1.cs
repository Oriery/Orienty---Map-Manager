using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Orienty_MapManager
{
    public partial class Form1 : Form
    {
        public GraphCanvas canvas;
        public Graph graph;

        int selected1; //выбранные вершины, для соединения линиями
        int selected2;

        List<Button> buttonGroup;

        WhatDoing whatDoing;

        bool shouldUpdateOnHover = true; // TODO только во время рисования стен и рёбер

        public Form1()
        {
            InitializeComponent();
            canvas = new GraphCanvas(sheet.Width, sheet.Height);
            graph = new Graph();

            buttonGroup = new List<Button>() { drawEdgeButton, drawVertexButton, deleteButton, selectButton, B_drawOuterWalls };

            whatDoing = WhatDoing.AddingVertices;
            ResetAllSelections();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            whatDoing = WhatDoing.Selecting;
            ResetAllSelections(sender);
        }

        private void drawVertexButton_Click(object sender, EventArgs e)
        {
            whatDoing = WhatDoing.AddingVertices;
            ResetAllSelections(sender);
        }

        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            whatDoing = WhatDoing.AddingEdges;
            ResetAllSelections(sender);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            whatDoing = WhatDoing.Deleting;
            ResetAllSelections(sender);
        }

        private void B_drawOuterWalls_Click(object sender, EventArgs e)
        {
            whatDoing = WhatDoing.DrawingOuterWall;
            ResetAllSelections(sender);
        }

        private void deleteALLButton_Click(object sender, EventArgs e)
        {
            const string message = "Вы действительно хотите полностью удалить граф?";
            const string caption = "Удаление";
            var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MBSave == DialogResult.Yes)
            {
                graph.Clear();
                UpdateGraphImage();
            }
        }

        void ResetAllSelections(object sender = null)
        {
            selected1 = -1;
            selected2 = -1;

            foreach (Button button in buttonGroup)
            {
                button.Enabled = button != sender;
            }

            UpdateGraphImage();
        }

        private int getIdOfClickedVertex(MouseEventArgs e)
        {
            for (int i = 0; i < graph.V.Count; i++)
            {
                if (Math.Pow((graph.V[i].x - e.X), 2) + Math.Pow((graph.V[i].y - e.Y), 2) < Math.Pow(canvas.rOfVertex, 2))
                {
                    return graph.V[i].id;
                }
            }

            // TODO если друг на друга накладываются, то вибирать ближайший

            return -1;
        }

        private Edge getClickedEdge(MouseEventArgs e)
        {
            Edge edge;
            Vertex v1, v2;
            for (int i = 0; i < graph.E.Count; i++)
            {
                edge = graph.E[i];
                v1 = graph.V[edge.v1];
                v2 = graph.V[edge.v2];

                int dx = (v2.x - v1.x);
                int dy = (v2.y - v1.y);
                int dx_ = (e.X - v1.x);
                int dy_ = (e.Y - v1.y);
                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    float ax = dx_ / dx;
                    if (Math.Abs(dx_ * dy / dx - dy_) < 10 && (ax >= 0) && (ax <= 1)) 
                    {
                        return edge;
                    }
                } 
                else
                {
                    float ay = dy_ / dy;
                    if (Math.Abs(dy_ * dx / dy - dx_) < 10 && (ay >= 0) && (ay <= 1))
                    {
                        return edge;
                    }
                }
            }
            return null;
        }

        private void sheet_MouseClick(object sender, MouseEventArgs e)
        {
            if (whatDoing == WhatDoing.AddingVertices)
            {
                Vertex vertex = new Vertex(e.X, e.Y, 0);
                vertex.name = "abc";
                graph.V.Add(vertex);
                UpdateGraphImage();

                return;
            }

            if (whatDoing == WhatDoing.AddingEdges)
            {
                int selectedV = getIdOfClickedVertex(e);
                if (selectedV != -1) // клик по вершине
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (selected1 == -1)
                        {
                            selected1 = selectedV;
                            UpdateGraphImage();
                        }
                        else if (selectedV != selected1)
                        {
                            selected2 = selectedV;
                            graph.E.Add(new Edge(selected1, selected2));
                            graph.V[selected1].arrIDs.Add(selected2);
                            graph.V[selected2].arrIDs.Add(selected1);
                            selected1 = -1;
                            selected2 = -1;
                            UpdateGraphImage();
                        }
                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        if (selected1 != -1 && selectedV == selected1)
                        {
                            selected1 = -1;
                            UpdateGraphImage();
                        }
                    }
                }

                return;
            }

            if (whatDoing == WhatDoing.Deleting)
            {
                bool haveDeleted = false;

                haveDeleted = graph.DeleteVertex(getIdOfClickedVertex(e)); // клик по вершине

                if (!haveDeleted)
                {
                    haveDeleted = graph.DeleteEdge(getClickedEdge(e)); // клик по ребру
                }

                // обновляем граф на экране
                if (haveDeleted)
                {
                    UpdateGraphImage();
                }

                return;
            }

            if (whatDoing == WhatDoing.DrawingOuterWall)
            {
                canvas.polygon.AddPointOfWall(e.Location);
                UpdateGraphImage();

                // TODO отмена рисования и прерывание рисования
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string json = MapSerializer.SerializeMap(graph);
            textBox1.Text = json;
        }

        private void UpdateGraphImage()
        {
            canvas.clearSheet();
            canvas.DrawEverything(graph, new List<int>() { selected1, selected2 });
            sheet.Image = canvas.GetBitmap();
        }

        private enum WhatDoing
        {
            AddingVertices,
            AddingEdges,
            Deleting,
            Selecting,
            DrawingPavilions,
            DrawingOuterWall
        }

        private void sheet_MouseMove(object sender, MouseEventArgs e)
        {
            if (shouldUpdateOnHover)
            {
                // TODO рисовать полностью канвас
            }
        }
    }
}
