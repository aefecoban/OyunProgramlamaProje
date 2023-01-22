using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalTween : MonoBehaviour
{

    [Header("Rotate")]
    [SerializeField] public Vector3 RotateVector = Vector3.up;
    [SerializeField] public float speed = 10f;

    [Header("Scale")]
    [SerializeField] public Vector3 scaleVector = Vector3.one;
    [SerializeField] public float scaleDuration = 0.4f;

    private float scaleTimer = 0f;

    private void Start()
    {
        transform.Rotate(RotateVector * Random.RandomRange(0, 100f));
        StartCoroutine("MetalAnim");
    }

    IEnumerator MetalAnim()
    {
        yield return StartCoroutine("ScaleAnimate");
        StartCoroutine("RotateAnimate");
    }

    IEnumerator ScaleAnimate()
    {
        transform.localScale = Vector3.zero;
        for (; scaleTimer < scaleDuration; )
        {
            scaleTimer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, scaleVector, (scaleTimer / scaleDuration));
            yield return null;
        }
        transform.localScale = scaleVector;
    }

    IEnumerator RotateAnimate()
    {
        while (true)
        {
            transform.Rotate(RotateVector * speed);
            yield return null;
        }
    }

}