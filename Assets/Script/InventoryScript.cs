using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void ItemCountChanged(Item item);

public class InventoryScript : MonoBehaviour
{
    public event ItemCountChanged itemCountChangedEvent;

    private static InventoryScript instance;

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<InventoryScript>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Item[] items;

    [SerializeField]
    private BagButton[] bagButtons;

    private SlotScript fromSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private List<Bag> bags = new List<Bag>();

    public bool CanAddBag
    {
        get { return bags.Count < 2; }
    }

    public SlotScript FromSlot 
    { get => fromSlot; 

        set
        {
            fromSlot = value;

            if(value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        } 
    }

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(75);
        bag.Use();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(74);
            bag.Use();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(74);
            AddItem(bag);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            HealthPotion potion = (HealthPotion)Instantiate(items[4]);
            AddItem(potion);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            
           
            AddItem((Armor)Instantiate(items[1]));
            AddItem((Armor)Instantiate(items[2]));
            AddItem((Armor)Instantiate(items[3]));
            AddItem((Armor)Instantiate(items[5]));
            AddItem((Armor)Instantiate(items[6]));
            AddItem((Armor)Instantiate(items[7]));
            AddItem((Armor)Instantiate(items[8]));
            AddItem((Armor)Instantiate(items[9]));
            AddItem((Armor)Instantiate(items[10]));
        }
    }
    private bool PlaceInEmpty(Item item)
    {
        foreach(Bag bag in bags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
               // OnItemCountChanged(item);
                return true;
            }
        }

        return false;
    }

    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach(Bag bag in bags)
        {
            foreach(SlotScript slot in bag.MyBagScript.MySlots)
            {
                if(!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach(Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                    }
                }
            }
        }

        return useables;
    }

    public void AddBag(Bag bag)
    {
        foreach(BagButton bagButton in bagButtons)
        {
            if(bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                bags.Add(bag);
                break;
            }
        }
    }

    private bool PlaceInStack(Item item)
    {
        foreach(Bag bag in bags)
        {
            foreach(SlotScript slots in bag.MyBagScript.MySlots)
            {
                if (slots.StackItem(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }

        return false;
    }

    public void OpenClose()
    {
        bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);

        foreach(Bag bag in bags)
        {
            if(bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }

    public bool  AddItem(Item item)
    {
       if(item.MyStackSize > 0)
       {
            if (PlaceInStack(item))
            {
                return true;
            }
       }

        return PlaceInEmpty(item);
    }

    public void OnItemCountChanged(Item item)
    {
        if(itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }
}
