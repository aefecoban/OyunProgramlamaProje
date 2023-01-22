using UnityEngine;

[CreateAssetMenu(fileName = "Unlocker", menuName = "ScriptableObjects/Unlocker", order = 1)]
public class UnlockSO : ScriptableObject
{
    private const string UNLOCKER_NAME = "UNLOCKER:";
    public string name;
    public int requirementMetal;
    public ActorType type = ActorType.Stickman;
    public bool unlocked => (requirementMetal > collectedMetal) ? false : true;
    public int collectedMetal
    {
        get => PlayerPrefs.GetInt(UNLOCKER_NAME + name, 0);
        set => PlayerPrefs.SetInt(UNLOCKER_NAME + name, value);
    }
}
