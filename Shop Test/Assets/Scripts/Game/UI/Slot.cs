using UnityEngine;
using Game.Inventory;

namespace Game.UI
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private bool _playerInventory;
        [SerializeField] private bool _occupy;

        public bool TryOccupy(Item item, ref Slot currentOccupiedSlot)
        {
            if (_occupy == false)
            {
                item.TryUpdateState(_playerInventory, out _occupy);
                if (_occupy)
                {
                    item.RectTransform.position = GetComponent<RectTransform>().position;
                    currentOccupiedSlot = this;
                    return true;
                }

                return false;
            }
            
            item.ReturnToStartPosition();
            return false;
        }

        public void Free()
        {
            _occupy = false;
        }
    }
}
