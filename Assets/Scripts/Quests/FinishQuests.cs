using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SearchService;

public class FinishQuests : MonoBehaviour
{
    public List<string> questNames = new List<string>();
    private Dictionary<string, bool> quests = new Dictionary<string, bool>();
    [SerializeField]
    private PlayerInput input;
    [SerializeField]
    private TextMeshProUGUI questText;
    [SerializeField]
    private TextMeshProUGUI dialogText;
    public string actualObjetive;

    private void Awake()
    {
        for (int i = 0; i < questNames.Count; i++)
        {
            quests.Add(questNames[i], false);
        }
        UpdateQuestList();
    }

    private void OnTriggerStay(Collider other)
    {
        if (input.actions["Action"].WasPressedThisFrame())
        {
            if (other.TryGetComponent(out QuestController player))
            {
                quests = player.quests;
                UpdateQuestList();
            }
        }
    }

    private void UpdateQuestList()
    {
        questText.text = "";
        actualObjetive = questNames[1];
        if (quests.Count > 0)
        {
            foreach (var quest in quests)
            {
                questText.text += $"- {quest.Key}: {quest.Value}\n";
                if (quest.Value && questNames.IndexOf(quest.Key) < questNames.Count - 1)
                {
                    actualObjetive = questNames[questNames.IndexOf(quest.Key) + 1];
                    Olfateo.singleton.objetivoActual = questNames.IndexOf(quest.Key) + 1;
                }
                else if (questNames.IndexOf(quest.Key) == questNames.Count - 1 && quest.Value)
                {
                    Debug.Log("ganó");
                }
            }
        }
        dialogText.text = $"el objetivo actual es: {actualObjetive}, Rómpele el cuello!";
    }
}
