using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour,IInteractable
{
    [SerializeField]
    private VendorItem[] items;

    [SerializeField]
    private VendorWindow vendowWindow;

    public bool IsOpen{ get; set; }

    public void Interact()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            vendowWindow.CreatePages(items);
            vendowWindow.Open(this);
        }
        
    }

    public void StopInteract()
    {
        if (IsOpen)
        {
            IsOpen = false;
            
            vendowWindow.CLose();
        }
       
    }

    
}
