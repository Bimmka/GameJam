using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCalm : MonoBehaviour
{
    [SerializeField] private FireSystem fire;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fire.StopFire();
            Destroy(gameObject);
        }
    }
}
