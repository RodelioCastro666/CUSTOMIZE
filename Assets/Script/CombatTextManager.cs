using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SCCTYPE { DAMAGE,HEAL,XP}

public class CombatTextManager : MonoBehaviour
{
    private static CombatTextManager instance;

    public static CombatTextManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<CombatTextManager>();
            }

            return instance;
        }
    }

    

    [SerializeField]
    private GameObject combatTxtPrefab;

    public void CreateText(Vector2 position, float offset, string text, SCCTYPE type, bool crit)
    {
        position.y += offset;
        Text sct =  Instantiate(combatTxtPrefab, transform).GetComponent<Text>();
        sct.transform.position = position;

        string operation = string.Empty;

        switch (type) 
        {
            case SCCTYPE.DAMAGE:
                operation += "-";
                sct.color = Color.red;
                break;
            case SCCTYPE.HEAL:
                operation += "+";
                sct.color = Color.green;
                break;
        }
        sct.text = operation + text;


        if (crit)
        {
            sct.GetComponent<Animator>().SetBool("crit", crit);
        }

    }
}
