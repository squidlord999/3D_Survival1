using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Object : MonoBehaviour
{
    public string ItemName;
    [SerializeField] private float interactionRange = 4f;
    public bool Obtainable = false;
    private GameObject Player;
    public bool onTarget = false;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public string GetItemName()
    {
        if(!Inventory_System.Instance.isOpen && !Crafting_System.instance.isOpen)
            return ItemName;
        else
            return "";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Vector3.Distance(Player.transform.position, transform.position) < interactionRange && onTarget)
        {
            Debug.Log("Interacted with " + ItemName);
            if (Inventory_System.Instance.CheckIfFull())//check if inventory is full
            {
                Debug.Log("Inventory is full");
                return;
            }
            if (Obtainable == true && !Inventory_System.Instance.isOpen && !Crafting_System.instance.isOpen)
            {
                Inventory_System.Instance.AddToInventory(ItemName);
                Debug.Log(ItemName + " was added to inventory.");
                Destroy(gameObject);
            }           
        }
    }
}
