using UnityEngine;
using UnityEngine.EventSystems;

namespace Voldakk.DragAndDrop
{
    public abstract class DragAndDropPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
    {
        DragAndDropManager manager;

        int containerIndex;
        int[] objectIndices;

        void Start()
        {
            manager = DragAndDropManager.Instance;
        }

        /// <summary>
        /// Set the container and object indices
        /// </summary>
        /// <param name="containerIndex">The container's index</param>
        /// <param name="objectIndices">The object indices</param>
        public void SetIndeces(int containerIndex, int[] objectIndices)
        {
            this.containerIndex = containerIndex;
            this.objectIndices = objectIndices;
        }

        /// <summary>
        /// Set the object
        /// </summary>
        /// <param name="o">The object</param>
        public abstract void SetObject(object o);

        /// <summary>
        /// Called before a drag is started
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            manager.ItemOnBeginDrag(containerIndex, objectIndices);
        }

        /// <summary>
        /// When draging is occuring this will be called every time the cursor is moved.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnDrag(PointerEventData eventData)
        {
            manager.ItemOnDrag();
        }

        /// <summary>
        /// Called when a drag is ended
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            manager.ItemOnEndDrag();
        }

        /// <summary>
        /// Called when an object is droped on this panel
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnDrop(PointerEventData eventData)
        {
            if (objectIndices == null)
                Debug.LogError("DragAndDropPanel missing objectIndices! If this is a drag panel all elements of the panel should have \"Raycats Target\" disabled.", gameObject);
            else
                manager.ItemOnDrop(containerIndex, objectIndices);
        }


        /// <summary>
        /// Called when this panel is clicked
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            manager.ItemOnMouseDown(eventData.button, containerIndex, objectIndices);
        }
    }
}