using System;
using System.Collections.Generic;
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
        public int[] coord { get => new int[] { x, y, z }; }

        public string name;
        public E_NodeType type { get; set; }
        public List<int> arrIDs { get; set; }

        public Vertex(int x, int y, int z, E_NodeType nodeType = E_NodeType.Pavilion)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            this.type = nodeType;

            arrIDs = new List<int>();
            name = "";

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
    public class Beacon
    {
        public string uuid { get; set; }
        public int x, y, z;

        public int[] coord { get => new int[] { x, y, z }; }

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