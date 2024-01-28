using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NinoSeguidor : MonoBehaviour
{
    NavMeshAgent agente;
    public Transform target;
    public Animator animator;
    void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        StartCoroutine(MiUpdate());
    }

    // Update is called once per frame
    IEnumerator MiUpdate()
    {
		while (true)
		{
            agente.SetDestination(target.position);
            animator.SetFloat("velocidad", agente.velocity.sqrMagnitude);
            yield return new WaitForSeconds(0.5f);
		}
    }
}
