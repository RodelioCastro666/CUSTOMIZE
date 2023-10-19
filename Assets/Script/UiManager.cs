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
            OpenCloseMenu();
        }
    }

   

    public void OpenCloseMenu()
    {
        keyBindMenu.alpha = keyBindMenu.alpha > 0 ? 0 : 1;
        keyBindMenu.blocksRaycasts = keyBindMenu.blocksRaycasts == true ? false : true;
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void CLickActionButton(string buttonName)
    {
        Array.Find(actionButton, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void SetUseable(ActionButton btn, IUseable useable)
    {
        btn.MyButton.image.sprite = useable.MyIcon;
        btn.MyButton.image.color = Color.white;
        btn.MyUseable = useable;
    }
}
