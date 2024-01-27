using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    [SerializeField]
    private string itemToDrop;
    [SerializeField]
    private QuestController questController;
    [SerializeField]
    private Vida vida;

    public void AddItem()
    {
        questController.CompleteQuest(itemToDrop);
    }
}
