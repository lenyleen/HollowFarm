using System;
using Service;

namespace DefaultNamespace.Views.UIVIiews.Service
{
    public interface ISoilActionButton
    {
        public IObservable<SoilActionType> OnClickAsObservable { get; }

		public void Initialize(SoilActionType actionType);
    }
}