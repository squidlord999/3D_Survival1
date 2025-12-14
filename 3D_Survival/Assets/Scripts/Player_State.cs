using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Player_State : MonoBehaviour
{
    public static Player_State Instance { get; set; }
    
    // ------ Health ------
    public float currentHealth = 100f;
    public float maxHealth = 100f;

    // ------ Hunger ------
    public float currentHunger = 100f;
    public float maxHunger = 100f;


    // ------ Thirst ------
    public float currentThirst = 100f;
    public float maxThirst = 100f;

    
    
    
    
    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }
    private void Start() 
    {
      currentHealth = maxHealth;  
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
