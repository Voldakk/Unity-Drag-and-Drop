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

        /// <summary>
        /// Used to determine whether two items are the same type
        /// </summary>
        /// <param name="other">The other item</param>
        /// <returns>True if they are the same</returns>
        public virtual bool Equals(Item other)
        {
            return name == other.name;
        }
    }
}