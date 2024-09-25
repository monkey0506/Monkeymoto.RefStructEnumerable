using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructSkipByRefEnumerator<TEnumerator, T>
    (
        TEnumerator enumerator,
        int count
    ) :
        IRefStructByRefEnumerator<RefStructSkipByRefEnumerator<TEnumerator, T>, T>
        where TEnumerator : struct, IRefStructByRefEnumerator<TEnumerator, T>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private int count = count;

        public ref T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref enumerator.Current;
        }

        T IRefStructEnumerator<RefStructSkipByRefEnumerator<TEnumerator, T>, T>.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructSkipByRefEnumerator<TEnumerator, T> GetEnumerator() => this;

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
