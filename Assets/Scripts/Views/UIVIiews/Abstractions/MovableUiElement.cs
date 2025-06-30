using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Views.UIVIiews.Abstractions
{
    public abstract class MovableUiElement : MonoBehaviour
    {
        [SerializeField] private RectTransform _contentPanel;
        [FormerlySerializedAs("_topPosition")] [SerializeField] protected Vector3 _startPosition;
        [FormerlySerializedAs("_bottomPosition")] [SerializeField] protected Vector3 _endPosition;
        
        protected CancellationTokenSource _cts;
        
        protected async UniTask MoveTo(Vector3 position, CancellationToken token)
        {
            var tcs = new UniTaskCompletionSource();
            var tween = _contentPanel.DOAnchorPos(position, 0.5f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => tcs.TrySetResult());
            
            await using (token.Register(() => {
                             tween.Kill();
                             tcs.TrySetCanceled(token);
                         }))
            {
                await tcs.Task;
            }
        }

        protected void CancelMove()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
        }
    }
}