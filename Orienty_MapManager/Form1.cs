using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Orienty_MapManager
{
    public partial class Form1 : Form
    {
        public GraphCanvas canvas;
        public Graph graph;

        string graphJson; //json for graph


        Image backgroundImage = Image.FromFile("../../Resources/grid.png");

        Dictionary<WhatDoing, Button> buttonsOfActions;

        WhatDoing whatDoing;

        int vertexHovered = -1;
        Edge edgeHovered = null;
        Beacon beaconHovered = null;
        int vertexSelected = -1;
        Beacon beaconSelected = null;


        const string PATHMAP = "../../Resources/map/map.png";
        const string PATHGRAPH = "../../Resources/json/graph.json";
        const string PATHBUILD = "../../Resources/json/";

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
            buttonsOfActions.Add(WhatDoing.DrawingBeacons, B_DrawBeacons);

            t.SetToolTip(B_drawOuterWalls, "Рисовать схему здания");
            t.SetToolTip(draw_Pav, "Рисовать павильоны");

            ResetAllSelections(WhatDoing.DrawingGraph);

            SetBackgroundImage(backgroundImage);
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

        private void B_DrawBeacons_Click(object sender, EventArgs e)
        {
            ResetAllSelections(WhatDoing.DrawingBeacons);
        }

        private void Mouse_move_draw_Pavs(object sender, MouseEventArgs e)
        {
            if(canvas.Pavilions.Count > 0)
            {
               // Point currectPoint = Polygon.GetCorrectPoint(e.Location, canvas.outerWall, 
                //    canvas.Pavilions[canvas.Pavilions.Count - 1].points[canvas.Pavilions[canvas.Pavilions.Count - 1].points.Count - 1]);
                canvas.DrawPavLine(e.Location);
                
               // canvas.DrawPavLine(currectPoint);
                //  if(canvas.Pavilions[canvas.Pavilions.Count-1].isFinished)
                //  sheet.MouseMove -= Mouse_move_draw_Pavs;
            }
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

            vertexSelected = -1;
            beaconSelected = null;

            foreach (Button button in buttonsOfActions.Values)
            {
                if (button != null)
                {
                    button.Enabled = button != buttonOfAction;
                }
            }

            panelContextVertex.Visible = false;
            panelContextBeacon.Visible = false;

            if (!canvas.outerWall.isFinished)
            {
                canvas.outerWall.Reset();
            }

            UpdateGraphImage();
        }

        private int GetIdOfUnderlyingVertex(Point point)
        {
            for (int i = 0; i < graph.V.Count; i++)
            {
                int rOfVertex = canvas.GetRadiusOfVertex(graph.V[i].type) * 2;
                if (IsPointOverCircle(point, graph.V[i].GetPoint(), rOfVertex))
                {
                    return graph.V[i].id;
                }
            }
            return -1;
        }

        private Beacon GetUnderlyingBeacon(Point point)
        {
            foreach (var beacon in graph.beacons)
            {
                if (IsPointOverCircle(point, beacon.GetPoint(), canvas.rOfBeacon))
                {
                    return beacon;
                }
            }
            return null;
        }

        private bool IsPointOverCircle(Point point, Point centre, int radius)
        {
            return Math.Pow((centre.X - point.X), 2) + Math.Pow((centre.Y - point.Y), 2) < Math.Pow(radius, 2);
        }

        private Edge GetUnderlyingEdge(MouseEventArgs e)
        {
            Edge edge;
            Vertex v1, v2;
            float alpha;
            for (int i = 0; i < graph.E.Count; i++)
            {
                edge = graph.E[i];
                v1 = graph.V[edge.v1];
                v2 = graph.V[edge.v2];

                int dx = (v2.x - v1.x);
                int dy = (v2.y - v1.y);
                int dx_ = (e.X - v1.x);
                int dy_ = (e.Y - v1.y);

                if (Math.Abs(dy) > Math.Abs(dx))
                {
                    (dx, dy) = (dy, dx);
                    (dx_, dy_) = (dy_, dx_);
                }
                dx = dx == 0 ? 1 : dx;
                dy = dy == 0 ? 1 : dy;

                alpha = (float)dx_ / dx;
                if (Math.Abs(dx_ * dy / dx - dy_) < 10 && (alpha >= 0) && (alpha <= 1))
                {
                    return edge;
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
                        if (vertexSelected == -1) // Новое ребро
                        {
                            StartNewEdge(vertexHovered);
                        }
                        else if (vertexHovered != vertexSelected) // Новое ребро оканчивается на существующей вершине
                        {
                            FinishNewEdge(vertexSelected, vertexHovered);
                            StartNewEdge(vertexHovered);
                        }
                    } 
                    else // Новая вершина
                    {
                        int id = CreateNewVertex(e.X, e.Y, 0);

                        if (vertexSelected != -1) // Рисовали ребро
                        {
                            FinishNewEdge(vertexSelected, id);
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

            if (whatDoing == WhatDoing.DrawingBeacons)
            {
                if (e.Button == MouseButtons.Left)
                {
                    graph.beacons.Add(new Beacon(e.X, e.Y, 0));
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
                        ResetAllSelections(WhatDoing.DrawingPavilions);
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
                        vertexSelected = vertexHovered;
                        ShowContextPanelVertex(vertexHovered);
                    }

                    UpdateGraphImage();
                }
                else if (beaconHovered != null) // клик по маячку
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        beaconSelected = beaconHovered;
                        ShowContextPanelBeacon(beaconSelected);
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
                            if(canvas.Pavilions[canvas.Pavilions.Count - 1].points.Count==0)
                                sheet.MouseMove += Mouse_move_draw_Pavs;
                            if (canvas.Pavilions[canvas.Pavilions.Count - 1].AddPointOfWall(e.Location))
                            {
                                ResetAllSelections(WhatDoing.DrawingPavilions);
                                sheet.MouseMove -= Mouse_move_draw_Pavs;
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
                    
                    if (!canvas.Pavilions[canvas.Pavilions.Count-1].isFinished)
                    {
                        canvas.Pavilions.RemoveAt(canvas.Pavilions.Count - 1);
                        sheet.MouseMove -= Mouse_move_draw_Pavs;
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
            int id;
            if (edgeHovered != null)
            {
                Point a1 = graph.V[edgeHovered.v1].GetPoint();
                Point a2 = graph.V[edgeHovered.v2].GetPoint();
                Point pointOnEdge = GetPointOnLineNearestToPoint(a1, a2, new Point(x, y));
                id = graph.InsertVertexIntoEdge(pointOnEdge.X, pointOnEdge.Y, z, edgeHovered);
            }
            else
            {
                id = graph.AddVertex(x, y, z);
            }

            return id;
        }

        private Point GetPointOnLineNearestToPoint(Point a1, Point a2, Point b)
        {
            int dx = (a2.X - a1.X);
            int dy = (a2.Y - a1.Y);
            dx = dx == 0 ? 1 : dx;
            dy = dy == 0 ? 1 : dy;
            bool swaped = false;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                (dx, dy) = (dy, dx);
                (a1.X, a1.Y) = (a1.Y, a1.X);
                (a2.X, a2.Y) = (a2.Y, a2.X);
                (b.X, b.Y) = (b.Y, b.X);
                swaped = true;
            }

            int y = (int)((float)(b.Y * dy * dy + a1.Y * dx * dx + dx * dy * (b.X - a1.X)) / (dx * dx + dy * dy));
            int x = (int)(a1.X + (y - a1.Y) * ((float)dx / dy));

            if (swaped)
            {
                (x, y) = (y, x);
            }

            return new Point(x, y);
        }

        private void StartNewEdge(int idOfNode)
        {
            vertexSelected = idOfNode;
            UpdateGraphImage();
        }

        private void FinishNewEdge(int v1, int v2)
        {
            if (graph.AddEdge(v1, v2))
            {
                vertexSelected = -1;
                UpdateGraphImage();
            }
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

        private void ShowContextPanelBeacon(Beacon beacon)
        {
            panelContextBeacon.Visible = false;

            Point pos = beacon.GetPoint() + new Size(-canvas.rOfPavilion, canvas.rOfPavilion * 2 / 3);
            pos.X = Math.Min(pos.X, mainPanel.Width - panelContextBeacon.Width);
            pos.X = Math.Max(pos.X, 0);
            if (pos.Y + panelContextBeacon.Height > mainPanel.Height)
            {
                pos.Y = beacon.GetPoint().Y - canvas.rOfPavilion * 2 / 3 - panelContextBeacon.Height;
            }
            panelContextBeacon.Location = pos;

            NUD_txPower.Value = beacon.tx_power;
            TB_Mac.Text = beacon.mac;

            panelContextBeacon.Visible = true;
        }

        private void SaveJPG100(Bitmap bmp, string filename)
        {
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            bmp.Save(filename, GetEncoder(ImageFormat.Png), encoderParameters);
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            graphJson = MapSerializer.SerializeMap(graph);
            TB_Debug.Text = graphJson;
            TB_Debug.Visible = true;
            //save graph
            using (var stream = new StreamWriter(PATHGRAPH))
            {
                stream.Write(graphJson);
            }
            //save build 
            saveFileDialog1.Filter = "Json Files (json)|*.json";
            saveFileDialog1.Title = "Сохранить файл здания";
            saveFileDialog1.FileName = "build.json";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                var pathInput = saveFileDialog1.FileName;

                MapSerializer.SerializeBuild(pathInput, canvas.outerWall);

            }
            saveFileDialog1.Title = "Сохранить файл павильонов";
            saveFileDialog1.FileName = "pavilions.json";
            //save pavilions
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                var pathInput = saveFileDialog1.FileName;

                MapSerializer.SerializePavs(pathInput, canvas.Pavilions);

            }

            //save map 
            var mapImg = canvas.SaveMap();

            SaveJPG100((Bitmap)mapImg, PATHMAP);

            sendServer.Enabled = true;
        }

        private void SetBackgroundImage(Image image)
        {
           sheet.BackgroundImage = backgroundImage;
        }

        private void UpdateGraphImage(PairPoints extraLine = null, bool drawExtraVertex = false)
        {
            if (whatDoing == WhatDoing.Selecting || whatDoing == WhatDoing.Deleting)
            {
                canvas.DrawEverything(graph, vertexHovered, edgeHovered, beaconHovered, vertexSelected, beaconSelected);
            }
            else if (whatDoing == WhatDoing.DrawingGraph)
            {
                canvas.DrawEverything(graph, vertexHovered, edgeHovered, null, vertexSelected, null, extraLine, drawExtraVertex);

            }
            else if (whatDoing == WhatDoing.DrawingBeacons)
            {
                canvas.DrawEverything(graph, -1, null, beaconHovered, -1, beaconSelected);
            }
            else
            {
                canvas.DrawEverything(graph);
            }

            sheet.Image = canvas.GetBitmap();
        }

        private enum WhatDoing
        {
            DrawingGraph,
            Deleting,
            Selecting,
            DrawingPavilions,
            DrawingOuterWall,
            DrawingBeacons
        }

        private void sheet_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateHoveredElements(e);

            if (whatDoing == WhatDoing.DrawingGraph && (vertexSelected != -1 || edgeHovered != null))
            {
                Point pointMouse;
                if (edgeHovered != null)
                {
                    Point a1 = graph.V[edgeHovered.v1].GetPoint();
                    Point a2 = graph.V[edgeHovered.v2].GetPoint();
                    pointMouse = GetPointOnLineNearestToPoint(a1, a2, e.Location);
                }
                else
                {
                    if (vertexHovered != -1)
                    {
                        pointMouse = graph.V[vertexHovered].GetPoint();
                    }
                    else
                    {
                        pointMouse = e.Location;
                    }
                }

                PairPoints pairPoints;
                if (vertexSelected != -1)
                {
                    pairPoints = new PairPoints(graph.V[vertexSelected].GetPoint(), pointMouse);
                }
                else
                {
                    pairPoints = new PairPoints(pointMouse, pointMouse); // Костыль
                }

                UpdateGraphImage(pairPoints, edgeHovered != null);
                return;
            }

            UpdateGraphImage();
        }
        private void UpdateHoveredElements(MouseEventArgs e)
        {
            bool foundHovered;

            beaconHovered = GetUnderlyingBeacon(e.Location);
            foundHovered = beaconHovered != null;

            if (!foundHovered)
            {
                vertexHovered = GetIdOfUnderlyingVertex(e.Location);
                foundHovered = vertexHovered != -1;
            }
            else
            {
                vertexHovered = -1;
            }

            if (!foundHovered)
            {
                edgeHovered = GetUnderlyingEdge(e);
                foundHovered = edgeHovered != null;
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
                graph.V[vertexSelected].name = (sender as TextBox).Text.Trim();
            }
            else
            {
                graph.V[vertexSelected].name = "Безымянный";
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
            graph.V[vertexSelected].type = nodeType;

            TB_Name.Visible = nodeType == E_NodeType.Pavilion;

            UpdateGraphImage();
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        private void sheet_Resize(object sender, EventArgs e)
        {
            if (canvas != null)
            {
                canvas.SetSize(sheet.Width, sheet.Height);
            }
        }
        private void SendServer_Click(object sender, EventArgs e)
        {
            byte[] file = ImageToByteArray(Image.FromFile(PATHMAP));

            WebClient http = new WebClient();
            http.UploadFile("http://nigger.by:7770/api/v1/map/map", @PATHMAP);

            //save json

            http.UploadFile("http://nigger.by:7770/api/v1/beacons", @PATHGRAPH);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Json Files (json)|*.json";
            openFileDialog1.Title = "Открыть файл здания";
            openFileDialog1.FileName = "build.json";
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                var pathInput = openFileDialog1.FileName;

                canvas.outerWall = MapSerializer.DeSerializeBuild(pathInput);

            }
            openFileDialog1.Title = "Открыть файл павильонов";
            openFileDialog1.FileName = "pavilions.json";
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                var pathInput = openFileDialog1.FileName;

                canvas.Pavilions = MapSerializer.DeSerializePavs(pathInput);

            }
           // graph = MapSerializer.DeSerializeMap(PATHGRAPH);
        }

        private void TB_Mac_TextChanged(object sender, EventArgs e)
        {
            beaconSelected.mac = TB_Mac.Text;
        }

        private void NUD_txPower_ValueChanged(object sender, EventArgs e)
        {
            beaconSelected.tx_power = (int)NUD_txPower.Value;
        }

        private void TB_Mac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ResetAllSelections();
            }
        }
    }
}
