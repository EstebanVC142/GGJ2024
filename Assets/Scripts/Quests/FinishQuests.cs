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



    private void OnTriggerStay(Collider other)
    {
        if (input.actions["Action"].WasPressedThisFrame() && other.TryGetComponent(out QuestController player))
        {
            quests = player.quests;
        }
    }

    private void UpdateQuestList()
    {
        if (quests.Count > 0)
        {
            foreach (var quest in quests)
            {
                questText.text = $"{quest.Key}: {quest.Value}\n";
            }
        }
    }
}
