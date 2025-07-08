using System;
using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Views.UIVIiews
{
    public class PlantUiView : MonoBehaviour
    {
        [SerializeField] private RectTransform _gridTransform;
        private Dictionary<PlantStatus, Image> _imageDict;
        
        public void Initialize(Dictionary<PlantStatus, Image> imageDict, float height, int soirtingOrder = 0)
        {
            var canvas = GetComponent<Canvas>();
            canvas.sortingOrder = soirtingOrder;
            _imageDict = imageDict;
            transform.localPosition = new Vector3(0, height + 1, 0);
            foreach (var image in imageDict)
            {
                image.Value.transform.SetParent(_gridTransform, false);
            }
            ClearStatusIcons();
        }

        public void SpriteChanged(float height)
        {
            transform.localPosition = new Vector3(0, height + 1, 0);
        }
        
        public void HideIcon(PlantStatus status)
        {
            if (_imageDict.TryGetValue(status, out var image))
            {
                image.gameObject.SetActive(false);
            }
        }
        
        public void ShowIcon(PlantStatus status)
        {
            if (_imageDict.TryGetValue(status, out var image))
            {
                image.gameObject.SetActive(true);
            }
        }

        private void ClearStatusIcons()
        {
            foreach (var image in _imageDict)
            {
                image.Value.gameObject.SetActive(false);
            }
        }
    }
}