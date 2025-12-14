using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Health_Bar : MonoBehaviour
{
    private Slider slider;
    public TMP_Text healthCounter;
    public GameObject playerstate;
    public float currentHealth, maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        currentHealth = playerstate.GetComponent<Player_State>().currentHealth;
        maxHealth = playerstate.GetComponent<Player_State>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerstate.GetComponent<Player_State>().currentHealth;
        maxHealth = playerstate.GetComponent<Player_State>().maxHealth;
        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;
        healthCounter.text = currentHealth / maxHealth * 100 + "%";
    }
}
