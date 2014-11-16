using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moonshine.Aggregator.Clustering
{
    public class Cluster
    {
        public double[] Vector { get; set; }
        public List<double[]> SetOfVectors { get; set; }
        public List<Cluster> Childs { get; set; }
        public double MergeDist { get; set; }
        public int Id { get; set; }

        public Cluster(double[] vector, int id)
        {
            Vector = vector;
            MergeDist = 0;
            Childs = new List<Cluster>();
            SetOfVectors = new List<double[]>();
            SetOfVectors.Add(Vector);
            Id = id;
        }

        public Cluster(Cluster c1, Cluster c2, double mergeDist)
        {
            Id = new Random().Next(-9999999, -1); // temporaire :)
            Childs = new List<Cluster>();
            Childs.Add(c1);
            Childs.Add(c2);
            MergeDist = mergeDist;
            Vector = null;
            SetOfVectors = new List<double[]>();
            SetOfVectors = SetOfVectors.Concat(c1.SetOfVectors).ToList();
            SetOfVectors = SetOfVectors.Concat(c2.SetOfVectors).ToList();
        }

        public double DistanceTo(Cluster cluter)
        {
            double distMin = 9999;
            foreach (var vector in this.SetOfVectors)
            {
                foreach (var _vector in cluter.SetOfVectors)
                {
                    double dist = Helpers.EuclideanDistance(vector, _vector);
                    if (dist < distMin)
                    {
                        distMin = dist;
                    }
                }
            }
            return distMin;
        }
    }
}
