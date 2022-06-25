using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropZone : MonoBehaviour, IDropHandler//, IPointerEnterHandler, IPointerExitHandler
{
    public bool empty = true;
    public bool singleslot = false;
    public GameObject singleSlotCurrentHeld;
    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if(singleslot == false)
        {
            if (d != null)
            {
                d.parentReturn = this.transform;
            }
        }
        else
        {
            if (d != null && empty == true)
            {
                d.parentReturn = this.transform;
                singleSlotCurrentHeld = d.gameObject;
                empty = false;
            }
            if(d != null && empty == false)
            {
                if (singleSlotCurrentHeld == null)
                    return;

                singleSlotCurrentHeld.transform.SetParent(d.parentReturn);
                d.parentReturn = this.transform;
                singleSlotCurrentHeld = d.gameObject;
            }

        }
        
    }

    public void Update()
    {
        if(singleslot)
        {
            if (this.transform.childCount == 0)
            {
                singleSlotCurrentHeld = null;
                empty = true;
            }
        }
         
    }
    /*    public void OnPointerEnter(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }*/
}
