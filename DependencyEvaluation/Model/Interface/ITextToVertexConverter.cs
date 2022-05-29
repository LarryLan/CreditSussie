using System.Collections.Generic;

namespace DependencyEvaluation.Model
{
    public interface ITextToVertexConverter
    {
        Vertex ConvertToVertex(string instruction);
        Dictionary<int, Vertex> ConvertToVertex(string[] instructions);
    }
}
