using System;
using System.Collections.Generic;
using System.Data;

namespace DependencyEvaluation.Model
{
    public class Memory : IMemory
    {
        private readonly Dictionary<int, double> _dict;

        public Memory()
        {
            _dict = new Dictionary<int, double>();
        }

        public bool TryGetByIndex(int index, out double value)
        {
            if (_dict.ContainsKey(index))
            {
                value = _dict[index];
                return true;
            }

            value = double.MinValue;
            return false;
        }

        public void Insert(int index, double value)
        {
            if (_dict.ContainsKey(index))
            {
                throw new DuplicateNameException($"Index: {index} with Value: {value} has been inserted? why insert again?");
            }

            _dict[index] = value;
        }
    }
}
