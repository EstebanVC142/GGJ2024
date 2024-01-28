using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestController : MonoBehaviour
{
    public Dictionary<string, bool> quests = new Dictionary<string, bool>();
    [SerializeField]
    private FinishQuests FinishQuests;

    private void Awake()
    {

    }

    public void CompleteQuest(string questName)
    {
        if (quests.ContainsKey(questName))
        {
            quests[questName] = true;
            Debug.Log($"item completado: {questName}");
        }
    }
}
