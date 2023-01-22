using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField][Range(1f, 60f)] private float maximumY = 60f;
    private float multiplerForVecX = 40f;
    private float multiplerForVecY = 40f;
    private float speed = 10f;

    private Vector3 previousPos;

    public float currentSpeed = 0;

    private void Start()
    {
        previousPos = this.transform.position;
        speed = Player.Instance.CurrentActor.actor.MovementSpeed;
        multiplerForVecX = GameManager.Instance.planeMultiplerForVecX;
        multiplerForVecY = GameManager.Instance.planeMultiplerForVecY;
    }

    void Update()
    {
        RotationForMovement();
        Move(Player.Instance.transform.position, Player.Instance.transform.forward, speed);
    }

    void RotationForMovement()
    {
        Vector3 mov = Player.Instance.MovementVector;
        Vector3 eulerAngles = Player.Instance.transform.eulerAngles;
        mov.y = 0;
        eulerAngles += new Vector3(
            mov.z * -1f * multiplerForVecX * Time.deltaTime,
            mov.x * multiplerForVecY * Time.deltaTime,
            0f
        );

        if (mov.z == 0f)
        {
            eulerAngles = new Vector3(Mathf.LerpAngle(eulerAngles.x, 0f, Time.deltaTime * (speed / 20)),
                eulerAngles.y,
                eulerAngles.z);
        }

        Player.Instance.transform.eulerAngles = new Vector3(
            MathHelper.ClampAngle(eulerAngles.x, -1 * maximumY, maximumY), eulerAngles.y, eulerAngles.z);
    }

    void Move(Vector3 currentPos, Vector3 dir, float speed)
    {
        Vector3 endPos = currentPos + dir * speed;
        previousPos = Player.Instance.transform.position;
        Vector3 newPos = Vector3.Lerp(currentPos, endPos, Time.deltaTime);
        Vector3 checkX = new Vector2(PlaneSystem.Instance.minXYZ.x, PlaneSystem.Instance.maxXYZ.x);
        Vector3 checkY = new Vector2(PlaneSystem.Instance.minXYZ.y, PlaneSystem.Instance.maxXYZ.y);
        Vector3 checkZ = new Vector2(PlaneSystem.Instance.minXYZ.z, PlaneSystem.Instance.maxXYZ.z);
        newPos.x = (checkX.x > newPos.x ? checkX.x : checkX.y < newPos.x ? checkX.y : newPos.x);
        newPos.y = (checkY.x > newPos.y ? checkY.x : checkY.y < newPos.y ? checkY.y : newPos.y);
        newPos.z = (checkZ.x > newPos.z ? checkZ.x : checkZ.y < newPos.z ? checkZ.y : newPos.z);
        Player.Instance.transform.position = newPos;

        currentSpeed = GetCurrentSpeed();
    }

    float GetCurrentSpeed()
    {
        return ((Player.Instance.transform.position - previousPos) / Time.deltaTime).magnitude;
    }
}