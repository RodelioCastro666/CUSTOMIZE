using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    [SerializeField]
    private ArmorType armorType;

    private Armor equippedArmor;

    [SerializeField]
    private Image icon;

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void EquipArmor(Armor armor)
    {
        armor.Remove();

        if(equippedArmor != null)
        {
            armor.MySlot.AddItem(equippedArmor);
            UiManager.MyInstance.RefreshToolTip(equippedArmor);
        }

        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        this.equippedArmor = armor;

        if(HandScript.MyInstance.MyMoveable == (armor as IMoveable))
        {
            HandScript.MyInstance.Drop();
        }

      
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable is Armor)
            {
                Armor tmp = (Armor)HandScript.MyInstance.MyMoveable;

                if (tmp.MyArmorType == armorType)
                {
                    EquipArmor(tmp);
                }
            }
        }
    }
}
