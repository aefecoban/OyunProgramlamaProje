using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainStash : MonoBehaviour
{

    public static TrainStash instance;

    [SerializeField] GameObject prefabForStash;
    [SerializeField] int stashMaxSize = 20;
    [SerializeField] float objOffset = 1f;
    [SerializeField] Vector3 spawnOrigin = Vector3.zero;
    public bool canCollect => transform.childCount > 0 ? true : false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (TrainSystem.instance.isStop)
            return;

        float timer = TrainSystem.instance.getNormalizedTimer();
        if (timer >= 0.3f && timer <= 0.5f && transform.childCount < 20)
        {
            MetalSpawn();
        }
    }

    private void MetalSpawn()
    {
        int need = stashMaxSize - transform.childCount;
        for(int i = 0; i < need; i++)
        {
            GameObject go = Instantiate<GameObject>(prefabForStash, transform);
        }
        CalculatePos();
    }

    private void CalculatePos()
    {
        int j = 0;
        for (int i = 0; i < stashMaxSize; i++)
        {
            j++;
            Vector3 p1 = Vector3.zero + Vector3.forward * ((j-1) * objOffset);
            Vector3 p2 = Vector3.zero + Vector3.forward * ((j-1) * objOffset);
            
            p1 += new Vector3(1.01f, 0, 0) * (objOffset /2f);
            p2 += new Vector3(-1.01f, 0, 0) * (objOffset / 2f);
            
            transform.GetChild(i).transform.localPosition = p1;
            transform.GetChild(i + 1).transform.localPosition = p2;
            i++;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for(int i = 0; i < stashMaxSize; i++)
        {
            Vector3 p = Vector3.zero;
            p = transform.right * objOffset + transform.forward * objOffset / 2 * i;

            if (i % 2 == 1)
                p = transform.right * -1f * objOffset + transform.forward * objOffset / 2 * i;

            p += transform.position + spawnOrigin;

            Gizmos.DrawWireCube(p, Vector3.one * objOffset);
        }
    }
}
