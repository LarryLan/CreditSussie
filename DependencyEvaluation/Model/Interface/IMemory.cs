namespace DependencyEvaluation.Model
{
    public interface IMemory
    {
        bool TryGetByIndex(int index, out double value);
        void Insert(int index, double value);
    }
}
