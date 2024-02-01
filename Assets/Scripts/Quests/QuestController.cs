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

    public void CompleteQuest(string questName, int indexToActivate)
    {
        if (quests.ContainsKey(questName) && FinishQuests.actualObjetive == questName)
        {
            FinishQuests.ObjetivoCasa();
            Olfateo.singleton.objetivoActual = 0;
            Movement.singleton.blockMovement = true;
            Movement.singleton.anim.SetTrigger("entregar");
            AttackBehaviour.singleton.blockAttack = true;
            StartCoroutine(ActivarItem(indexToActivate));
            quests[questName] = true;
        }
    }

    private IEnumerator ActivarItem(int indexToActivate)
    {
        yield return new WaitForSeconds(1);
        Muertos.singleton.Activar(indexToActivate);
        Movement.singleton.blockMovement = false;
    }
}
