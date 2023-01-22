using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{

    NavMeshAgent agent;
    bool canIMove = false; //özel hareket sistemi olan uçak gibi araçlar için

    public void dontMove()
    {
        canIMove = false;
    }
    public void youCanMove()
    {
        canIMove = true;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Warp(Vector3 pos)
    {
        agent.Warp(pos);
    }

    void Update()
    {
        if (!canIMove)
            return;

        Vector3 mv = Player.Instance.MovementVector;
        if (mv.magnitude > 1)
        {
            mv = mv.normalized;
        }
        if (mv.magnitude > 0)
        {
            Vector3 direction = (mv * Player.Instance.MovementSpeed);
            direction = Vector3.Lerp(Vector3.zero, direction, Time.deltaTime);
            direction.y = 0;
            agent.Move(direction);

            Player.Instance.SetForward(
                Vector3.Lerp(Player.Instance.Forward, mv.x * Vector3.right + mv.z * Vector3.forward, Time.deltaTime / Player.Instance.angularTime).normalized
            );
            float angel = Vector3.Angle(Player.Instance.LastForward, Player.Instance.Forward);
            Vector3 cross = Vector3.Cross(Player.Instance.LastForward, Player.Instance.Forward);
            angel = (cross.y < 0) ? -angel : angel;
            angel = angel * 10;
            angel = Mathf.Clamp(angel, -30f, 30f);
            Player.Instance.PAS.SetTurnAngel(angel);
        }
        Player.Instance.PAS.SetAnimSpeed(mv.magnitude);
    }
}
