using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarWheels
{
    [SerializeField] public GameObject ForwardLeft;
    [SerializeField] public GameObject ForwardRight;
    [SerializeField] public GameObject BackLeft;
    [SerializeField] public GameObject BackRight;
    [SerializeField] public GameObject ParentOfForwardLeft;
    [SerializeField] public GameObject ParentOfForwardRight;
}
