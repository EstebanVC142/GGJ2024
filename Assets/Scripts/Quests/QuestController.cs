using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestController : MonoBehaviour
{
    public Dictionary<string, bool> quests = new Dictionary<string, bool>();
    [SerializeField]
    private FinishQuests FinishQuests;

    private void Start()
    {
        for (int i = 0; i < FinishQuests.questNames.Count; i++)
        {
            quests.Add(FinishQuests.questNames[i], false);
        }
    }

    public void CompleteQuest(string questName)
    {
        if (quests.ContainsKey(questName) && FinishQuests.actualObjetive == questName)
        {
            quests[questName] = true;
            Debug.Log($"item completado: {questName}");
        }
    }
}
