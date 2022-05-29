using System.Collections.Generic;
using System.Linq;

namespace DependencyEvaluation.Model
{
    public class GraphVisitor : IGraphVisitor
    {
        private readonly Dictionary<int, Vertex> _vertices;
        private readonly IMemory _cache;

        public GraphVisitor(Dictionary<int, Vertex> vertices, IMemory cache)
        {
            _vertices = vertices;
            _cache = cache;
        }

        public double Visit()
        {
            var firstInstruction = _vertices.First();
            return Visit(firstInstruction.Key);
        }

        private double Visit(int index)
        {
            if (_cache.TryGetByIndex(index, out var result))
            {
                return result;
            }

            var vertex = _vertices[index];
            
            switch (vertex.Operator)
            {
                case Operator.Value:
                {
                    var val = vertex.Value.GetValueOrDefault();
                    _cache.Insert(index, val);
                    return val;
                }
                case Operator.Add:
                {
                    var val = vertex.Dependents.Select(Visit).Sum();
                    _cache.Insert(index, val);
                    return val;
                }
                case Operator.Mult:
                {
                    var val = vertex.Dependents.Select(Visit).Aggregate((x, y) => x * y);
                    _cache.Insert(index, val);
                    return val;
                }
            }

            return -1;
        }
    }
}
