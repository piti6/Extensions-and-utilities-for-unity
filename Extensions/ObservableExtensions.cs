#if UNIRX
using System;
using System.Collections.Generic;
using UniRx;

public static class ObservableExtensions
{
    public static IObservable<(T Value, int Index)> WithIndex<T>(this IObservable<T> observable)
    {
        return observable.Select((x, i) => (x, i));
    }

    public static IObservable<T> ConnectAndSubscribe<T>(this IConnectableObservable<T> observable)
    {
        return Observable.Create<T>(observer =>
        {
            var disposable1 = observable.Subscribe(observer);
            var disposable2 = observable.Connect();

            return StableCompositeDisposable.Create(disposable1, disposable2);
        });
    }

    public static IObservable<T> ConnectAndSubscribe<T>(this IConnectableObservable<T> observable, ICollection<IDisposable> disposables)
    {
        return Observable.Create<T>(observer =>
        {
            var disposable = observable.Subscribe(observer);
            var connectedDisposable = observable.Connect();

            connectedDisposable.AddTo(disposables);

            return disposable;
        });
    }

    public static IObservable<TResult> Flatten<TSource, TResult>(this IObservable<IEnumerable<TSource>> source,
        Func<TSource, IObservable<TResult>> resultSelector)
    {
        return source.SelectMany(x => x).SelectMany(resultSelector);
    }
}
#endif