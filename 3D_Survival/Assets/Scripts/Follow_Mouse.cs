using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Mouse : MonoBehaviour
{
    public Inventory_System inventorySystem;
    public Crafting_System craftingSystem; 

    void Update()
    {
        if (inventorySystem.isOpen || craftingSystem.isOpen)
        {
            Vector2 mousePosition = Input.mousePosition;
            transform.position = mousePosition;
        }
        else
        {
            transform.position = new Vector2(0, 0);
        }
    }
}
