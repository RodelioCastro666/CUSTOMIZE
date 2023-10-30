using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    private Quest selected;

    [SerializeField]
    private TextMeshProUGUI questDescription;

    private static QuestLog instance;

    public static QuestLog MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<QuestLog>();
            }

            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSelected()
    {
        ShowDescription(selected);
    }

    public void AcceptQuest(Quest quest)
    {
        foreach(CollectObjective o in quest.MyCollectObjectives)
        {
            InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(o.UpdateItemCount);
        }

        GameObject go = Instantiate(questPrefab, questParent);

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;
     

        go.GetComponent<TextMeshProUGUI>().text = quest.MyTitle;
    }

    public void ShowDescription(Quest quest)
    {
        if(selected != null)
        {
            selected.MyQuestScript.DeSelect();
        }

        string objectives = string.Empty;

        selected = quest;

        string title = quest.MyTitle;

        foreach(Objective obj in quest.MyCollectObjectives)
        {
            objectives += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }

        questDescription.text = string.Format("{0}\n\n<size=25>{1}</size>\nObjectives:\n<size=25>{2}</size>", title, quest.MyDescription,objectives);
    }
}
