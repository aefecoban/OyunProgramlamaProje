using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSystem : MonoBehaviour
{
    public static PlaneSystem Instance;
    [SerializeField] public GameObject propeller;
    [SerializeField] public PlaneMovement planeMovementSystem;
    [SerializeField] public Vector3 minXYZ = new Vector3(-60f, 25f, -60f);
    [SerializeField] public Vector3 maxXYZ = new Vector3(160f, 100f, 160f);

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}