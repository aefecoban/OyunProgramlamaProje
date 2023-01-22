using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] string CollectableTagName = "Collectable";

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        if (!Player.Instance.canCollect)
            return;

        if (other.transform.CompareTag(CollectableTagName))
        {
            if(other.TryGetComponent<Metal>(out Metal metalCollectable))
            {
                metalCollectable.Collect();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == null)
            return;
        if (!Player.Instance.canCollect)
            return;

        if (other.transform.CompareTag(CollectableTagName))
        {
            if (other.TryGetComponent<TrainStop>(out TrainStop tStop))
            {
                tStop.Collect();
            }
        }
    }

}
