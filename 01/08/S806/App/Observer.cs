using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class Observer<T> : IObserver<T>
    {
        private Action<T> _onNext;
        public Observer(Action<T> onNext) => _onNext = onNext;

        public void OnCompleted() { }
        public void OnError(Exception error) { }
        public void OnNext(T value) => _onNext(value);
    }
}
