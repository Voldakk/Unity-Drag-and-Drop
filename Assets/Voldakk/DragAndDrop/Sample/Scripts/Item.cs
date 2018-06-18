using UnityEngine;

namespace Voldakk.DragAndDrop
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Item", menuName = "Voldakk/DragAndDrop/Item")]
    public class Item : ScriptableObject
    {
        new public string name;
        public Sprite icon;
        public int stackSize = 1;
        public int count = 1;

        public virtual bool Equals(Item other)
        {
            return name == other.name;
        }
    }
}