using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;

public class TogglePurchaseAmount : MonoBehaviour
{
    private int[] options= {1, 10, 100};
    private int selected = 0;
    public int purchaseAmount = 1;
    TMP_Text displayThing;
    
        
    // Start is called before the first frame update
    void Start()
    {
        displayThing = GameObject.Find("PurchaseAmountText").GetComponent<TMP_Text>();
        UpdateDisplayAmount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        selected = (selected + 1) % options.Length;
        purchaseAmount = options[selected];
        UpdateDisplayAmount();
        Debug.Log(purchaseAmount);
    }

    private void UpdateDisplayAmount()
    {
        displayThing.text = "Toggle Buy/Worker Amount x " + purchaseAmount.ToString();
    }

    public int GetPurchaseAmount()
    {
        return purchaseAmount;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}
