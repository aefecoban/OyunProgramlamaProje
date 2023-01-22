using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stash : MonoBehaviour
{
    [Header("Inventory")]
    public int collectedCount = 0;
    public int maxCollectable = 10;
    public float heightSize = 0.35f;
    public bool canCollect => maxCollectable > collectedCount ? true : false;

    public void Increment()
    {
        collectedCount++;
    }

    public int GetMyNumber()
    {
        return collectedCount;
    }

    public void AddItem()
    {
        Increment();
    }

    public Vector3 GetMyPos(int count)
    {
        Vector3 myPos = transform.position;
        myPos += Vector3.up * (heightSize * count);
        return myPos;
    }

    public void Pay(UnlockerArea UA)
    {
        if (collectedCount > 0)
        {
            GameObject last = transform.GetChild(collectedCount - 1).gameObject;
            if(last == null)
                return;

            if(last.TryGetComponent<MetalCollected>(out MetalCollected MC))
            {
                collectedCount--;
                UA.Decrease();
                //animation and text update:
                MC.Pay(UA.transform, (() =>
                {
                    UA.UpdateInfo();
                }));
            }
        }
    }

    public void ClearStash()
    {
        collectedCount = 0;
        for(int i = this.transform.childCount - 1; i > -1; i--)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

}
