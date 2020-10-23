using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroibaleObject : MonoBehaviour
{
    [Header("Количество колб для уничтожения"), Range(0,5)]
    [SerializeField] private int flasksToDestroy = 1;

    [Header("Animator разрушаемого объекта")]
    [SerializeField] private Animator animator;

    [Header("Название триггера")]
    [SerializeField] private string triggerName;

    private int damageDeal = 0;
    private bool isDead = false;

    public bool IsDead => isDead;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flask"))
        {
            damageDeal++;
            if (damageDeal == flasksToDestroy)
            {
                if (animator != null && triggerName != null)
                {
                    animator.SetTrigger(triggerName);

                }
                isDead = true;
            }
                
        }
    }
}
