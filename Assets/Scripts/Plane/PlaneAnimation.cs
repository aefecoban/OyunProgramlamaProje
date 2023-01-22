using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnimation : MonoBehaviour
{
    void Update()
    {
        PlaneSystem.Instance.propeller.transform.Rotate(new Vector3(0f, 0f, PlaneSystem.Instance.planeMovementSystem.currentSpeed * 30f * Time.deltaTime));
    }
}
