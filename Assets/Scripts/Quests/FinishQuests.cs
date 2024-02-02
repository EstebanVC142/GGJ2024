using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            Muertos.singleton.DesactivarConDelay(0.5f);
            AttackBehaviour.singleton.blockAttack = false;
            Movement.singleton.blockMovement = true;
            Movement.singleton.GetComponent<Vida>().Curar(2);
            UpdateQuestList();
        }
    }

    public void ObjetivoCasa()
    {
        StartCoroutine(ShowMessage("Vuelve a casa y deja tu presa.", 5f));
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
            switch (actualObjetive)
            {
                case "gallina":
                    StartCoroutine(ShowMessage("kikiriki hace cada mañana. Tráeme su cuerpo, pero sin su cabeza. Es el primer ingrediente. Tu objetivo actual es la gallina.", 10f));
                    break;
                case "conejo":
                    StartCoroutine(ShowMessage("Saltan y son escurridizos. Tráeme su cabeza. Es el segundo ingrediente. Tu objetivo actual es el conejo.", 10f));
                    break;
                case "gato":
                    StartCoroutine(ShowMessage("Ronronea y ronronea. Tráeme su cola. Es el tercer ingrediente. Tu objetivo actual es el gato.", 10f));
                    break;
                case "toro":
                    StartCoroutine(ShowMessage("Embiste con fuerza, levantando polvo con cada paso. Tráeme alguna de sus patas. Es el último ingrediente. Tu objetivo actual es el toro.", 10f));
                    break;
                default:
                    StartCoroutine(ShowMessage($"el objetivo actual es: {actualObjetive}, Rómpele el cuello!", 10f));
                    break;
            }
        }
        else
        {
            SceneManager.LoadScene("Final");
        }
    }

    private IEnumerator ShowMessage(string message, float seconds = 3)
    {
        dialogPanel.SetActive(true);
        dialogText.text = message;
        yield return new WaitForSeconds(seconds);
        dialogPanel.SetActive(false);
    }
}
