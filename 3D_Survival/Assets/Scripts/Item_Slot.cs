using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
 
 
public class Item_Slot : MonoBehaviour, IDropHandler
{
 
    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0 )
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        //if there is not item already then set our item.
        if (!Item)
        {
            Drag_Drop.itemBeingDragged.transform.SetParent(transform);
            Drag_Drop.itemBeingDragged.transform.localPosition = new Vector2(0, 0); 
        }
    }
}