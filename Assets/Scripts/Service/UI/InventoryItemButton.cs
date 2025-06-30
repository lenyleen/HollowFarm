using System;
using DefaultNamespace.DataObjects;
using DefaultNamespace.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Service.UI
{
    public class InventoryItemButton: MonoBehaviour, IDisposable
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI quantityText;
        [SerializeField] private Button button;
        
        private int _quantity;
        
        public ICanBeDrawnInInventory Data{ get; private set; }

        public void Initialize<T>(T plantData, Sprite imageSprite, int quantity,UnityAction onClick) where T :ICanBeDrawnInInventory
        {
            Data = plantData;
            _quantity = quantity; 
            
            image.sprite = imageSprite;
            quantityText.text = quantity.ToString();
            button.onClick.AddListener(onClick);
        }

        public void UpdateQuantity(int quantity)
        {
            _quantity += quantity;
            if (_quantity <= 0)
            {
                Destroy(this.gameObject);
                Dispose();
            }

            quantityText.text = quantity > 99 ? "99+" : _quantity.ToString();
        }

        public void Dispose()
        {
            button.onClick.RemoveAllListeners();
            Destroy(this.gameObject);
        }
    }
}