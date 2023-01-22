using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TrainSystem : MonoBehaviour
{
    public static TrainSystem instance;
    [SerializeField] public float speed = 0.035f;
    [SerializeField] bool forceStop = false;
    public PathCreator pathCreator;
    public EndOfPathInstruction EOPInstruction;
    public float startTime = 0;
    public bool isStop = false;

    private float timer = 0;
    private float _speed = 0.05f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        timer = startTime;
    }

    private void Update()
    {
        if (instance == null)
            instance = this;

        float oTimer = timer;
        timer += _speed * Time.deltaTime;

        if(TrainSystem.instance.timer > 1f && TrainSystem.instance.getNormalizedTimer() >= startTime && !isStop)
        {
            MoveStop();
        }

    }

    public float getNormalizedTimer()
    {
        return timer % 1;
    }
    
    public void MoveStop()
    {
        _speed = 0;
        isStop = true;
    }

    public void MoveStart()
    {
        _speed = speed;
        timer = startTime;
        isStop = false;
    }

    public Vector3 GetMyPos(float offset = 0)
    {
        if (pathCreator == null)
            return Vector3.zero;
        return pathCreator.path.GetPointAtTime(timer + offset, EOPInstruction);
    }

    public Quaternion GetMyRot(float offset = 0)
    {
        if (pathCreator == null)
            return Quaternion.identity;
        return pathCreator.path.GetRotation(timer + offset, EOPInstruction);
    }
}
