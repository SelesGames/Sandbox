using System.Collections.ObjectModel;

namespace System.Collections.Generic
{
    public static class IEnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return new ObservableCollection<T>(source);
        }
    }
}
