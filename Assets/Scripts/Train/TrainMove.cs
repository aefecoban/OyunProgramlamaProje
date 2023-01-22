using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : MonoBehaviour
{
    [SerializeField] public float offset = 0;
    void Update()
    {
        transform.position = TrainSystem.instance.GetMyPos(offset);
        transform.rotation = TrainSystem.instance.GetMyRot(offset);
    }
}
