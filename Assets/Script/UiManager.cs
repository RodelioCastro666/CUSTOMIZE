using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Button[] actionButton;

    private KeyCode action1, action2, action3;

    // Start is called before the first frame update
    void Start()
    {
        action1 = KeyCode.Alpha1;
        action2 = KeyCode.Alpha2;
        action3 = KeyCode.Alpha3;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(action1))
        {
            ActionButtonsOnClick(0);
        }
        if(Input.GetKeyDown(action2))
        {
            ActionButtonsOnClick(1);
        }
        if(Input.GetKeyDown(action3))
        {
            ActionButtonsOnClick(2);
        }
    }

    private void ActionButtonsOnClick(int btnIndex)
    {
        actionButton[btnIndex].onClick.Invoke();
    }
}
