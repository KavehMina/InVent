
namespace InVent
{
    public class State<T> where T : class, new()
    {
        public T Value { get; private set; }
        public delegate void StateChanged();
        public event StateChanged OnChanged;
        public IObservable<T> Observable;
        public State() : this(new T())
        {
        }
        public State(T state)
        {
            Value = state;
            Observable = System.Reactive.Linq.Observable.Create<T>(o =>
            {
                StateChanged handler = () => o.OnNext(Value);
                OnChanged += handler;
                return () =>
                {
                    OnChanged -= handler;
                };
            });
        }
        public void SetState(T state)
        {
            Value = state;
            OnChanged?.Invoke();
        }
        public void SetState(Func<T, T> func)
        {
            SetState(func(Value));
        }
        public void SetState(Action<T> action)
        {
            action?.Invoke(Value);
            SetState(Value);
        }
    }
}
