using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Blueprint : MonoBehaviour
{
    public string name;
    public string req1;
    public string req2;
    public int req1Amount;
    public int req2Amount;
    public int numOfRequirements;
    Button craftBTN;
    TMP_Text Req1, Req2;

    public static Blueprint Instance { get; internal set; }
    private void Awake()
    {
        Instance = this; // Now it's set correctly
    }
    void Start()
    {

        Req1 = transform.Find("req1").GetComponent<TMP_Text>();
        Req2 = transform.Find("req2").GetComponent<TMP_Text>();
        craftBTN = transform.Find("CraftButton").GetComponent<Button>();
        craftBTN.onClick.AddListener(delegate { Crafting_System.instance.CraftAnyItem(this); });
    }
    public Blueprint(string name, string req1, string req2, int req1Amount, int req2Amount, int numOfRequirements)
    {
        this.name = name;
        this.req1 = req1;
        this.req2 = req2;
        this.req1Amount = req1Amount;
        this.req2Amount = req2Amount;
        this.numOfRequirements = numOfRequirements;
    }
     public void RefreshNeededItems()
    {
        int req1Count = 0;
        int req2Count = 0;
        foreach (string itemName in Inventory_System.Instance.InventoryList)

        {
            if (itemName == req1)
            {
                req1Count++;
            }
            if (itemName == req2)
            {
                req2Count++;
            }
           
        }
        Req1.text = req1Amount + " " + req1 + " [" + req1Count + "]";
        Req2.text = req2Amount + " " + req2 + " [" + req2Count + "]";
        if (req1Count >= req1Amount && req2Count >= req2Amount )
        {
            craftBTN.gameObject.SetActive(true);
        }
        else
        {
            craftBTN.gameObject.SetActive(false);
        }
    }
}

