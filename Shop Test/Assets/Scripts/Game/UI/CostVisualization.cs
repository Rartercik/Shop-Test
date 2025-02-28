using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Text))]
    public class CostVisualization : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void UpdateCost(int cost)
        {
            _text.text = cost.ToString();
        }
    }
}