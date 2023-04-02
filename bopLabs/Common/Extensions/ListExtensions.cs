namespace bpoLabs.Common.Extensions
{
    public static class ListExtensions
    {
        public static List<List<T>> Partition<T>(this List<T> values, int chunkSize)
        {
            return values.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList<T>())
                .ToList();
        }
    }
}
