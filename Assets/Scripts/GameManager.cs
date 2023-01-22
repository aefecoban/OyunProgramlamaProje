using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    [SerializeField] GameObject dynamicJoystick;
    [SerializeField] public GameObject uncollectedMetal;
    [SerializeField] public GameObject collectedMetal;

    [Header("Plane Settings")]
    [SerializeField] public float planeMultiplerForVecX = 50f;
    [SerializeField] public float planeMultiplerForVecY = 90f;

    [Header("Animation Settings")]
    [SerializeField] public float CollectAnimDuration = 0.5f;
    [SerializeField] public float payDuration = 0.1f;

    [Header("Actor Settings")]
    [SerializeField] public float ConvertDuration = 1f;

    [Header("Spawn Settings")]
    [SerializeField] public Vector3 SeaToGroundSpawnPoint = new Vector3(38f, 0f, 0f);
    [SerializeField] public Vector3 GroundToSeaSpawnPoint = new Vector3(53f, 0f, 0f);

    [Header("UI")]
    [SerializeField] public GameObject UIModelExitButton;

    [Header("Unlock Areas")]
    [Tooltip("Ýlk liste elemaný oyun baþýnda aktifleþir.")]
    [SerializeField] GameObject[] unlockAreas;

    [Header("Train System Area")]
    [SerializeField] GameObject[] trainsSystemLockGO;

    [Header("Converter")]
    [SerializeField] public GameObject CarConverterArea;

    DynamicJoystick dj = null;


    public const string SELECTEDMODEL = "SELECTEDMODEL:";

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        //Lock
        for (int i = 1; i < unlockAreas.Length; i++)
            unlockAreas[i].SetActive(false);
        if(unlockAreas[0] != null)
            unlockAreas[0].SetActive(true);
        for(int i = 0; i < trainsSystemLockGO.Length; i++)
            trainsSystemLockGO[i].SetActive(false);

    }

    void Start()
    {
        if (dynamicJoystick != null)
        {
            dj = dynamicJoystick.GetComponent<DynamicJoystick>();
            PrepareJoystick();
        }

        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if(Player.Instance != null)
        {
            Player.Instance.MovementVector = new Vector3(dj.Direction.x, 0, dj.Direction.y);
        }

        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.DeleteAll();
            Player.Instance.SetModel(Player.Instance.selectedModel);
        }

        if (GameManager.Instance == null)
        {
            Instance = this;
        }
        #endif
    }

    void PrepareJoystick()
    {
        if(dj != null)
        {
            dj.AxisOptions = AxisOptions.Both;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(GroundToSeaSpawnPoint, Vector3.one);
        Gizmos.DrawCube(SeaToGroundSpawnPoint, Vector3.one);
    }

    public void ExitToStickman()
    {
        Vector3 pos = Player.Instance.transform.position;
        Vector3 forward = Player.Instance.transform.forward;
        bool warp = false;
        if (
            Player.Instance.CurrentActorType == ActorType.Yacht
            ||
            Player.Instance.CurrentActorType == ActorType.Plane
        )
        {
            warp = true;
            pos = GameManager.Instance.SeaToGroundSpawnPoint;
        }

        Player.Instance.SetModel(ActorType.Stickman);
        if (CarConverterArea == null)
            return;

        CarConverterArea.SetActive(true);
        CarConverterArea.transform.position = new Vector3(pos.x, 0.0015f, pos.z);
        if (warp)
        {
            Player.Instance.moveSystem.Warp(pos);
        }
        Player.Instance.transform.position += forward * 4f;
    }

    public void PlayerConverted(ActorType aType)
    {
        if(aType == ActorType.Car)
        {
            CarConverterArea.SetActive(false);
        }
    }

}