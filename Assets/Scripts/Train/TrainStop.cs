using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainStop : MonoBehaviour
{

    float collectTimer = 0;

    public void Collect()
    {
        if (!TrainSystem.instance.isStop)
            return;
        if (!TrainStash.instance.canCollect)
            return;

        if(collectTimer == 0)
        {
            collectTimer = GameManager.Instance.CollectAnimDuration;
            GameObject go = TrainStash.instance.transform.GetChild(TrainStash.instance.transform.childCount - 1).gameObject;
            if (go == null)
                return;

            if(go.TryGetComponent<Metal>(out Metal m))
            {
                m.Collect();
            }
        }
        else
        {
            collectTimer -= Time.fixedDeltaTime;
            collectTimer = (collectTimer < 0) ? 0 : collectTimer;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            collectTimer = 0;
            TrainSystem.instance.MoveStart();
        }
    }

}
