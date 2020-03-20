using UnityEngine;
using UnityEngine.UI;

namespace GiantMonkey
{
    public class TableCell : MonoBehaviour
    {
        [SerializeField] private Text textLabel = null;

        public void Construct(string value)
        {
            textLabel.text = value;
        }
    }

}
