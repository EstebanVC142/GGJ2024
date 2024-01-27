using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestController : MonoBehaviour
{
    [SerializeField]
    private List<string> questNames = new List<string>();
    [SerializeField]
    private TextMeshProUGUI questText;
    private Dictionary<string, bool> quests = new Dictionary<string, bool>();

    private void Awake()
    {
        for (int i = 0; i < questNames.Count; i++)
        {
            quests.Add(questNames[i], false);
        }
    }

    public void CompleteQuest(string questName)
    {
        if (quests.ContainsKey(questName))
        {
            quests[questName] = true;
            Debug.Log($"item completado: {questName}");
        }
    }

    private void UpdateQuestList()
    {
        if (quests.Count > 0)
        {

        }
    }
}
