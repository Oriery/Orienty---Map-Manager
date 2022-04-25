using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Orienty_MapManager
{ 
    static class MapSerializer
    {
        public static string SerializeMap(Graph graph)
        {
            MapContainer mapContainer = new MapContainer();
            mapContainer.nodes = graph.V;

            foreach ( var v in mapContainer.nodes )
            {
                if (v.type == E_NodeType.Pavilion)
                {
                    mapContainer.idNamePairs.Add(new IdNamePair(v.id, v.name));
                }

                // TODO сейчас сериализуются ненастоящие маячки
                mapContainer.beacons.Add(new Beacon("4hgjrb-264rtb-524bdg-245gbt-tr565tb", v.x + 5, v.y - 2, 0, v.id)); 
            }

            return JsonSerializer.Serialize<MapContainer>(mapContainer);
        }

        private class MapContainer
        {
            public MapContainer()
            {
                nodes = new List<Vertex>();
                idNamePairs = new List<IdNamePair>();
                beacons = new List<Beacon>();
            }

            public List<Vertex> nodes { get; set; }
            public List<IdNamePair> idNamePairs { get; set; }
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
