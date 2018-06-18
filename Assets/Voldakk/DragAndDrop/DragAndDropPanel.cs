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
            manager = DragAndDropManager.instance;
        }

        /// <summary>
        /// Set the container and object indeces
        /// </summary>
        /// <param name="containerIndex">The containers index</param>
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

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            manager.ItemOnBeginDrag(containerIndex, objectIndices);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            manager.ItemOnDrag();
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            manager.ItemOnEndDrag();
        }

        public virtual void OnDrop(PointerEventData eventData)
        {
            if (objectIndices == null)
                Debug.LogError("DragAndDropPanel missing objectIndices! If this is a drag panel all elements of the panel should have \"Raycats Target\" disabled.", gameObject);
            else
                manager.ItemOnDrop(containerIndex, objectIndices);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            manager.ItemOnMouseDown(eventData.button, containerIndex, objectIndices);
        }
    }
}