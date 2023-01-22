using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCollected : MonoBehaviour
{
    private float trackDuration = 1f;
    private float trackTimer = 0f;
    private int myCount = 0;

    private float payDuration = 0.1f;
    private Transform payTarget = null;
    private Action payCallback = null;

    private void Start()
    {
        trackDuration = GameManager.Instance.CollectAnimDuration;
        payDuration = GameManager.Instance.payDuration;
    }

    public void TrackTarget(int _myCount)
    {
        myCount = _myCount;
        Player.Instance.STASH.AddItem();
        StartCoroutine("TrackPlayer");
    }

    private IEnumerator TrackPlayer()
    {
        Vector3 myStartPos = this.transform.position;
        Vector3 myScale = this.transform.localScale;
        Vector3 targetPos = Player.Instance.STASH.GetMyPos(myCount);
        trackTimer = 0;

        while (trackTimer < trackDuration)
        {
            targetPos = Player.Instance.STASH.GetMyPos(myCount);
            Vector3 midPos = Vector3.Lerp(this.transform.position, targetPos, 0.5f) + Vector3.up * 1.5f;
            transform.position = MathHelper.quadraticBezierCurve(myStartPos, midPos, targetPos, trackTimer / trackDuration);
            transform.localScale = Vector3.Lerp(myScale, Vector3.one, trackTimer / trackDuration);
            trackTimer += Time.deltaTime;
            yield return null;
        }

        transform.position = Player.Instance.STASH.GetMyPos(myCount);
        transform.localScale = Vector3.one;
        transform.parent = Player.Instance.STASH.transform;
        transform.localRotation = Quaternion.EulerAngles(new Vector3(0, 0, 0));
    }
    
    private Vector3 calculatePos(Vector3 myPos, Vector3 targetPos, float normalizedTime)
    {
        if(normalizedTime < 0.5)
        {
            targetPos += Vector3.up * 1.75f;
        }
        return Vector3.Lerp(
               myPos,
               targetPos,
               normalizedTime
        );
    }

    public void Pay(Transform target, Action callback)
    {
        payTarget = target;
        payCallback = callback;
        StartCoroutine("Paying");
    }

    IEnumerator Paying()
    {
        float timer = 0f;
        this.transform.parent = null;
        Vector3 myStartPos = this.transform.position;
        Vector3 targetPos = payTarget.transform.position;
        while (timer <= payDuration)
        {
            Vector3 midPos = Vector3.Lerp(myStartPos, targetPos, 0.33f) + Vector3.up * 3.5f;
            this.transform.position = MathHelper.quadraticBezierCurve(myStartPos, midPos, targetPos, timer / payDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        if(payCallback != null)
            payCallback();

        yield return null;

        Destroy(this.gameObject);
    }

}
