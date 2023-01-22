using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathCreator))]
public class TrainPath : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    public bool isClose = true;
    public Transform[] Points;

    private void Awake()
    {
        if (pathCreator == null)
            pathCreator = GetComponent<PathCreator>();
    }

    void Start()
    {
    }

    [ContextMenu("CreateBezierPath")]
    private void CreateBezierPath()
    {
        if (Points.Length < 1)
            return;

        BezierPath bezierPath = new BezierPath(Points, isClose, PathSpace.xyz);

        if(pathCreator == null)
        {
            GetComponent<PathCreator>().bezierPath = bezierPath;
        }
        else
        {
            pathCreator.bezierPath = bezierPath;
        }

    }

}
