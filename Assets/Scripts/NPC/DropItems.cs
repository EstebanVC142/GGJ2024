using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    [SerializeField]
    private string itemToDrop;
    public int indexToActivate;

    public void AddItem()
    {
        QuestController.singleton.CompleteQuest(itemToDrop, indexToActivate);
    }
}
