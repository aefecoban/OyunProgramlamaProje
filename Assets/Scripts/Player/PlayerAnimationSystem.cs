using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSystem : MonoBehaviour
{
    [Header("General")]
    public float animSpeed;
    private float carWheelsAngel = 0f;
    private float max = 0;
    private float min = 0;

    [Header("Stickman")]
    [SerializeField] Animator animator;

    [Header("Car")]
    [SerializeField] CarWheels carWheels;
    Vector3 wheelsForward = Vector3.right;


    public void PrepareAnimation()
    {
        animator = Player.Instance.models[Player.Instance.selectedModel].GetComponent<Animator>();
    }

    public void SetAnimSpeed(float s)
    {
        // 0.001 = walk anim min val
        animSpeed = (s > 0 ? Mathf.Max(0.001f, s) : 0);
        UpdateAnimation();
    }

    public void SetTurnAngel(float angel)
    {
        carWheelsAngel = (angel < 0 ? (angel > -0.01) ? 0 : angel : (angel < 0.01) ? 0 : angel);
    }

    private void UpdateAnimation()
    {
        if(animator != null)
            animator.SetFloat("Movement", animSpeed);
    }

    void Update()
    {
        if (Player.Instance.CurrentActorType == ActorType.Car)
        {
            CarAnimUpdate();
        }
    }

    void CarAnimUpdate()
    {
        Vector3 forwardVector = wheelsForward * animSpeed * 10;

        #region WheelsForward
        carWheels.ForwardLeft.transform.Rotate(forwardVector, Space.Self);
        carWheels.BackLeft.transform.Rotate(forwardVector, Space.Self);
        carWheels.ForwardRight.transform.Rotate(forwardVector, Space.Self);
        carWheels.BackRight.transform.Rotate(forwardVector, Space.Self);
        #endregion

        #region WheelsTurn
        carWheels.ForwardLeft.transform.parent.localEulerAngles = new Vector3(0, carWheelsAngel, 0);
        carWheels.ForwardRight.transform.parent.localEulerAngles = new Vector3(0, carWheelsAngel, 0);
        #endregion
    }

}
