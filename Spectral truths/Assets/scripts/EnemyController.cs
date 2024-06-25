using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float detectionRadius = 200f;
    [SerializeField] private LayerMask obstaclesLayer;
    [SerializeField] private LayerMask playerLayer;

    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        detectPlayer();
    }

    private void detectPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstaclesLayer))
            {
                agent.SetDestination(player.position);
            } else
            {
                Debug.Log("El enemigo no esta viendo");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
