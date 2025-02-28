using Game.Economics;
using Game.UI;
using UnityEngine;

namespace Game.Inventory
{
    [RequireComponent(typeof(InventoryItemMover))]
    [RequireComponent(typeof(ItemSelling))]
    [RequireComponent(typeof(CostVisualization))]
    public class Item : MonoBehaviour
    {
        [SerializeField] private bool _inPlayerInventory;
        
        private InventoryItemMover _mover;
        private ItemSelling _selling;
        private CostVisualization _visualization;

        public RectTransform RectTransform { get; private set; }

        private void Start()
        {
            RectTransform = GetComponent<RectTransform>();
            _mover = GetComponent<InventoryItemMover>();
            _selling = GetComponent<ItemSelling>();
            _visualization = GetComponent<CostVisualization>();
            var cost = _selling.GetCost(!_inPlayerInventory);
            _visualization.UpdateCost(cost);
        }

        public void TryUpdateState(bool toPlayerInventory, out bool updated)
        {
            var inventoryChanged = _inPlayerInventory != toPlayerInventory;
            if (_selling.CanBeUpdated(inventoryChanged, toPlayerInventory))
            {
                _selling.TryUpdateSelling(inventoryChanged, toPlayerInventory);
                _inPlayerInventory = !_inPlayerInventory;
                var cost = _selling.GetCost(!_inPlayerInventory);
                _visualization.UpdateCost(cost);
                updated = true;
            }
            else
            {
                if (inventoryChanged)
                {
                    _mover.ReturnToStartPosition();
                    updated = false;
                }
                else
                {
                    updated = true;
                }
            }
        }

        public void ReturnToStartPosition()
        {
            _mover.ReturnToStartPosition();
        }
    }
}