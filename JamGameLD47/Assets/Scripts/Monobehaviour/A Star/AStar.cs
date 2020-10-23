using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    [SerializeField] private float waitTime = 10f;
    void Start()
    {
        StartCoroutine(RecreatePath());
    }

    IEnumerator RecreatePath()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            AstarPath.active.Scan(AstarPath.active.data.gridGraph);
        }
        

    }
}
