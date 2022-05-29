namespace DependencyEvaluation.Model
{
    public class Vertex
    {
        public string Raw { get; set; }
        public int Index { get; set; } // line number
        public int[] Dependents { get; set; }
        public Operator Operator { get; set; }
        public double? Value;
        public override string ToString()
        {
            return Raw;
        }
    }
}
