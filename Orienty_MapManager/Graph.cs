using System;
using System.Collections.Generic;

namespace Orienty_MapManager
{
    public enum E_NodeType
    {
        Junktion,
        Exit,
        Pavilion
    }

    public class Vertex
    {
        private int _id;
        public int id { get => _id; }

        public int x, y;
        public string Name;

        E_NodeType nodeType;

        public Vertex(int x, int y, E_NodeType nodeType = E_NodeType.Junktion)
        {
            this.x = x;
            this.y = y;
            this.nodeType = nodeType;
            Name = "";

            _id = Program.form.graph.GetFreeIdOfVertex();
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
            int maxId = -1;
            foreach (var v in V)
            {
                maxId = Math.Max(maxId, v.id);
            }

            return maxId + 1;
        }

        public Vertex GetVertexById(int id)
        {
            foreach (var v in V)
            {
                if (v.id == id)
                {
                    return v;
                }
            }

            return null;
        }

        public void Clear()
        {
            V.Clear();
            E.Clear();
        }
    }
}