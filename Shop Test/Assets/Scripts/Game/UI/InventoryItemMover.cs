using System.Collections.Generic;
using Game.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    [RequireComponent(typeof(Item))]
    public class InventoryItemMover : MonoBehaviour, IInitializePotentialDragHandler, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Slot _occupiedSlot;

        private Item _facadeItem;
        private RectTransform _rectTransform;
        private Vector3 _startPosition;

        private void Start()
        {
            _facadeItem = GetComponent<Item>();
            _rectTransform = GetComponent<RectTransform>();
        }
        
        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            eventData.useDragThreshold = false;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _startPosition = _rectTransform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var slotInCurrentPosition = GetSlotInPositionOrNull(_eventSystem, _rectTransform.position);
            
            if (slotInCurrentPosition != null)
            {
                var oldSlot = _occupiedSlot;
                if (slotInCurrentPosition.TryOccupy(_facadeItem, ref _occupiedSlot))
                {
                    oldSlot.Free();
                }
            }
            else
            {
                ReturnToStartPosition();
            }
        }

        public void ReturnToStartPosition()
        {
            _rectTransform.position = _startPosition;
        }

        private Slot GetSlotInPositionOrNull(EventSystem eventSystem, Vector2 position)
        {
            var pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = position;
            var UIs = new List<RaycastResult>();
            eventSystem.RaycastAll(pointerEventData, UIs);
            Slot result = null;
            foreach (var ui in UIs)
            {
                if (ui.gameObject.TryGetComponent(out Slot slot))
                {
                    result = slot;
                }
            }

            return result;
        }
    }
}
