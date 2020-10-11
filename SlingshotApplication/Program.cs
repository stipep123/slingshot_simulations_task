using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using SlingshotApplication.Models;
using SlingshotApplication.Context;

namespace SlingshotApplication
{
    class Program
    {

        static void Main(string[] args)
        {
            var context = new SlingshotContext();
            string json;
            using (StreamReader r = new StreamReader("../../../BasicJSON.json"))
            {
                json = r.ReadToEnd();
            }

            var obj = JObject.Parse(json);

            var nodes = GetAllNodes(obj);
            var edges = GetAllEdges(obj, nodes);

            if (nodes.Count > 0 && edges.Count > 0)
                Console.WriteLine("Nodes and edges red from JSON file successefully!");
            else
                Console.WriteLine("Problem accured while reading JSON file. Nodes or edges empty!");

            InsertDataIntoDatabase(context, nodes, edges);

            Console.WriteLine("Please enter the ID of PointA:");
            var idA = Console.ReadLine();
            var nodeA = context.Nodes.Where(n => n.Id == idA).FirstOrDefault();

            while (nodeA == null)
            {
                Console.WriteLine("Please enter the correct ID of PointA:");
                idA = Console.ReadLine();
                nodeA = context.Nodes.Where(n => n.Id == idA).FirstOrDefault();
            }
            Console.WriteLine("Please enter the ID of PointB:");
            var idB = Console.ReadLine();

            var nodeB = context.Nodes.Where(n => n.Id == idB).FirstOrDefault();

            while (nodeB == null)
            {
                Console.WriteLine("Please enter the correct ID of PointB:");
                idB = Console.ReadLine();
                nodeB = context.Nodes.Where(n => n.Id == idB).FirstOrDefault();
            }

            var path = GetShortestPathDijkstra(nodeA, nodeB, context);
            PrintPath(path);

            Console.WriteLine("Press enter for finish!");
            Console.ReadLine();

        }

        private static IList<Node> GetAllNodes(JObject obj)
        {
            var nodes = new List<Node>();

            foreach (var child in obj["Nodes"].Children())
            {
                var id = child.First()["Id"].ToString();

                var positionx = child.First()["Position"]["X"].ToString();
                var positiony = child.First()["Position"]["Y"].ToString();
                nodes.Add(new Node
                {
                    Id = id,
                    PosX = float.Parse(positionx),
                    PosY = float.Parse(positiony)
                });
                
            }

            return nodes;
        }

        private static IList<Edge> GetAllEdges(JObject obj, IList<Node> nodes)
        {
            var edges = new List<Edge>();
            foreach (var child in obj["Edges"].Children())
            {
                var id = child.First()["Id"].ToString();
                var source = child.First()["Source"].ToString();

                var target = child.First()["Target"].ToString();

                edges.Add(new Edge
                {
                    Id = id,
                    Target = nodes.Where(x => x.Id == target).FirstOrDefault(),
                    Source = nodes.Where(x => x.Id == source).FirstOrDefault()

                });
            }

            return edges;
        }

        private static void PrintPath(IList<Node> path)
        {
            if(path.Count == 0)
            {
                Console.WriteLine("No connection between those two nodes!");
            }

            foreach(var node in path)
            {
                Console.WriteLine("******************************");
                Console.WriteLine("Node name:" + node.Id);
                if (node.NearestToStart != null)
                    Console.WriteLine("Node connection:" + node.NearestToStart.Id);
                else
                {
                    Console.WriteLine("Starting node");
                }
                Console.WriteLine("Distance: " + node.Distance);
                Console.WriteLine("******************************");
            }
        }

        private static void InsertDataIntoDatabase(SlingshotContext context, IList<Node> nodes, IList<Edge> edges)
        {
            foreach(var node in nodes)
            {
                if(context.Nodes.Where(n => n.Id == node.Id).FirstOrDefault() == null)
                    context.Nodes.Add(node);
            }
            foreach(var edge in edges)
            {
                if (context.Edges.Where(e => e.Id == edge.Id).FirstOrDefault() == null)
                {
                    edge.Cost = edge.GetCost();
                    context.Edges.Add(edge);
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<Node> GetShortestPathDijkstra(Node Start, Node End, SlingshotContext context)
        {
            GoFindWay(context, Start, End);
            var shortestPath = new List<Node>();
            shortestPath.Add(End);
            BuildShortestPath(shortestPath, End);
            shortestPath.Reverse();
            return shortestPath;
        }

        private static void BuildShortestPath(List<Node> list, Node node)
        {
            if (node.NearestToStart == null)
                return;
            list.Add(node.NearestToStart);
            BuildShortestPath(list, node.NearestToStart);
        }

        private static void GoFindWay(SlingshotContext context, Node Start, Node End)
        {
            var queue = new List<Node>();
            queue.Add(Start);
            Start.Distance = 0;
            do
            {
                queue.OrderBy(x => x.Distance).ToList();
                var node = queue.First();
                queue.Remove(node);
                foreach (var connectedNode in node.GetConnections(context))
                {
                    var childNode = connectedNode.Target;
                    if (childNode.Visited)
                        continue;
                    if (childNode.Distance == null || node.Distance + connectedNode.Cost < childNode.Distance)
                    {
                        childNode.Distance = node.Distance + connectedNode.Cost;
                        childNode.NearestToStart = node;
                    }
                    if(!queue.Contains(childNode))
                    {
                        queue.Add(childNode);
                    }
                }
                node.Visited = true;
                if(node == End)
                {
                    return;
                }
            } while (queue.Any());
        }
    }
}
