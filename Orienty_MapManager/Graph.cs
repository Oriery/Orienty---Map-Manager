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
        public string uuid { get; set; }
        public int x, y, z;

        public Coord coord { get => new Coord(x, y, z); }

        /// <summary>
        /// Ближайшая нода
        /// </summary>
        public int node { get; set; }

        public Beacon(string uuid, int x, int y, int z, int node)
        {
            this.uuid = uuid;
            this.x = x;
            this.y = y;
            this.z = z;
            this.node = node;
        }
    }

    public class Graph
    {
        public List<Vertex> V;
        public List<Edge> E;

        public Graph()
        {
            V = new List<Vertex>();
            E = new List<Edge>();
        }

        public int GetFreeIdOfVertex()
        {
            return V.Count;
        }

        public void Clear()
        {
            V.Clear();
            E.Clear();
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
    }
}