using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentReturn = null;
    public Transform movingParent;
    public void Start()
    {
        movingParent = this.transform.parent.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentReturn = this.transform.parent;
        this.transform.SetParent(movingParent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parentReturn);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //Yikes.
    public void Destruction()
    {
        Destroy(this.gameObject);
    }
}
