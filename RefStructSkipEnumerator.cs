using System;
using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructSkipEnumerator<T, TEnumerator>
    (
        TEnumerator enumerator,
        int count
    ) :
        IRefStructEnumerator<T, RefStructSkipEnumerator<T, TEnumerator>>
        where TEnumerator : struct, IRefStructEnumerator<T, TEnumerator>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private int count = Math.Max(count, 0);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => enumerator.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructSkipEnumerator<T, TEnumerator> GetEnumerator() => this;

        public bool MoveNext()
        {
            while (--count >= 0)
            {
                if (!enumerator.MoveNext())
                {
                    return false;
                }
            }
            return enumerator.MoveNext();
        }
    }
}
