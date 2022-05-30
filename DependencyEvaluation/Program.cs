using System;
using System.IO;
using DependencyEvaluation.Model;

namespace DependencyEvaluation
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"input.txt");
            var converter = new TextToVertexConverter();
            var vertexDict = converter.ConvertToVertex(lines);
            var memory = new Memory();
            var graphVisitor = new GraphVisitor(vertexDict, memory);
            var result = graphVisitor.Visit();
            //result = 275225993853;
        }
    }
}
