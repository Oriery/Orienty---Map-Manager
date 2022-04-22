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
            mapContainer.vertices = graph.V;

            foreach ( var v in mapContainer.vertices )
            {
                if (v.type == E_NodeType.Pavilion)
                {
                    mapContainer.idNamePairs.Add(v.id, v.name);
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
                vertices = new List<Vertex>();
                idNamePairs = new Dictionary<int, string>();
                beacons = new List<Beacon>();
            }

            public List<Vertex> vertices { get; set; }
            public Dictionary<int, string> idNamePairs { get; set; }
            public List<Beacon> beacons { get; set; }

         
        }
    }
}
