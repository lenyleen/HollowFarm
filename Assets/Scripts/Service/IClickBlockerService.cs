using UniRx;

namespace Service
{
    public interface IClickBlockerService
    {
        public ReactiveProperty<bool> IsUiClosingClicksEnabled { get; }
    }
}