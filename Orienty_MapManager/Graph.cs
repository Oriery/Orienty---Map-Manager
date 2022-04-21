using System;
using System.Collections.Generic;

namespace Orienty_MapManager
{
    public class Vertex
    {
        private int _id;
        public int id { get => _id; }

        public int x, y;

        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
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
        public Dictionary<int, string> idNamePairs;

        public Graph()
        {
            V = new List<Vertex>();
            E = new List<Edge>();
            idNamePairs = new Dictionary<int, string>();
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

        /// <summary>
        /// Возвращает имя вершины, если она есть в списке. Иначе возвращает само число id в виде строки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNameOfVertexById(int id)
        {
            return idNamePairs.TryGetValue(id, out string name) ? name : id.ToString();
        }

        public void Clear()
        {
            V.Clear();
            E.Clear();
        }
    }
}