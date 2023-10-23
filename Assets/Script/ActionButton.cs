using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable, IDropHandler
{
    
    public IUseable MyUseable { get; set; }

    public Button MyButton { get; private set; }

    private Stack<IUseable> useables = new Stack<IUseable>();

    private int count;

    [SerializeField]
    private TextMeshProUGUI stackSize;
    
    [SerializeField]
    private Image icon;

    public Image MyIcon { get => icon; set => icon = value; }

    public int MyCount => count;

    public TextMeshProUGUI MyStackText 
    {
        get { return stackSize; }
    }

   


    // Start is called before the first frame update
    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if(HandScript.MyInstance.MyMoveable == null)
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
            if(useables != null && useables.Count > 0)
            {
                useables.Peek().Use();
            }
        }

       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    public void SetUseable(IUseable useable)
    {
        if(useable is Item)
        {
            useables = InventoryScript.MyInstance.GetUseables(useable);
            count = useables.Count;
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            InventoryScript.MyInstance.FromSlot = null;
        }
        else
        {
            this.MyUseable = useable;
        }

        

        UpdateVisual(useable as IMoveable);
    }

    public void UpdateVisual(IMoveable moveable)
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
      //  MyIcon.sprite = moveable.MyIcon;
        MyIcon.color = Color.white;

        if(count > 1)
        {
            UiManager.MyInstance.UpdateStackSize(this);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUseable)
            {
                SetUseable(HandScript.MyInstance.MyMoveable as IUseable);
            }
        }
    }

    public void UpdateItemCount(Item item)
    {
        if(item is IUseable && useables.Count > 0)
        {
            if(useables.Peek().GetType() == item.GetType())
            {
                useables = InventoryScript.MyInstance.GetUseables(item as IUseable);

                count = useables.Count;

                UiManager.MyInstance.UpdateStackSize(this);
            }
        }
    }
}
