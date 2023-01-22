using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MetalSpawner : MonoBehaviour
{
    [SerializeField] GameObject SpriteObj;
    [SerializeField] GameObject CollectablePrefab;
    [SerializeField] Vector3 originPos = Vector3.zero;
    [SerializeField] float radius = 4f;

    private int CollectableCount = 0;
    [SerializeField] int maxCollectable = 10;
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] bool waitActorType = false;
    [SerializeField] ActorType actorType = ActorType.Yacht;
    [SerializeField] bool yPosRadius = false;
    [SerializeField][Range(0, 10f)] float objSize = 1f;

    void Start()
    {
        LocateSpriteObj();

        if (Application.isPlaying)
        {
            StartCoroutine("Spawn");
        }
    }

    private void Update()
    {
        #if UNITY_EDITOR
        LocateSpriteObj();
        #endif
    }

    void LocateSpriteObj()
    {
        if (SpriteObj != null)
        {
            SpriteObj.transform.position = new Vector3(
                originPos.x,
                SpriteObj.transform.position.y,
                originPos.z
                );
            SpriteObj.transform.localScale = Vector3.one * radius;
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if(waitActorType && actorType != Player.Instance.CurrentActorType)
            {
                yield return new WaitForSeconds(spawnDelay * 3);
                continue;
            }
            if (maxCollectable > CollectableCount)
            {
                Vector3 pos = FindPos();
                if (yPosRadius)
                {
                    pos.y = Random.RandomRange(originPos.y - radius / 2f, originPos.y + radius / 2f);
                }
                else
                {
                    pos.y = originPos.y + 0.5f;
                }
                GameObject g = Instantiate<GameObject>(CollectablePrefab, pos, Quaternion.identity, this.transform);
                if(objSize != 1)
                {
                    if(g.TryGetComponent(out MetalTween mt))
                    {
                        mt.scaleVector *= objSize;
                    }
                }
            }
            CollectableCount = this.transform.childCount;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    Vector3 FindPos()
    {
        Vector3 p = originPos;
        Vector2 r = (Random.insideUnitCircle * radius / 2f);

        p += new Vector3(r.x, 0, r.y);

        //üst üste gelmeyi engellemek için 2 kere deneme yapabilir.
        for(int i = 0; i < 2; i++)
        {
            Collider[] colliders = Physics.OverlapBox(p, Vector3.one / 2f);
            if(colliders.Length < 1)
            {
                break;
            }
        }

        return p;
    }

    private void OnDrawGizmos()
    {
        if (SpriteObj != null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originPos, radius);

    }

}
