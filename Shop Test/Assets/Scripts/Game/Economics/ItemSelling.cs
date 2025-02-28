using UnityEngine;
using Game.UI;

namespace Game.Economics
{
    [RequireComponent(typeof(CostVisualization))]
    public class ItemSelling : MonoBehaviour
    {
        [SerializeField] private Vault _vault;
        [SerializeField] private int _inventoryCost;
        [SerializeField] private int _shopCost;

        public void TryUpdateSelling(bool inventoryChanged, bool inPlayerInventory)
        {
            if (CanBeUpdated(inventoryChanged, inPlayerInventory) == false) return;

            if (inPlayerInventory)
            {
                _vault.TakeMoney(_shopCost);
            }
            else
            {
                _vault.GiveMoney(_inventoryCost);
            }
        }

        public bool CanBeUpdated(bool inventoryChanged, bool toPlayerInventory)
        {
            var cost = GetCost(toPlayerInventory);
            var availableCost = toPlayerInventory == false || cost <= _vault.Money;
            return inventoryChanged && availableCost;
        }

        public int GetCost(bool toPlayerInventory)
        {
            return toPlayerInventory ? _shopCost : _inventoryCost;
        }
    }
}