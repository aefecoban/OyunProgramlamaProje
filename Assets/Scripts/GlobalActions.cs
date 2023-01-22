using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GlobalActions : MonoBehaviour
{
    public void SeaToGround()
    {
        Player.Instance.transform.position = GameManager.Instance.SeaToGroundSpawnPoint;
        if(Player.Instance.transform.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            agent.Warp(GameManager.Instance.SeaToGroundSpawnPoint);
        }
    }

    public void GroundToSea()
    {
        Player.Instance.transform.position = GameManager.Instance.GroundToSeaSpawnPoint;
        if (Player.Instance.transform.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            agent.Warp(GameManager.Instance.GroundToSeaSpawnPoint);
        }
    }
}
