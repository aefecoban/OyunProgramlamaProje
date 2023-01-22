using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player Instance = null;
    public Vector3 MovementVector = Vector3.zero;
    public float MovementSpeed = 4f;
    public Vector3 Forward = Vector3.forward;
    public Vector3 LastForward = Vector3.forward;
    public float angularTime = 0.025f;

    [Space]
    [Header("Models")]
    private const string SELECTED_MODEL = "SMODEL:";

    [SerializeField] public int selectedModel
    {
        get => PlayerPrefs.GetInt(SELECTED_MODEL, 0);
        set => PlayerPrefs.SetInt(SELECTED_MODEL, value);
    }

    [SerializeField] public GameObject[] models = new GameObject[0];

    [SerializeField] private GameObject StashObject;
    public PlayerAnimationSystem PAS;
    public Stash STASH;
    public BoxCollider collider;
    public PlayerMove moveSystem;
    public Actor CurrentActor => Player.Instance.models[Player.Instance.selectedModel].GetComponent<Actor>();
    public ActorType CurrentActorType => CurrentActor.actor.actorType;

    public bool canCollect => (STASH != null) ? STASH.canCollect : false;

    private void Awake()
    {
        if(Player.Instance == null)
        {
            Instance = this;
            PAS = this.GetComponent<PlayerAnimationSystem>();
            STASH = this.StashObject.GetComponent<Stash>() ?? null;
            collider = this.GetComponent<BoxCollider>();
            moveSystem = this.GetComponent<PlayerMove>();
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        Forward = Vector3.forward;
        Prepare();
    }

    public void Prepare()
    {
        PrepareModels();
        PrepareSystems();
        PrepareAttriutes();
        PrepareUI();
    }

    public void SetForward(Vector3 f)
    {
        Player.Instance.LastForward = Player.Instance.Forward;
        Player.Instance.Forward = f;
        this.gameObject.transform.forward = f;
    }

    public void SetModel(ActorType aType)
    {
        int i = 0;
        int selected = selectedModel;
        foreach(var model in Player.Instance.models)
        {
            if(model.GetComponent<Actor>().actor.actorType == aType)
            {
                selected = i;
                break;
            }
            i++;
        }

        if ( CurrentActorType == ActorType.Yacht || aType == ActorType.Yacht )
        {
            if(TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
            {
                agent.Warp(GameManager.Instance.GroundToSeaSpawnPoint);
            }
        }

        GameManager.Instance.PlayerConverted(aType);
        STASH.ClearStash();
        SetModel(selected);
    }

    public void SetModel(int m)
    {
        selectedModel = m;
        Prepare();
    }

    public void PrepareModels()
    {
        int i = 0;
        foreach(var model in models)
        {
            model.SetActive(
                (i != selectedModel) ? false : true
            );
            i++;
        }

    }

    public void PrepareSystems()
    {
        NavMeshAgent agent = null;
        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent a))
        {
            agent = a;
        }
        if (CurrentActor.actor.specialMovementSystem)
        {
            Player.Instance.moveSystem.dontMove();
            if(agent != null)
                agent.enabled = false;
        }
        else
        {
            Player.Instance.moveSystem.youCanMove();
            if (agent != null)
                agent.enabled = true;
        }
    }

    public void PrepareAttriutes()
    {
        PAS.PrepareAnimation();
        collider.size = CurrentActor.actor.ColliderSize;
        STASH.transform.localPosition = CurrentActor.actor.StashPos;
        STASH.maxCollectable = CurrentActor.actor.maxCollectable;
        angularTime = CurrentActor.actor.angularTime;
        MovementSpeed = CurrentActor.actor.MovementSpeed;
    }

    public void PrepareUI()
    {
        GameManager.Instance.UIModelExitButton.SetActive(
            CurrentActorType != ActorType.Stickman ? true : false
        );
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if (Player.Instance == null)
        {
            Instance = this;
            PAS = this.GetComponent<PlayerAnimationSystem>();
            STASH = this.StashObject.GetComponent<Stash>() ?? null;
            collider = this.GetComponent<BoxCollider>();
            moveSystem = this.GetComponent<PlayerMove>();
        }
        #endif
    }

}
