using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockerArea : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI ReqText;
    [SerializeField] public UnlockSO Data;
    [SerializeField] GameObject UnlockGameobject;
    [SerializeField] GameObject LockGameobject;
    [SerializeField] GameObject[] NextUnlock;
    [SerializeField] GameObject[] DeleteAfterUnlock;

    public bool needPayment => !Data.unlocked;

    private void Start()
    {
        UpdateInfo();

        if (Data.unlocked)
        {
            Unlocked();
        }
    }

    public void UpdateGameobject()
    {
        if (UnlockGameobject == null && LockGameobject == null)
            return;

        if (Data.unlocked)
        {
            if(UnlockGameobject != null)
                UnlockGameobject.SetActive(true);
            LockGameobject.SetActive(false);
        }
        else
        {
            if (UnlockGameobject != null)
                UnlockGameobject.SetActive(false);
            LockGameobject.SetActive(true);
        }
    }

    public void UpdateInfo()
    {
        ReqText.text = (Data.requirementMetal - Data.collectedMetal).ToString();
        UpdateGameobject();
    }

    public void Decrease()
    {
        if (Data.unlocked)
            return;
        Data.collectedMetal++;
        if (Data.unlocked)
        {
            Unlocked();
        }
    }

    private void Unlocked()
    {
        Player.Instance.SetModel(Data.type);
        UpdateInfo();
        if (NextUnlock != null && NextUnlock.Length > 0)
        {
            foreach (GameObject GO in NextUnlock)
            {
                if (GO != null)
                    GO.SetActive(true);
            }
            foreach (GameObject GO in DeleteAfterUnlock)
            {
                if (GO != null)
                    GO.SetActive(false);
            }
        }   
    }

}
