using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UiManager : MonoBehaviour
{
    private static UiManager instance;

    public static UiManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<UiManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private CanvasGroup keyBindMenu;

    [SerializeField]
    private ActionButton[] actionButton;

    //[SerializeField]
    //private CanvasGroup spellBook;

    private GameObject[] keybindButtons;

    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybinds");
    }

    // Start is called before the first frame update
    void Start()
    {
       

       

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose(keyBindMenu);
        }
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    OpenClose(spellBook);
        //}
        if (Input.GetKeyDown(KeyCode.B))
        {
            InventoryScript.MyInstance.OpenClose();
        }
    }

   

    //public void OpenCloseMenu()
    //{
    //    keyBindMenu.alpha = keyBindMenu.alpha > 0 ? 0 : 1;
    //    keyBindMenu.blocksRaycasts = keyBindMenu.blocksRaycasts == true ? false : true;
    //    Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    //}

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void CLickActionButton(string buttonName)
    {
        Array.Find(actionButton, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    

    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if(clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }
        if(clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
    }
}
