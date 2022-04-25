using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Orienty_MapManager
{
    public partial class Form1 : Form
    {
        public GraphCanvas canvas;
        public Graph graph;

        int selected1; //выбранные вершины, для соединения линиями
        int selected2;

        Dictionary<WhatDoing, Button> buttonsOfActions;

        WhatDoing whatDoing;

        bool shouldUpdateOnHover = true; // TODO только во время рисования стен и рёбер

        public Form1()
        {
            InitializeComponent();
            canvas = new GraphCanvas(sheet.Width, sheet.Height);
            graph = new Graph();

            buttonsOfActions = new Dictionary<WhatDoing, Button>();
            buttonsOfActions.Add(WhatDoing.Selecting, selectButton);
            buttonsOfActions.Add(WhatDoing.AddingEdges, drawEdgeButton);
            buttonsOfActions.Add(WhatDoing.AddingVertices, drawVertexButton);
            buttonsOfActions.Add(WhatDoing.Deleting, deleteButton);
            buttonsOfActions.Add(WhatDoing.DrawingPavilions, null); // TODO добавить кнопку рисования стен павильонов
            buttonsOfActions.Add(WhatDoing.DrawingOuterWall, B_drawOuterWalls);

            whatDoing = WhatDoing.AddingVertices;
            ResetAllSelections();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            ResetAllSelections(WhatDoing.Selecting);
        }

        private void drawVertexButton_Click(object sender, EventArgs e)
        {
            ResetAllSelections(WhatDoing.AddingVertices);
        }

        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            ResetAllSelections(WhatDoing.AddingEdges);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            ResetAllSelections(WhatDoing.Deleting);
        }

        private void B_drawOuterWalls_Click(object sender, EventArgs e)
        {
            bool allowedToDrawNewOuterWall = !canvas.outerWall.isFinished;

            if (!allowedToDrawNewOuterWall)
            {
                const string message = "Вы действительно хотите перерисовать внешнюю стену?";
                const string caption = "Внешняя стена уже существует!";
                var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                allowedToDrawNewOuterWall = MBSave == DialogResult.Yes;
            }

            if (allowedToDrawNewOuterWall)
            {
                canvas.outerWall.Reset();
                ResetAllSelections(WhatDoing.DrawingOuterWall);
            } 
            else
            {
                ResetAllSelections();
            }
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

        void ResetAllSelections(WhatDoing whatDoing = WhatDoing.Selecting)
        {
            this.whatDoing = whatDoing;

            Button buttonOfAction;
            buttonsOfActions.TryGetValue(whatDoing, out buttonOfAction);

            selected1 = -1;
            selected2 = -1;

            foreach (Button button in buttonsOfActions.Values)
            {
                if (button != null)
                {
                    button.Enabled = button != buttonOfAction;
                }
            }

            panelContextVertex.Visible = false;

            UpdateGraphImage();
        }

        private int getIdOfClickedVertex(MouseEventArgs e)
        {
            for (int i = 0; i < graph.V.Count; i++)
            {
                int rOfVertex = canvas.GetRadiusOfVertex(graph.V[i]);
                if (Math.Pow((graph.V[i].x - e.X), 2) + Math.Pow((graph.V[i].y - e.Y), 2) < Math.Pow(rOfVertex, 2))
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
                if (e.Button == MouseButtons.Left)
                {
                    Vertex vertex = new Vertex(e.X, e.Y, 0);
                    graph.V.Add(vertex);
                    UpdateGraphImage();
                }

                return;
            }

            if (whatDoing == WhatDoing.AddingEdges)
            {
                if (e.Button == MouseButtons.Left)
                {
                    int selectedV = getIdOfClickedVertex(e);
                    if (selectedV != -1) // клик по вершине
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
                }

                if (e.Button == MouseButtons.Right)
                {
                    ResetAllSelections(WhatDoing.AddingEdges);
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

                if (haveDeleted)
                {
                    UpdateGraphImage();
                }

                return;
            }

            if (whatDoing == WhatDoing.DrawingOuterWall)
            {
                if (canvas.outerWall.AddPointOfWall(e.Location))
                {
                    ResetAllSelections();
                }

                UpdateGraphImage();


                // TODO отмена рисования и прерывание рисования

                return;
            }

            if (whatDoing == WhatDoing.Selecting)
            {
                int selectedV = getIdOfClickedVertex(e);
                if (selectedV != -1) // клик по вершине
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        selected1 = selectedV;
                        ShowContextPanelVertex(selectedV);
                    }

                    UpdateGraphImage();
                }
                else
                {
                    ResetAllSelections();
                }
            }
        }

        private void ShowContextPanelVertex(int idOfVertex)
        {
            panelContextVertex.Visible = false;

            Vertex vertex = graph.V[idOfVertex];

            panelContextVertex.Location = vertex.GetPoint() + new Size(-canvas.rOfPavilion, canvas.rOfPavilion * 2 / 3);

            TB_Name.Visible = vertex.type == E_NodeType.Pavilion;

            TB_Name.Text = vertex.name;

            switch (vertex.type)
            {
                case E_NodeType.Junktion:
                    RB_Junktion.Checked = true;
                    break;
                case E_NodeType.Pavilion:
                    RB_Pavilion.Checked = true;
                    break;
                case E_NodeType.Exit:
                    RB_Exit.Checked = true;
                    break;
            }

            panelContextVertex.Visible = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string json = MapSerializer.SerializeMap(graph);
            TB_Debug.Text = json;
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

        private void TB_Name_TextChanged(object sender, EventArgs e)
        {
            if (!(sender as TextBox).Text.Trim().Equals(""))
            {
                graph.V[selected1].name = (sender as TextBox).Text.Trim();
            }
            else
            {
                graph.V[selected1].name = "Безымянный";
            }

            UpdateGraphImage();
        }

        private void TB_Name_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ResetAllSelections();
            }
        }

        private void RB_Type_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                switch ((sender as RadioButton).Tag)
                {
                    case "Junktion":
                        ChangeTypeOfSelectedVertex(E_NodeType.Junktion);
                        break;
                    case "Pavilion":
                        ChangeTypeOfSelectedVertex(E_NodeType.Pavilion);
                        break;
                    case "Exit":
                        ChangeTypeOfSelectedVertex(E_NodeType.Exit);
                        break;
                }
            }
        }

        private void ChangeTypeOfSelectedVertex(E_NodeType nodeType)
        {
            graph.V[selected1].type = nodeType;

            TB_Name.Visible = nodeType == E_NodeType.Pavilion;

            UpdateGraphImage();
        }
    }
}
