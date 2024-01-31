using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestController : MonoBehaviour
{
    public static QuestController singleton;
    public Dictionary<string, bool> quests = new Dictionary<string, bool>();
    [SerializeField]
    private FinishQuests FinishQuests;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            DestroyImmediate(gameObject);
    }

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
            FinishQuests.ObjetivoCasa();
            Olfateo.singleton.objetivoActual = 0;
            quests[questName] = true;
        }
    }
}
