using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Crafting_System : MonoBehaviour
{
    public GameObject CraftingMenuUI;
    public GameObject ToolsMenuUI;
    public List<string> inventory = new List<string>();
    //Category Buttons
    Button toolsBTN;

    //Craft Buttons
    Button axeBTN;
    //REquirement Text
    TMP_Text AxeReq1, AxeReq2;
    public bool isOpen;
    public static Crafting_System instance { get; set; }
    public Blueprint[] allBlueprints;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        isOpen = false;
        toolsBTN = CraftingMenuUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsMenu(); });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            Debug.Log("C is pressed");
            CraftingMenuUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;
            RefreshBlueprints();
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            CraftingMenuUI.SetActive(false);
            ToolsMenuUI.SetActive(false);
            if (!Inventory_System.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            isOpen = false;
        }
    }
    private IEnumerator RefreshBlueprints()
    {
        yield return null;
        Debug.Log("Refreshing Blueprints...");
        foreach (Blueprint blueprint in allBlueprints)
        {
            blueprint.RefreshNeededItems();
            Debug.Log("Refreshed " + blueprint.name);
        }
    }
    private void OpenToolsMenu()
    {
        CraftingMenuUI.SetActive(false);
        ToolsMenuUI.SetActive(true);
        StartCoroutine(RefreshBlueprints());
    }
    public void CraftAnyItem(Blueprint blueprintToCraft)
    {
        Inventory_System.Instance.AddToInventory(blueprintToCraft.name);


       
        if(blueprintToCraft.numOfRequirements == 1)
        {
            Inventory_System.Instance.RemoveItem(blueprintToCraft.req1,blueprintToCraft.req1Amount);
        }
        else if(blueprintToCraft.numOfRequirements == 2)
        {
            Inventory_System.Instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.req1Amount);
            Inventory_System.Instance.RemoveItem(blueprintToCraft.req2, blueprintToCraft.req2Amount);
        }
        StartCoroutine(calculate());      

    }
    public IEnumerator calculate()
    {
        yield return new WaitForSeconds(0.1f);
        Inventory_System.Instance.ReCalculateList();
        foreach (Blueprint blueprint in allBlueprints)// calls refresh blueprints but doesn't have the wait
        {
            blueprint.RefreshNeededItems();
            Debug.Log("Refreshed " + blueprint.name);
        }
    }

}
