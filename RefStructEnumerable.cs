using System;
using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public static class RefStructEnumerable
    {
        public delegate ref TResult SelectByRefSelector<TSource, TResult>(ref TSource source);
        public delegate bool WhereByRefPredicate<T>(ref readonly T arg);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<TEnumerator, T>(this TEnumerator enumerator, T? _ = default)
            where TEnumerator : struct, IRefStructEnumerator<TEnumerator, T>, allows ref struct
        {
            return enumerator.MoveNext();
        }

        public static bool Any<TEnumerator, T>(this TEnumerator enumerator, Func<T, bool> predicate, T? _ = default)
            where TEnumerator : struct, IRefStructEnumerator<TEnumerator, T>, allows ref struct
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
        public static bool All<TEnumerator, T>(this TEnumerator enumerator, Func<T, bool> predicate, T? _ = default)
            where TEnumerator : struct, IRefStructEnumerator<TEnumerator, T>, allows ref struct
        {
            return !enumerator.Any<TEnumerator, T>(x => !predicate(x));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructSelectByRefEnumerator<TEnumerator, TSource, TResult>
            Select<TEnumerator, TSource, TResult>
        (
            this TEnumerator enumerator,
            SelectByRefSelector<TSource, TResult> selector
        )
            where TEnumerator : struct, IRefStructByRefEnumerator<TEnumerator, TSource>, allows ref struct
        {
            return new(enumerator, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructSelectEnumerator<TEnumerator, TSource, TResult>
            Select<TEnumerator, TSource, TResult>
        (
            this TEnumerator enumerator,
            Func<TSource, TResult> selector
        )
            where TEnumerator : struct, IRefStructEnumerator<TEnumerator, TSource>, allows ref struct
        {
            return new(enumerator, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructSkipEnumerator<TEnumerator, T> Skip<TEnumerator, T>
        (
            this TEnumerator enumerator,
            int count,
            T? _ = default
        )
            where TEnumerator : struct, IRefStructEnumerator<TEnumerator, T>, allows ref struct
        {
            return new(enumerator, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructSkipByRefEnumerator<TEnumerator, T> SkipByRef<TEnumerator, T>
        (
            this TEnumerator enumerator,
            int count,
            T? _ = default
        )
            where TEnumerator : struct, IRefStructByRefEnumerator<TEnumerator, T>, allows ref struct
        {
            return new(enumerator, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructTakeEnumerator<TEnumerator, T> Take<TEnumerator, T>
        (
            this TEnumerator enumerator,
            int count,
            T? _ = default
        )
            where TEnumerator : struct, IRefStructEnumerator<TEnumerator, T>, allows ref struct
        {
            return new(enumerator, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructTakeByRefEnumerator<TEnumerator, T> TakeByRef<TEnumerator, T>
        (
            this TEnumerator enumerator,
            int count,
            T? _ = default
        )
            where TEnumerator : struct, IRefStructByRefEnumerator<TEnumerator, T>, allows ref struct
        {
            return new(enumerator, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructWhereByRefEnumerator<TEnumerator, T> Where<TEnumerator, T>
        (
            this TEnumerator enumerator,
            WhereByRefPredicate<T> predicate
        )
            where TEnumerator : struct, IRefStructByRefEnumerator<TEnumerator, T>, allows ref struct
        {
            return new(enumerator, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefStructWhereEnumerator<TEnumerator, T> Where<TEnumerator, T>
        (
            this TEnumerator enumerator,
            Func<T, bool> predicate
        )
            where TEnumerator : struct, IRefStructEnumerator<TEnumerator, T>, allows ref struct
        {
            return new(enumerator, predicate);
        }
    }
}
