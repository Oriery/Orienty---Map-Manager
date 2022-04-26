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

        Image backgrImage = Image.FromFile("../../Resources/grid.png");

        int selected1;

        Dictionary<WhatDoing, Button> buttonsOfActions;

        WhatDoing whatDoing;

        int vertexHovered = -1;
        Edge edgeHovered = null;

        ToolTip t = new ToolTip();

        public Form1()
        {
            InitializeComponent();
            canvas = new GraphCanvas(sheet.Width, sheet.Height);
            graph = new Graph();

            buttonsOfActions = new Dictionary<WhatDoing, Button>();
            buttonsOfActions.Add(WhatDoing.Selecting, selectButton);
            buttonsOfActions.Add(WhatDoing.DrawingGraph, drawEdgeButton);
            buttonsOfActions.Add(WhatDoing.Deleting, deleteButton);
            buttonsOfActions.Add(WhatDoing.DrawingPavilions, draw_Pav);
            buttonsOfActions.Add(WhatDoing.DrawingOuterWall, B_drawOuterWalls);

            t.SetToolTip(B_drawOuterWalls, "Рисовать схему здания");
            t.SetToolTip(draw_Pav, "Рисовать павильоны");

            ResetAllSelections(WhatDoing.DrawingGraph);
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            ResetAllSelections(WhatDoing.Selecting);
        }

        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            ResetAllSelections(WhatDoing.DrawingGraph);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            ResetAllSelections(WhatDoing.Deleting);
        }

        private void draw_Pav_Click(object sender, EventArgs e)
        {
            ResetAllSelections(WhatDoing.DrawingPavilions);
        }

        private void B_drawOuterWalls_Click(object sender, EventArgs e)
        {
            bool allowedToDrawNewOuterWall = !canvas.outerWall.isFinished;

            if (!allowedToDrawNewOuterWall)
            {
                const string message = "Вы действительно хотите перерисовать схему здания и тем самым удалив все павильоны?";
                const string caption = "Внешняя стена уже существует!";
                var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                allowedToDrawNewOuterWall = MBSave == DialogResult.Yes;
            }

            if (allowedToDrawNewOuterWall)
            {
                canvas.outerWall.Reset();
                foreach(var pav in canvas.Pavilions)
                {
                    pav.Reset();
                }
                canvas.Pavilions.Clear();
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
                ResetAllSelections(WhatDoing.DrawingGraph);
            }
        }

        void ResetAllSelections(WhatDoing whatDoing = WhatDoing.Selecting)
        {
            this.whatDoing = whatDoing;

            Button buttonOfAction;
            buttonsOfActions.TryGetValue(whatDoing, out buttonOfAction);

            selected1 = -1;

            foreach (Button button in buttonsOfActions.Values)
            {
                if (button != null)
                {
                    button.Enabled = button != buttonOfAction;
                }
            }

            panelContextVertex.Visible = false;

            if (!canvas.outerWall.isFinished)
            {
                canvas.outerWall.Reset();
            }

            UpdateGraphImage();
        }

        private int GetIdOfUnderlyingVertex(MouseEventArgs e)
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

        private Edge GetUnderlyingEdge(MouseEventArgs e)
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
                    float ax = (float)dx_ / dx;
                    if (Math.Abs(dx_ * dy / dx - dy_) < 10 && (ax >= 0) && (ax <= 1)) 
                    {
                        return edge;
                    }
                } 
                else
                {
                    float ay = (float)dy_ / dy;
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
            if (whatDoing == WhatDoing.DrawingGraph)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (vertexHovered != -1) // Рисуем ребро
                    {
                        if (selected1 == -1) // Новое ребро
                        {
                            StartNewEdge(vertexHovered);
                        }
                        else if (vertexHovered != selected1) // Новое ребро оканчивается на существующей вершине
                        {
                            FinishNewEdge(selected1, vertexHovered);
                            StartNewEdge(vertexHovered);
                        }
                    } 
                    else // Новая вершина
                    {
                        int id = CreateNewVertex(e.X, e.Y, 0);

                        if (selected1 != -1) // Рисовали ребро
                        {
                            FinishNewEdge(selected1, id);
                        }

                        StartNewEdge(id);
                    }
                }

                if (e.Button == MouseButtons.Right)
                {
                    ResetAllSelections(WhatDoing.DrawingGraph);
                }

                return;
            }

            if (whatDoing == WhatDoing.Deleting)
            {
                DeleteClicked(e);

                return;
            }

            if (whatDoing == WhatDoing.DrawingOuterWall)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (canvas.outerWall.AddPointOfWall(e.Location))
                    {
                        ResetAllSelections();
                        t.SetToolTip(B_drawOuterWalls, "Перерисовать схему здания");
                    }
                }

                if (e.Button == MouseButtons.Right)
                {
                    canvas.outerWall.Reset();
                }

                UpdateGraphImage();

                return;
            }

            if (whatDoing == WhatDoing.Selecting)
            {
                if (vertexHovered != -1) // клик по вершине
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        selected1 = vertexHovered;
                        ShowContextPanelVertex(vertexHovered);
                    }

                    UpdateGraphImage();
                }
                else
                {
                    ResetAllSelections();
                }

                return;
            }

            if(whatDoing == WhatDoing.DrawingPavilions)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (Polygon.IsPointInPolygon(e.Location, canvas.outerWall.points)) // check is point inside outerwall
                    {
                        if (GetClickedPolygon(e.Location, canvas.Pavilions, false) == -1) // if not ckieck on exist polygon
                        {
                            if (canvas.Pavilions.Count == 0)
                            {
                                canvas.Pavilions.Add(new Polygon());
                            }
                            else if (canvas.Pavilions[canvas.Pavilions.Count - 1].isFinished)
                            {
                                canvas.Pavilions.Add(new Polygon());
                            }

                            if (canvas.Pavilions[canvas.Pavilions.Count - 1].AddPointOfWall(e.Location))
                            {
                                ResetAllSelections(WhatDoing.DrawingPavilions);
                            }
                        }
                    }
                }

                if (e.Button == MouseButtons.Right)
                {
                    
                    int selectedP = GetClickedPolygon(e.Location, canvas.Pavilions);
                    if(selectedP != -1)
                    {
                        const string message = "Вы действительно хотите удалить павильон?";
                        const string caption = "Предупреждение";
                        var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if(MBSave == DialogResult.Yes)
                        {
                            canvas.Pavilions.RemoveAt(selectedP);
                        }
                    }
                }

                UpdateGraphImage();

                return;
            }
        }


        private int GetClickedPolygon(Point mouseP, List<Polygon> polygons, bool checkLast = true)
        {

            int last;
            if(checkLast)
                last = polygons.Count - 1;
            else
                last = polygons.Count - 2;
            for (int i = 0; i <= last; ++i)
            {
                if (Polygon.IsPointInPolygon(mouseP, polygons[i].points))
                {
                    return i;
                }
            }

            return-1; //not selected polygon
        }

        private void DeleteClicked(MouseEventArgs e)
        {
            bool haveDeleted = false;

            haveDeleted = graph.DeleteVertex(vertexHovered); // клик по вершине

            if (!haveDeleted)
            {
                haveDeleted = graph.DeleteEdge(edgeHovered); // клик по ребру
            }

            if (haveDeleted)
            {
                UpdateGraphImage();
            }
        }

        private int CreateNewVertex(int x, int y, int z)
        {
            Vertex vertex = new Vertex(x, y, z);
            graph.V.Add(vertex);
            return vertex.id;
        }

        private void StartNewEdge(int idOfNode)
        {
            selected1 = idOfNode;
            UpdateGraphImage();
        }

        private void FinishNewEdge(int v1, int v2)
        {
            graph.E.Add(new Edge(v1, v2));
            graph.V[v1].arrIDs.Add(v2);
            graph.V[v2].arrIDs.Add(v1);
            selected1 = -1;
            UpdateGraphImage();
        }

        private void ShowContextPanelVertex(int idOfVertex)
        {
            panelContextVertex.Visible = false;

            Vertex vertex = graph.V[idOfVertex];

            Point pos = vertex.GetPoint() + new Size(-canvas.rOfPavilion, canvas.rOfPavilion * 2 / 3);
            pos.X = Math.Min(pos.X, mainPanel.Width - panelContextVertex.Width);
            pos.X = Math.Max(pos.X, 0);
            if (pos.Y + panelContextVertex.Height > mainPanel.Height)
            {
                pos.Y = vertex.GetPoint().Y - canvas.rOfPavilion * 2 / 3 - panelContextVertex.Height;
            }
            panelContextVertex.Location = pos;

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
            TB_Debug.Visible = true;
        }

        private void FillBackgroundImage()
        {
           sheet.BackgroundImage = backgrImage;
        }

        private void UpdateGraphImage(List<PairPoints> extraLines = null)
        {
            
            canvas.clearSheet();
            FillBackgroundImage();
            canvas.DrawEverything(graph, vertexHovered, edgeHovered, new List<int>() { selected1 }, extraLines);
            sheet.Image = canvas.GetBitmap();

        }

        private enum WhatDoing
        {
            DrawingGraph,
            Deleting,
            Selecting,
            DrawingPavilions,
            DrawingOuterWall
        }

        private void sheet_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateHoveredElements(e);

            if (whatDoing == WhatDoing.DrawingGraph && selected1 != -1)
            {
                UpdateGraphImage(new List<PairPoints> { new PairPoints(graph.V[selected1].GetPoint(), e.Location) });
                return;
            }

            UpdateGraphImage();
        }
        private void UpdateHoveredElements(MouseEventArgs e)
        {
            vertexHovered = GetIdOfUnderlyingVertex(e);
            if (vertexHovered == -1)
            {
                edgeHovered = GetUnderlyingEdge(e);
            }
            else
            {
                edgeHovered = null;
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
