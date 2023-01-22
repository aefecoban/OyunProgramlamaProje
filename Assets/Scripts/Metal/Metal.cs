using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{

    public void Collect()
    {
        GameObject collectedMetal = Instantiate<GameObject>(GameManager.Instance.collectedMetal, null);
        collectedMetal.transform.position = transform.position;
        collectedMetal.transform.localScale = transform.localScale;
        int count = Player.Instance.STASH.GetMyNumber();
        collectedMetal.GetComponent<MetalCollected>().TrackTarget(count);
        Destroy(this.gameObject);
    }

}
