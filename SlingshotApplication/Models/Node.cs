using Newtonsoft.Json;
using SlingshotApplication.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SlingshotApplication.Models
{
    public class Node
    {
        public string Id { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public double? Distance { get; set; }
        public bool Visited { get; set; }
        public Node NearestToStart { get; set; }
        public IList<Edge> GetConnections(SlingshotContext context) => context.Edges.Where(s => s.Source == this).OrderBy(s => s.Cost).ToList();
    }
}
