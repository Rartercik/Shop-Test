using System;
using UnityEngine;
using Game.UI;

namespace Game.Economics
{
    [RequireComponent(typeof(CostVisualization))]
    public class Vault : MonoBehaviour
    {
        [SerializeField] private int _money;

        private CostVisualization _visualization;

        public int Money => _money;

        private void Start()
        {
            _visualization = GetComponent<CostVisualization>();
            _visualization.UpdateCost(_money);
        }

        public void TakeMoney(int count)
        {
            if (_money < count)
            {
                throw new ArgumentException("You can't take more then the vault has");
            }

            _money -= count;
            _visualization.UpdateCost(_money);
        }

        public void GiveMoney(int count)
        {
            _money += count;
            _visualization.UpdateCost(_money);
        }
    }
}