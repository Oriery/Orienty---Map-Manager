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

        public Form1()
        {
            InitializeComponent();
            canvas = new GraphCanvas(sheet.Width, sheet.Height);
            graph = new Graph();
            sheet.Image = canvas.GetBitmap();
        }

        //кнопка - выбрать вершину
        private void selectButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = false;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            canvas.clearSheet();
            canvas.drawALLGraph(graph);
            sheet.Image = canvas.GetBitmap();
            selected1 = -1;
        }

        //кнопка - рисовать вершину
        private void drawVertexButton_Click(object sender, EventArgs e)
        {
            drawVertexButton.Enabled = false;
            selectButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            canvas.clearSheet();
            canvas.drawALLGraph(graph);
            sheet.Image = canvas.GetBitmap();
        }

        //кнопка - рисовать ребро
        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            deleteButton.Enabled = true;
            canvas.clearSheet();
            canvas.drawALLGraph(graph);
            sheet.Image = canvas.GetBitmap();
            selected1 = -1;
            selected2 = -1;
        }

        //кнопка - удалить элемент
        private void deleteButton_Click(object sender, EventArgs e)
        {
            deleteButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            canvas.clearSheet();
            canvas.drawALLGraph(graph);
            sheet.Image = canvas.GetBitmap();
        }

        //кнопка - удалить граф
        private void deleteALLButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            const string message = "Вы действительно хотите полностью удалить граф?";
            const string caption = "Удаление";
            var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MBSave == DialogResult.Yes)
            {
                graph.Clear();
                canvas.clearSheet();
                sheet.Image = canvas.GetBitmap();
            }
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

        private void sheet_MouseClick(object sender, MouseEventArgs e)
        {
            //нажата кнопка "выбрать вершину", ищем степень вершины
            if (selectButton.Enabled == false)
            {
                for (int i = 0; i < graph.V.Count; i++)
                {
                    if (Math.Pow((graph.V[i].x - e.X), 2) + Math.Pow((graph.V[i].y - e.Y), 2) <= canvas.rOfVertex * canvas.rOfVertex)
                    {
                        if (selected1 != -1)
                        {
                            selected1 = -1;
                            canvas.clearSheet();
                            canvas.drawALLGraph(graph);
                            sheet.Image = canvas.GetBitmap();
                        }
                        if (selected1 == -1)
                        {
                            canvas.drawVertex(graph.V[i], true);
                            selected1 = i;
                            sheet.Image = canvas.GetBitmap();
                            listBoxMatrix.Items.Clear();
                            int degree = 0;
                            // Здесь был подсчёт степени вершины
                            listBoxMatrix.Items.Add("Степень вершины №" + (selected1 + 1) + " равна " + degree);
                            break;
                        }
                    }
                }
            }
            //нажата кнопка "рисовать вершину"
            if (drawVertexButton.Enabled == false)
            {
                Vertex vertex = new Vertex(e.X, e.Y);
                vertex.Name = "";
                graph.V.Add(vertex);
                canvas.drawVertex(vertex);
                sheet.Image = canvas.GetBitmap();
            }
            //нажата кнопка "рисовать ребро"
            if (drawEdgeButton.Enabled == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < graph.V.Count; i++)
                    {
                        if (Math.Pow((graph.V[i].x - e.X), 2) + Math.Pow((graph.V[i].y - e.Y), 2) <= canvas.rOfVertex * canvas.rOfVertex)
                        {
                            if (selected1 == -1)
                            {
                                canvas.drawVertex(graph.V[i], true);
                                selected1 = i;
                                sheet.Image = canvas.GetBitmap();
                                break;
                            }
                            if (selected2 == -1)
                            {
                                canvas.drawVertex(graph.V[i], true);
                                selected2 = i;
                                graph.E.Add(new Edge(selected1, selected2));
                                canvas.drawEdge(graph.V[selected1], graph.V[selected2], graph.E[graph.E.Count - 1]);
                                selected1 = -1;
                                selected2 = -1;
                                sheet.Image = canvas.GetBitmap();
                                break;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if ((selected1 != -1) &&
                        (Math.Pow((graph.V[selected1].x - e.X), 2) + Math.Pow((graph.V[selected1].y - e.Y), 2) <= canvas.rOfVertex * canvas.rOfVertex))
                    {
                        canvas.drawVertex(graph.V[selected1]);
                        selected1 = -1;
                        sheet.Image = canvas.GetBitmap();
                    }
                }
            }
            //нажата кнопка "удалить элемент"
            if (deleteButton.Enabled == false)
            {
                bool haveDeleted = false; //удалили ли что-нибудь по ЭТОМУ клику

                //ищем, возможно была нажата вершина
                for (int i = 0; i < graph.V.Count; i++)
                {
                    if (Math.Pow((graph.V[i].x - e.X), 2) + Math.Pow((graph.V[i].y - e.Y), 2) <= canvas.rOfVertex * canvas.rOfVertex)
                    {
                        for (int j = 0; j < graph.E.Count; j++)
                        {
                            if ((graph.E[j].v1 == i) || (graph.E[j].v2 == i))
                            {
                                graph.E.RemoveAt(j);
                                j--;
                            }
                            else
                            {
                                if (graph.E[j].v1 > i) graph.E[j].v1--;
                                if (graph.E[j].v2 > i) graph.E[j].v2--;
                            }
                        }
                        graph.V.RemoveAt(i);
                        haveDeleted = true;
                        break;
                    }
                }
                //ищем, возможно было нажато ребро
                if (!haveDeleted)
                {
                    for (int i = 0; i < graph.E.Count; i++)
                    {
                        if (graph.E[i].v1 == graph.E[i].v2) //если это петля
                        {
                            if ((Math.Pow((graph.V[graph.E[i].v1].x - canvas.rOfVertex - e.X), 2) + Math.Pow((graph.V[graph.E[i].v1].y - canvas.rOfVertex - e.Y), 2) <= ((canvas.rOfVertex + 2) * (canvas.rOfVertex + 2))) &&
                                (Math.Pow((graph.V[graph.E[i].v1].x - canvas.rOfVertex - e.X), 2) + Math.Pow((graph.V[graph.E[i].v1].y - canvas.rOfVertex - e.Y), 2) >= ((canvas.rOfVertex - 2) * (canvas.rOfVertex - 2))))
                            {
                                graph.E.RemoveAt(i);
                                haveDeleted = true;
                                break;
                            }
                        }
                        else //не петля
                        {
                            if (((e.X - graph.V[graph.E[i].v1].x) * (graph.V[graph.E[i].v2].y - graph.V[graph.E[i].v1].y) / (graph.V[graph.E[i].v2].x - graph.V[graph.E[i].v1].x) + graph.V[graph.E[i].v1].y) <= (e.Y + 4) &&
                                ((e.X - graph.V[graph.E[i].v1].x) * (graph.V[graph.E[i].v2].y - graph.V[graph.E[i].v1].y) / (graph.V[graph.E[i].v2].x - graph.V[graph.E[i].v1].x) + graph.V[graph.E[i].v1].y) >= (e.Y - 4))
                            {
                                if ((graph.V[graph.E[i].v1].x <= graph.V[graph.E[i].v2].x && graph.V[graph.E[i].v1].x <= e.X && e.X <= graph.V[graph.E[i].v2].x) ||
                                    (graph.V[graph.E[i].v1].x >= graph.V[graph.E[i].v2].x && graph.V[graph.E[i].v1].x >= e.X && e.X >= graph.V[graph.E[i].v2].x))
                                {
                                    graph.E.RemoveAt(i);
                                    haveDeleted = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                //если что-то было удалено, то обновляем граф на экране
                if (haveDeleted)
                {
                    canvas.clearSheet();
                    canvas.drawALLGraph(graph);
                    sheet.Image = canvas.GetBitmap();
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (sheet.Image != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        sheet.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
