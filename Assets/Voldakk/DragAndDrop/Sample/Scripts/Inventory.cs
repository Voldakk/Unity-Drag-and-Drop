using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Voldakk.DragAndDrop
{
    public class Inventory : DragAndDropContainer
    {
        public GameObject itemPanelPrefab;

        [SerializeField] private List<Item> items;

        private List<ItemPanel> itemPanels;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // Clear the list
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }


            itemPanels = new List<ItemPanel>();

            for (int i = 0; i < items.Count; i++)
            {
                // Make a copy of the scriptable object
                if (items[i] != null)
                    items[i] = Instantiate(items[i]);

                // Create a panel to display the item
                ItemPanel ip = Instantiate(itemPanelPrefab, transform).GetComponent<ItemPanel>();

                ip.SetIndeces(containerIndex, new int[] { i });
                ip.SetObject(items[i]);

                itemPanels.Add(ip);
            }
        }

        /// <summary>
        /// Return the object in the given indices
        /// </summary>
        /// <param name="indices">The indeces in which the object is stored</param>
        /// <returns>The object in the given indices</returns>
        public override object GetObject(int[] indices, bool isFromContainer)
        {
            if (indices.Length == 1 && indices[0] >= 0 && indices[0] < items.Count)
                return items[indices[0]];

            return null;
        }

        /// <summary>
        /// Try to recieve an object form another container
        /// </summary>
        /// <param name="o">The object to recieve</param>
        /// <param name="indices">The indeces in which the object should be stored</param>
        /// <returns>Whether the container sucsessfully recieved the object</returns>
        public override bool RecieveObject(object o, int[] indices)
        {
            // If it's not an item, or it's thw wrong number of indices
            if (!(o is Item) || indices.Length != 1)
                return false;

            // Get the index
            int index = indices[0];

            // Make sure the index is within range
            if (index < 0 && index >= items.Count)
                return false;

            // Get the item
            Item i = o as Item;

            // If the recieving slot is empty
            if (items[index] == null)
            {
                items[index] = i;
                itemPanels[index].SetObject(i);

                return true;
            }
            else // There's an item in the slot
            {
                // If it's the same type of item
                if (items[index].Equals(i))
                {
                    // If we can combine the two stack of items
                    if (i.stackSize >= i.count + items[index].count)
                    {
                        items[index].count += i.count;
                        itemPanels[index].SetObject(items[index]);

                        return true;
                    }
                }
            }

            // Failed to recieve the object
            return false;
        }

        /// <summary>
        /// Remove the object in the given indices
        /// </summary>
        /// <param name="indices">The indeces in which the object is stored</param>
        public override void RemoveObject(int[] indices)
        {
            // Check the indices
            if (indices.Length == 1 && indices[0] >= 0 && indices[0] < items.Count)
            {
                // Remove the object
                items[indices[0]] = null;
                itemPanels[indices[0]].SetObject(null);
            }
        }

        /// <summary>
        /// When an object is clicked
        /// </summary>
        /// <param name="button">The mouse button that was pressed</param>
        /// <param name="indices">The indeces in which the object is stored</param>
        public override void OnObjectMouseDown(PointerEventData.InputButton button, int[] indices)
        {
            if (indices.Length != 1)
                return;

            if (button == PointerEventData.InputButton.Left)
                Debug.LogFormat("Left click on index {0}", indices[0]);
            else if (button == PointerEventData.InputButton.Right)
                Debug.LogFormat("Right click on index {0}", indices[0]);
        }
    }
}