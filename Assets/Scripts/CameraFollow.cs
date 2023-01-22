using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] GameObject Target;
    [SerializeField] Vector3 LookDelta = Vector3.zero;
    [SerializeField] Vector3 PositionDelta = new Vector3(0, 7, -10);

    void LateUpdate()
    {
        PositionDelta = Player.Instance.CurrentActor.actor.CameraDistance;
        this.transform.position = Target.transform.position + PositionDelta;
        this.transform.LookAt(Target.transform.position + LookDelta);
    }
}
