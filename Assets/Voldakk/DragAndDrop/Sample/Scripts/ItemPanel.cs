using UnityEngine.UI;

namespace Voldakk.DragAndDrop
{
    public class ItemPanel : DragAndDropPanel
    {
        public Image icon;
        public Text count;

        public override void SetObject(object o)
        {
            if (o == null || !(o is Item))
            {
                icon.enabled = false;
                count.text = "";
            }
            else
            {
                Item item = o as Item;

                icon.enabled = true;
                icon.sprite = item.icon;

                if (item.stackSize > 1)
                    count.text = item.count.ToString();
                else
                    count.text = "";
            }
        }
    }
}