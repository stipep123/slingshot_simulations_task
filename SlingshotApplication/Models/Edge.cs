using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlingshotApplication.Models
{
    public class Edge
    {
        public string Id { get; set; }
        public Node Target { get; set; }
        public Node Source { get; set; }
        public double Cost { get; set; }

        public double GetCost()
        {
            return Math.Sqrt(((Math.Pow((this.Source.PosX - this.Source.PosY), 2) + Math.Pow((this.Target.PosX - this.Target.PosY), 2))));
        }
    }
}
