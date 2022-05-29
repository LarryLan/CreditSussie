using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DependencyEvaluation.Model
{
    public class TextToVertexConverter : ITextToVertexConverter
    {
        public Vertex ConvertToVertex(string instruction)
        {
            var indexCmd = instruction.Split(':');
            var index = Convert.ToInt32(indexCmd[0]);
            var cmd = indexCmd[1].Trim();
            var cmdParts = cmd.Split(' ');
            var dependents = cmdParts.Skip(1).Select(x => Convert.ToInt32(x)).ToArray();
            var vertex = new Vertex
            {
                Index = index,
                Operator = GetOperator(cmdParts[0]),
                Value = GetOperator(cmdParts[0]) == Operator.Value ? Convert.ToDouble(cmdParts[1]) : (double?) null,
                Dependents = GetOperator(cmdParts[0]) == Operator.Value ? new int[] { } : dependents,
                Raw = instruction
            };
            return vertex;
        }

        public Dictionary<int, Vertex> ConvertToVertex(string[] instructions)
        {
            return instructions.Select(ConvertToVertex).ToDictionary(k => k.Index, v => v);
        }

        private Operator GetOperator(string input)
        {
            switch (input)
            {
                case "Value":
                    return Operator.Value;
                case "Add":
                    return Operator.Add;
                case "Mult":
                    return Operator.Mult;
            }

            throw new InvalidEnumArgumentException($"Can not find operator for {input}");
        }
    }
}
