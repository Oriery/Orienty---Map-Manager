﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Windows.Forms;

namespace Orienty_MapManager
{ 
    static class MapSerializer
    {
        static JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        public static void SerializeBuild(string path, Polygon build)
        {
            string jsonBuild =  JsonSerializer.Serialize<Polygon>(build, options);

            using (var stream = new StreamWriter(path))
            {
                stream.Write(jsonBuild);
            }

        }

        public static void SerializePavs(string path, List<Polygon> pavilions)
        {
            string jsonPavs = JsonSerializer.Serialize<List<Polygon>>(pavilions, options);

            using (var stream = new StreamWriter(path))
            {
                stream.Write(jsonPavs);
            }
        }

        public static Polygon DeserializeBuild(string path)
        {
            string jsonBuild;

            using (var stream = new StreamReader(path))
            {
                jsonBuild = stream.ReadToEnd();
            }

            try
            {
                return JsonSerializer.Deserialize<Polygon>(jsonBuild, options);
            }
            catch
            {
                MessageBox.Show("файлы поверждены, загрузка готовой схемы невозможна");
                return  null;
            }
            
        }

        public static List<Polygon> DeserializePavs(string path)
        {
            string jsonPavs;

            using (var stream = new StreamReader(path))
            {
                jsonPavs = stream.ReadToEnd();
            }
            try
            {
                return JsonSerializer.Deserialize<List<Polygon>>(jsonPavs, options);
            }
            catch
            {
                MessageBox.Show("файлы поверждены, загрузка готовой схемы невозможна");
                return null;
            }

        }

        public static Graph DeserializeMap(string path)
        {
            string jsonGraph;
            using (var stream = new StreamReader(path))
            {
                jsonGraph = stream.ReadToEnd();
            }

            MapContainer mapContainer;
            try
            {
                mapContainer = JsonSerializer.Deserialize<MapContainer>(jsonGraph, options);
            }
            catch (Exception ex)
            {
                MessageBox.Show("файлы поврерждены, загрузка готовой схемы невозможна\n" + ex.Message);
                return new Graph();
            }

            Graph graph = new Graph();
            graph.V = mapContainer.nodes;
            graph.beacons = mapContainer.beacons;

            foreach (var item in mapContainer.nodeInfos)
            {
                graph.V[item.id].name = item.name;
            }

            // Создаём рёбра
            foreach (Vertex v1 in graph.V)
            {
                foreach (int v2 in v1.arrIDs)
                {
                    if (v2 > v1.id)
                    {
                        graph.E.Add(new Edge(v1.id, v2));
                    }
                }
            }

            return graph;
        }

        public static string SerializeMap(Graph graph)
        {
            MapContainer mapContainer = new MapContainer();
            mapContainer.nodes = graph.V;
            mapContainer.beacons = graph.beacons;

            foreach ( var v in mapContainer.nodes )
            {
                if (v.type == E_NodeType.Pavilion)
                {
                    mapContainer.nodeInfos.Add(new IdNamePair(v.id, v.name));
                }
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(mapContainer, options);
        }

        private class MapContainer
        {
            public MapContainer()
            {
                nodes = new List<Vertex>();
                nodeInfos = new List<IdNamePair>();
                beacons = new List<Beacon>();
            }

            public MapContainer(List<Vertex> nodes, List<IdNamePair> nodeInfos, List<Beacon> beacons)
            {
                this.nodes = nodes;
                this.nodeInfos = nodeInfos;
                this.beacons = beacons;
            }

            public List<Vertex> nodes { get; set; }
            public List<IdNamePair> nodeInfos { get; set; }
            public List<Beacon> beacons { get; set; }
        }

        private class IdNamePair
        {
            public int id { get; set; }
            public string name { get; set; }

            public IdNamePair(int id, string name)
            {
                this.id = id;
                this.name = name;
            }
        }
    }
}
