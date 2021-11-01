using UnityEngine;
using UnityEngine.EventSystems;

namespace Voldakk.DragAndDrop
{
    public abstract class DragAndDropContainer : MonoBehaviour
    {
        [Tooltip("The panel used when draging an object from this container")]
        public DragAndDropPanel dragPanel;

        protected int containerIndex;

        void Start()
        {
            // Subscribe to the DragAndDropManager
            DragAndDropManager.Instance.Subscribe(this);

            // Hide the panel in case it's vissible
            if (dragPanel.gameObject.activeSelf)
                dragPanel.gameObject.SetActive(false);
        }

        /// <summary>
        /// Set the container index of all panels
        /// </summary>
        /// <param name="containerIndex">The containers index</param>
        public void SetContainerIndex(int containerIndex)
        {
            this.containerIndex = containerIndex;
            Initialize();
        }

        /// <summary>
        /// Use this for initialization
        /// </summary>
        protected virtual void Initialize() { }

        /// <summary>
        /// Gets the object at the given indices
        /// </summary>
        /// <param name="indices">The indices where the object is stored</param>
        /// <returns>The object at the given indices</returns>
        public abstract object GetObject(int[] indices, bool isFromContainer);

        /// <summary>
        /// Remove the object at the given indices
        /// </summary>
        /// <param name="indices">The indices where the object is stored</param>
        public abstract void RemoveObject(int[] indices);

        /// <summary>
        /// Try to recieve an object from another container
        /// </summary>
        /// <param name="o">The object to recieve</param>
        /// <param name="indices">The indices where the object should be stored</param>
        /// <returns>Whether the container sucsessfully recieved the object</returns>
        public abstract bool RecieveObject(object o, int[] indices);

        /// <summary>
        /// When an object is clicked
        /// </summary>
        /// <param name="button">The mouse button that was pressed</param>
        /// <param name="indices">The indices where the object is stored</param>
        public abstract void OnObjectMouseDown(PointerEventData.InputButton button, int[] indices);
    }
}