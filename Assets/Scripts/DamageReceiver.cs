using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour, ITakeDamage
{
    public void TakeDamage(int damage)
    {
        Debug.Log($"damage taken: {damage}");
    }
}
