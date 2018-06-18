using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Voldakk.DragAndDrop
{
    public class DragAndDropManager : MonoBehaviour
    {
        public static DragAndDropManager instance;

        [Tooltip("All the subscribed drag and drop containers")]
        public List<DragAndDropContainer> containers = new List<DragAndDropContainer>();

        private object fromObject;
        private int fromContainer;
        private int[] fromIndices;
        private DragAndDropPanel currentPanel;

        // Singleton
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogError("More than one DragAndDropManager in the scene!");
                Destroy(this);
            }
        }

        /// <summary>
        /// Lets a DragAndDropContainer register to be able to recieve objects
        /// </summary>
        /// <param name="container">The DragAndDropContainer</param>
        public void Subscribe(DragAndDropContainer container)
        {
            if (!containers.Contains(container))
            {
                containers.Add(container);
                container.SetContainerIndex(containers.Count - 1);
            }
        }

        /// <summary>
        /// When the player begins dragging an object 
        /// </summary>
        /// <param name="containerIndex">The containers index</param>
        /// <param name="objectIndices">The object indices of the panel</param>
        public void ItemOnBeginDrag(int containerIndex, int[] objectIndices)
        {
            // Remember where the object is comming from
            fromContainer = containerIndex;
            fromIndices = objectIndices;

            // Get the object from the sender
            fromObject = containers[fromContainer].GetObject(fromIndices, true);

            if (fromObject == null)
            {
                ItemOnEndDrag();
                return;
            }

            // Get the correct drag panel
            currentPanel = containers[containerIndex].dragPanel;

            // Set the object in the panel
            currentPanel.SetObject(fromObject);

            // Show the panel
            currentPanel.gameObject.SetActive(true);
        }

        /// <summary>
        /// Called every frame when the player is currently draging an object
        /// </summary>
        public void ItemOnDrag()
        {
            if (currentPanel != null)
                currentPanel.transform.position = Input.mousePosition;
        }

        /// <summary>
        /// When the player stops dragging an object 
        /// </summary>
        public void ItemOnEndDrag()
        {
            // Hide the panel
            if (currentPanel != null)
                currentPanel.gameObject.SetActive(false);

            // Reset
            fromObject = null;
            fromContainer = -1;
            fromIndices = null;
            currentPanel = null;
        }

        /// <summary>
        /// When an object is dropped on an item panel
        /// </summary>
        /// <param name="toContainer">The containers index</param>
        /// <param name="toIndices">The object indices of the panel</param>
        public void ItemOnDrop(int toContainer, int[] toIndices)
        {
            if (fromObject == null)
                return;

            // Get the object from the reciever
            object toObject = containers[toContainer].GetObject(toIndices, false);


            // If theres objects in both slots
            if (toObject != null && fromObject != null)
            {
                // Try to add the fromObject to the toObject
                if (containers[toContainer].RecieveObject(fromObject, toIndices))
                {
                    containers[fromContainer].RemoveObject(fromIndices);
                    return;
                }

                // Remove the objects
                containers[toContainer].RemoveObject(toIndices);
                containers[fromContainer].RemoveObject(fromIndices);

                // Try to switch the objects
                if (!containers[toContainer].RecieveObject(fromObject, toIndices) || !containers[fromContainer].RecieveObject(toObject, fromIndices))
                {
                    // If any of them won't recieve the other object

                    // Remove any objects from both
                    containers[toContainer].RemoveObject(toIndices);
                    containers[fromContainer].RemoveObject(fromIndices);

                    // Give them their own object back
                    containers[toContainer].RecieveObject(toObject, toIndices);
                    containers[fromContainer].RecieveObject(fromObject, fromIndices);
                }
            }
            else if (toObject != null)
            {
                // If the reciever sucsessfully recieved the object
                if (containers[fromContainer].RecieveObject(toObject, fromIndices))
                {
                    containers[toContainer].RemoveObject(toIndices);
                }
            }
            else if (fromObject != null)
            {
                // If the reciever sucsessfully recieved the object
                if (containers[toContainer].RecieveObject(fromObject, toIndices))
                {
                    containers[fromContainer].RemoveObject(fromIndices);
                }
            }
        }

        /// <summary>
        /// When an panel is clicked
        /// </summary>
        /// <param name="button">The mouse button that was pressed</param>
        /// <param name="containerIndex">The containers index</param>
        /// <param name="objectIndices">The object indices of the panel</param>
        public void ItemOnMouseDown(PointerEventData.InputButton button, int containerIndex, int[] objectIndices)
        {
            ItemOnEndDrag();

            containers[containerIndex].OnObjectMouseDown(button, objectIndices);
        }
    }
}