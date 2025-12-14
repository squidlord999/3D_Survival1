 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory_System : MonoBehaviour
{
    public static Inventory_System Instance { get; set; }
    public GameObject inventoryScreenUI;
    public bool isOpen;
    public List<GameObject> SlotSpace = new List<GameObject>();
    public List<string> InventoryList = new List<string>();
    private GameObject ItemToAdd;
    private GameObject EquipSlot;
    public GameObject pickupAlert;
    public TMPro.TMP_Text pickupText;
    public Image pickupImage;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        isOpen = false;
        PopulateSlotSpace();
    }
    private void PopulateSlotSpace()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                SlotSpace.Add(child.gameObject);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            if (!Crafting_System.instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            isOpen = false;
        }
    } 
    public void AddToInventory(string itemName)
    {
        EquipSlot = FindSlot();
        ItemToAdd = Instantiate(Resources.Load<GameObject>(itemName), EquipSlot.transform);
        ItemToAdd.transform.SetParent(EquipSlot.transform);//creates the item as a child of the slot
        InventoryList.Add(itemName);
        Debug.Log(itemName + " was added to inventory.");
        TriggerPickupAlert(itemName, ItemToAdd.GetComponent<Image>().sprite);

    }
    public void RemoveItem(string itemName, int amountToRemove)
    {
        int counter = amountToRemove;
        for (var i = SlotSpace.Count - 1; i >= 0; i--)
        {
            if (SlotSpace[i].transform.childCount > 0 && SlotSpace[i].transform.GetChild(0).name == itemName + "(Clone)" && counter != 0)
            {
                Destroy(SlotSpace[i].transform.GetChild(0).gameObject);
                counter--;
                Debug.Log(itemName + " was removed from inventory.");
            }
        }
    }
    private void TriggerPickupAlert(string itemName, Sprite itemSprite)
    {
        StopAllCoroutines();
        pickupText.text = "Acquired: " + itemName;
        pickupImage.sprite = itemSprite;
        pickupAlert.SetActive(true);
        pickupAlert.GetComponent<CanvasGroup>().alpha = 1; // Reset alpha to fully visible
        StartCoroutine(HidePickupAlert());
    }
    private IEnumerator HidePickupAlert()
    {
        yield return new WaitForSeconds(1f);
        while (pickupAlert.GetComponent<CanvasGroup>().alpha > 0)
        {
            pickupAlert.GetComponent<CanvasGroup>().alpha -= Time.deltaTime / 1f; // Fade out over 1 second
            yield return null;
        }
        pickupAlert.SetActive(false);
    }
    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in SlotSpace)
        {
            if (slot.transform.childCount != 0)
            {
                counter++;
            }
        }
        if (counter >= SlotSpace.Count)
        {
            Debug.Log("Inventory Full");
            return true;
        }
        else
        {
            return false;
        }
    }
    private GameObject FindSlot()
    {
        foreach (GameObject slot in SlotSpace)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }
    public void ReCalculateList()
    {
        InventoryList.Clear();
        foreach (GameObject slot in SlotSpace) // go through each slot
        {
            if (slot.transform.childCount > 0) //checks if there is anything in the slot like stone(Clone)
            {
                InventoryList.Add(slot.transform.GetChild(0).name.Replace("(Clone)", ""));//removes the (Clone) from the name and adds it to inventory list
            }
        }
    }
}
