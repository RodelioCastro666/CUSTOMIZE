using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quest;

    [SerializeField]
    private QuestLog tmpLog;


    private void Awake()
    {
        tmpLog.AcceptQuest(quest[0]);
       // tmpLog.AcceptQuest(quest[1]);
    }
}
