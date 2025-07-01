using System;
using Service;
using UnityEngine;

namespace DefaultNamespace.Views.UIVIiews.Service
{
    public interface ISoilActionButton
    {
        public IObservable<SoilActionType> OnClickAsObservable { get; }

        public void Initialize(Sprite sprite,RectTransform parntTransform, SoilActionType actionType);
    }
}