using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Item : ScriptableObject, IMoveable,IDescribable
{
    private static Item instance;

    public static Item MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<Item>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    [SerializeField]
    private string title;

    [SerializeField]
    private Quality quality;

    private SlotScript slot;

    public Sprite MyIcon { get => icon;}

    public int MyStackSize { get => stackSize;}

    public SlotScript MySlot { get => slot; set => slot = value; }

    public Quality MyQuality { get => quality;}

    public string MyTitle { get => title;}

    public void Remove()
    {
        if(MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }

    public virtual string GetDescription()
    {

        return string.Format("<color={0}> {1}</color>", QualityColor.MyColors[MyQuality],MyTitle);
    }
}
