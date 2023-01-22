using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActorSO", menuName = "ScriptableObjects/ActorSO", order = 3)]
public class ActorSO : ScriptableObject
{
    public string Name;
    public Vector3 ColliderSize;
    public Vector3 StashPos;
    public int maxCollectable = 5;
    public Vector3 CameraDistance = new Vector3(0, 7f, -10f);
    public float angularTime = 0.025f;
    public ActorType actorType = ActorType.Stickman;
    public float MovementSpeed = 4f;
    public bool specialMovementSystem = false;
}

[System.Serializable]
public enum ActorType {
    Stickman,
    Car,
    Train,
    Plane,
    Yacht
}
