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
    private TextMeshProUGUI dialogText;
    public GameObject dialogPanel;
    public List<Dialogador> dialogador = new List<Dialogador>();
    public string actualObjetive;
    private bool playerInside = false;
    public bool gano = false;
    public int dialogIndex;

    private void Awake()
    {
        for (int i = 0; i < questNames.Count; i++)
        {
            quests.Add(questNames[i], false);
        }
        UpdateQuestList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (input.actions["Action"].WasPressedThisFrame() && playerInside)
        {
            QuestController.singleton.gameObject.GetComponent<Perro>().animator.SetTrigger("entregar");
            quests = QuestController.singleton.quests;
            UpdateQuestList();
        }
    }

    public void ObjetivoCasa()
    {
        StartCoroutine(ShowMessage("vuelve con tu presa a la casa"));
    }

    private void UpdateQuestList()
    {
        actualObjetive = questNames[1];
        if (quests.Count > 0)
        {
            foreach (var quest in quests)
            {
                if (quest.Value && questNames.IndexOf(quest.Key) < questNames.Count - 1)
                {
                    actualObjetive = questNames[questNames.IndexOf(quest.Key) + 1];
                    Olfateo.singleton.objetivoActual = questNames.IndexOf(quest.Key) + 1;
                }
                else if (questNames.IndexOf(quest.Key) == questNames.Count - 1 && quest.Value)
                {
                    gano = true;
                    Debug.Log("ganó");
                }
            }
        }
        if (!gano)
        {
            //dialogador[dialogIndex].IniciarDialogo();dialogText.text = $"el objetivo actual es: {actualObjetive}, Rómpele el cuello!";
            StartCoroutine(ShowMessage($"el objetivo actual es: {actualObjetive}, Rómpele el cuello!", 5f));
        }
        else
            StartCoroutine(ShowMessage("ritual completado"));
    }

    private IEnumerator ShowMessage(string message, float seconds = 2)
    {
        dialogPanel.SetActive(true);
        dialogText.text = message;
        yield return new WaitForSeconds(seconds);
        dialogPanel.SetActive(false);
    }
}
