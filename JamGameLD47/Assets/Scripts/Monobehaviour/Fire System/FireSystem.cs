using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSystem : MonoBehaviour
{
    [Header("Animator для воды")]
    [SerializeField] private Animator water;

    [Header("Animator для огня")]
    [SerializeField] private Animator fire;

    [Header("Animator для тумана")]
    [SerializeField] private Animator smoke;

    [Header("Collider")]
    [SerializeField] private BoxCollider2D col;

    private void Start()
    {
        water.enabled = false;
        smoke.enabled = false;
    }
    [ContextMenu("Fire")]
    public void StopFire()
    {
        col.enabled = false;
        fire.SetTrigger("Fire");
        smoke.enabled = true;
        water.enabled = true;
    }
}
