using UnityEngine;
using UnityEngine.EventSystems;

namespace Voldakk.DragAndDrop
{
    public abstract class DragAndDropContainer : MonoBehaviour
    {
        [Tooltip("The panel vissible when draging an object from this container")]
        public DragAndDropPanel dragPanel;

        protected int containerIndex;

        void Start()
        {
            // Subscribe to the DragAndDropManager
            DragAndDropManager.instance.Subscribe(this);

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
        /// Return the object in the given indices
        /// </summary>
        /// <param name="indices">The indeces in which the object is stored</param>
        /// <returns>The object in the given indices</returns>
        public abstract object GetObject(int[] indices, bool isFromContainer);

        /// <summary>
        /// Remove the object in the given indices
        /// </summary>
        /// <param name="indices">The indeces in which the object is stored</param>
        public abstract void RemoveObject(int[] indices);

        /// <summary>
        /// Try to recieve an object form another container
        /// </summary>
        /// <param name="o">The object to recieve</param>
        /// <param name="indices">The indeces in which the object should be stored</param>
        /// <returns>Whether the container sucsessfully recieved the object</returns>
        public abstract bool RecieveObject(object o, int[] indices);

        /// <summary>
        /// When an object is clicked
        /// </summary>
        /// <param name="button">The mouse button that was pressed</param>
        /// <param name="indices">The indeces in which the object is stored</param>
        public abstract void OnObjectMouseDown(PointerEventData.InputButton button, int[] indices);
    }
}