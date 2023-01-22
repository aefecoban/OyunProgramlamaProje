using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPay : MonoBehaviour
{

    [SerializeField] string unlockerTagName = "Unlocker";
    private float timer = 0;
    private bool CanPay => Player.Instance.STASH.collectedCount < 1 ? false : true;

    private void OnTriggerStay(Collider other)
    {
        if (!CanPay)
            return;

        if (other.CompareTag(unlockerTagName))
        {
            if(other.TryGetComponent<UnlockerArea>(out UnlockerArea UA))
            {
                if (!UA.needPayment)
                {
                    timer = 0;
                    return;
                }
                timer += Time.fixedDeltaTime;
                if (timer >= GameManager.Instance.payDuration)
                {
                    timer = 0;
                    PayTo(UA);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(unlockerTagName))
            timer = 0;
    }

    private void PayTo(UnlockerArea UA)
    {
        Player.Instance.STASH.Pay(UA);
    }

}
