using System;
using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public static class RefStructEnumerable
    {
        public delegate ref TResult SelectByRefSelector<TSource, TResult>(ref TSource source);
        public delegate bool WhereByRefPredicate<T>(ref readonly T arg);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<T, TEnumerator>(this TEnumerator enumerator, T? _ = default)
            where TEnumerator : struct, IRefStructEnumerator<T, TEnumerator>, allows ref struct
        {
            return enumerator.MoveNext();
        }

        public static bool Any<T, TEnumerator>(this TEnumerator enumerator, Func<T, bool> predicate, T? _ = default)
            where TEnumerator : struct, IRefStructEnumerator<T, TEnumerator>, allows ref struct
        {
            for (bool moveNext = enumerator.MoveNext(); moveNext; moveNext = enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    return true;
                }
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All<T, TEnumerator>(this TEnumerator enumerator, Func<T, bool> predicate, T? _ = default)
            where TEnumerator : struct, IRefStructEnumerator<T, TEnumerator>, allows ref struct
        {
            return !enumerator.Any<T, TEnumerator>(x => !predicate(x));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructSelectByRefEnumerator<TSource, TResult, TEnumerator>
            Select<TSource, TResult, TEnumerator>
        (
            this TEnumerator enumerator,
            SelectByRefSelector<TSource, TResult> selector
        )
            where TEnumerator : struct, IRefStructByRefEnumerator<TSource, TEnumerator>, allows ref struct
        {
            return new(enumerator, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructSelectEnumerator<TSource, TResult, TEnumerator>
            Select<TSource, TResult, TEnumerator>
        (
            this TEnumerator enumerator,
            Func<TSource, TResult> selector
        )
            where TEnumerator : struct, IRefStructEnumerator<TSource, TEnumerator>, allows ref struct
        {
            return new(enumerator, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructSkipEnumerator<T, TEnumerator> Skip<T, TEnumerator>
        (
            this TEnumerator enumerator,
            int count,
            T? _ = default
        )
            where TEnumerator : struct, IRefStructEnumerator<T, TEnumerator>, allows ref struct
        {
            return new(enumerator, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructSkipByRefEnumerator<T, TEnumerator> SkipByRef<T, TEnumerator>
        (
            this TEnumerator enumerator,
            int count,
            T? _ = default
        )
            where TEnumerator : struct, IRefStructByRefEnumerator<T, TEnumerator>, allows ref struct
        {
            return new(enumerator, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructTakeEnumerator<T, TEnumerator> Take<T, TEnumerator>
        (
            this TEnumerator enumerator,
            int count,
            T? _ = default
        )
            where TEnumerator : struct, IRefStructEnumerator<T, TEnumerator>, allows ref struct
        {
            return new(enumerator, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructTakeByRefEnumerator<T, TEnumerator> TakeByRef<T, TEnumerator>
        (
            this TEnumerator enumerator,
            int count,
            T? _ = default
        )
            where TEnumerator : struct, IRefStructByRefEnumerator<T, TEnumerator>, allows ref struct
        {
            return new(enumerator, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructWhereByRefEnumerator<T, TEnumerator> Where<T, TEnumerator>
        (
            this TEnumerator enumerator,
            WhereByRefPredicate<T> predicate
        )
            where TEnumerator : struct, IRefStructByRefEnumerator<T, TEnumerator>, allows ref struct
        {
            return new(enumerator, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructWhereEnumerator<T, TEnumerator> Where<T, TEnumerator>
        (
            this TEnumerator enumerator,
            Func<T, bool> predicate
        )
            where TEnumerator : struct, IRefStructEnumerator<T, TEnumerator>, allows ref struct
        {
            return new(enumerator, predicate);
        }
    }
}
