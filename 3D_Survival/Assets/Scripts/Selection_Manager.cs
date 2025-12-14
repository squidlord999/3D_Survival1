using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection_Manager : MonoBehaviour
{
     public GameObject interaction_Info_UI;
    Text interaction_text;
    [SerializeField] private float Raycast_Range;
    private GameObject lastTarget;
    public Image centerDotImage;
    public Image handIcon;
 
    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();//get the text component of the interaction info UI
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Raycast_Range))//if the raycast hits something
        {
            var selectionTransform = hit.transform;
            Debug.Log("Raycast hit: " + selectionTransform.name);

            if (selectionTransform.GetComponent<Interactable_Object>())//if the raycast hits an interactable object
            {
                Debug.Log("Looking at interactable object: " + selectionTransform.name);
                interaction_text.text = selectionTransform.GetComponent<Interactable_Object>().GetItemName();
                interaction_Info_UI.SetActive(true);
                lastTarget = selectionTransform.gameObject;
                selectionTransform.GetComponent<Interactable_Object>().onTarget = true;
                if (selectionTransform.GetComponent<Interactable_Object>().Obtainable == true && !Inventory_System.Instance.isOpen && !Crafting_System.instance.isOpen)
                {
                    Debug.Log("Hand icon should be showing");
                    centerDotImage.color = new Color(1, 1, 1, 0);
                    handIcon.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    Debug.Log("Center dot should be showing");
                    centerDotImage.color = new Color(1, 1, 1, 1);
                    handIcon.color = new Color(1, 1, 1, 0);
                }
            }
            else//if the raycast hits something that is not an interactable object
            {
                interaction_Info_UI.SetActive(false);
                if (lastTarget != null)
                {
                    lastTarget.GetComponent<Interactable_Object>().onTarget = false;

                }
                centerDotImage.color = new Color(1, 1, 1, 1);
                handIcon.color = new Color(1, 1, 1, 0);

            }
        }
        else//if the raycast doesn't hit anything
        {
            centerDotImage.color = new Color(1, 1, 1, 1);
            handIcon.color = new Color(1, 1, 1, 0);
            interaction_Info_UI.SetActive(false);
            if (lastTarget != null)
            {
                lastTarget.GetComponent<Interactable_Object>().onTarget = false;
            }
            
        }
    }
}