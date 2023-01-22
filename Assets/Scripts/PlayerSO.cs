using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player", order = 1)]
public class PlayerSO : ScriptableObject
{
    public string name;
    public int requirementMetal;
}

public class Playable
{
    public string name;
    public int capasity;
}