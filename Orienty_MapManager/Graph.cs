using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Orienty_MapManager
{
    public enum E_NodeType
    {
        Junktion,
        Pavilion,
        Exit
    }

    public class Vertex
    {
        private int _id;
        public int id { get => _id; }
        public int x, y, z;
        public Coord coord { get => new Coord(x, y, z); }

        public string name;
        public E_NodeType type { get; set; }
        public List<int> arrIDs { get; set; }

        public Vertex(int x, int y, int z, E_NodeType nodeType = E_NodeType.Junktion)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            this.type = nodeType;

            arrIDs = new List<int>();
            name = "";

            _id = Program.form.graph.GetFreeIdOfVertex();
        }

        public void DecID()
        {
            _id--;
        }

        public Point GetPoint()
        {
            return new Point(x, y);
        }
    }

    public class Edge
    {
        public int v1, v2;

        public Edge(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
    public class Beacon
    {
        public string mac { get; set; }
        public int x, y, z;
        public int tx_power { get; set; }

        public Coord coord { get => new Coord(x, y, z); }

        /// <summary>
        /// Ближайшая нода
        /// </summary>
        public int node { get => Program.form.graph.GetNearestVertex(x, y, z); }

        public Beacon(int x, int y, int z)
        {
            this.mac = "00:00:00:00:00:00";
            this.x = x;
            this.y = y;
            this.z = z;
            tx_power = -69;
        }

        public Point GetPoint()
        {
            return new Point(x, y);
        }
    }

    public class Graph
    {
        public List<Vertex> V;
        public List<Edge> E;
        public List<Beacon> beacons;

        public Graph()
        {
            V = new List<Vertex>();
            E = new List<Edge>();
            beacons = new List<Beacon>();
        }

        public int GetFreeIdOfVertex()
        {
            return V.Count;
        }

        public void Clear()
        {
            V.Clear();
            E.Clear();
            beacons.Clear();
        }

        public bool DeleteVertex(int Id)
        {
            if (Id < 0 || Id > V.Count - 1)
            {
                return false;
            }

            // удаляем рёбра
            for (int i = 0; i < E.Count; i++)
            {
                if ((E[i].v1 == Id) || (E[i].v2 == Id))
                {
                    DeleteEdge(E[i]);
                    i--;
                }
                else // сдвигаем id вершин у рёбер
                {
                    if (E[i].v1 > Id) E[i].v1--;
                    if (E[i].v2 > Id) E[i].v2--;
                }
            }

            // сдвигаем id вершин
            for (int i = Id + 1; i < V.Count; i++)
            {
                V[i].DecID();
            }

            // сдвигаем id вершин у вершин
            foreach (Vertex v in V)
            {
                for (int i = 0; i < v.arrIDs.Count; i++)
                {
                    if (v.arrIDs[i] > Id)
                    {
                        v.arrIDs[i]--;
                    }
                }
            }

            V.RemoveAt(Id);

            return true;
        }

        public int InsertVertexIntoEdge(int x, int y, int z, Edge edge)
        {
            int v1 = edge.v1;
            int v2 = edge.v2;

            DeleteEdge(edge);
            int idVertex = AddVertex(x, y, z);
            AddEdge(v1, idVertex);
            AddEdge(v2, idVertex);

            return idVertex;
        }

        public bool DeleteEdge(Edge edge)
        {
            if (edge == null)
            {
                return false;
            }

            V[edge.v1].arrIDs.Remove(edge.v2);
            V[edge.v2].arrIDs.Remove(edge.v1);

            E.Remove(edge);
            return true;
        }

        public bool DeleteBeacon(Beacon beacon)
        {
            if (beacon == null)
            {
                return false;
            }

            beacons.Remove(beacon);
            return true;
        }

        public bool AddEdge(int v1, int v2)
        {
            if (!V[v1].arrIDs.Contains(v2))
            {
                E.Add(new Edge(v1, v2));
                V[v1].arrIDs.Add(v2);
                V[v2].arrIDs.Add(v1);
                return true;
            }

            return false;
        }

        public int AddVertex(int x, int y, int z)
        {
            Vertex vertex = new Vertex(x, y, z);
            V.Add(vertex);
            return vertex.id;
        }

        public int GetNearestVertex(int x, int y, int z)
        {
            if (V.Count == 0)
            {
                return -1;
            }

            Point point = new Point(x, y);
            float minDist = int.MaxValue;
            int id = -1;

            foreach (Vertex v in V)
            {
                if (v.z == z)
                {
                    float dist = GetDistanceSquaredBetweenPoints(v.GetPoint(), point);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        id = v.id;
                    }
                }
            }

            return id;

            float GetDistanceSquaredBetweenPoints(Point p1, Point p2)
            {
                return (float)(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
            }
        }
    }
}