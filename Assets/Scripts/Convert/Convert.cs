using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Convert : MonoBehaviour
{
    [SerializeField] public string ConvertToName = "Stickman";
    [SerializeField] public TextMeshProUGUI Text;
    [SerializeField] public ActorType ConvertToAT = ActorType.Stickman;
    [SerializeField] public Slider slider;
    [SerializeField] public bool deleteAfterConvert = false;
    [SerializeField] public UnityEngine.Events.UnityEvent callback;

    private bool playerStay = false;
    private float timer = 0f;

    private void Start()
    {
        Text.text = "Convert to " + ConvertToName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        if (Player.Instance.CurrentActorType == ConvertToAT)
            return;

        if (other.CompareTag("Player"))
        {
            playerStay = true;
        }
    }

    private void OnTriggerStay()
    {
        if (playerStay == false)
            return;

        timer += Time.fixedDeltaTime;
        float percent = timer / GameManager.Instance.ConvertDuration;
        percent = (percent > 1) ? 1 : percent;
        if (slider != null)
        {
            slider.value = percent;
        }
        if (percent == 1)
        {
            timer = 0;
            Player.Instance.SetModel(ConvertToAT);
            slider.value = 0;
            callback.Invoke();
            if(deleteAfterConvert)
                gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null)
            return;
        if (other.CompareTag("Player"))
        {
            timer = 0;
            slider.value = 0;
            playerStay = false;
        }
    }
}
