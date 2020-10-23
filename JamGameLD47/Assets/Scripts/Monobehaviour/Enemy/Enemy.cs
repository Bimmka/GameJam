using Pathfinding;
using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Характеристики врага")]
    [SerializeField] private Character_SO enemy;

    [Header("Прикрипленный AIPath")]
    [SerializeField] private AIPath aiPath;

    [Header("Прикрипленный AI Destination Setter")]
    [SerializeField] private AIDestinationSetter setter;

    [Header("Зона агра")]
    [SerializeField] private float agroRadius;

    [Header("Дистанция атаки")]
    [SerializeField] private float attackRange = 1f;

    [Header("Зона патрулированя")]
    [SerializeField] private Transform[] patrulPoints;

    [Header("Частота поиска цели")]
    [SerializeField] private float interval = 3;

    [Header("Маска для поиска")]
    [SerializeField] private LayerMask mask;

    [SerializeField] private DestroibaleObject objectEnemy;

    public static Action HitPlayer;

    private Collider2D target = null;

    private int patrulPointIndex = 0;

    private Vector3 scale;


    void Start()
    {
        scale = transform.localScale;
        setter.target = patrulPoints[patrulPointIndex];
        aiPath.maxSpeed = enemy.Speed;
        StartCoroutine(SearchTarger());
    }

    private void Update()
    {
        if (!DialogManager.instance.GetDialogSrart())
        {
            aiPath.maxSpeed = enemy.Speed;
            if (objectEnemy.IsDead) Destroy(gameObject);
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
            }
            else if (aiPath.desiredVelocity.x <= 0.01f)
            {
                transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            }


            if (target != null) ForwardForPlayer();
            else Patruling();
        }
        else
        {
            aiPath.maxSpeed = 0;
        }
        
        
    }

    private void ForwardForPlayer()
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        if (Mathf.Abs(distance) > agroRadius)
        {
            target = null;
            aiPath.maxSpeed = enemy.Speed;
            StartCoroutine(SearchTarger());
        }
        else if (Mathf.Abs(distance) < attackRange)
        {
            if (HitPlayer != null) HitPlayer();
        }
    }

    private void Patruling()
    {
        if (Vector2.Distance(transform.position, patrulPoints[patrulPointIndex].position) < 3f)
        {
            patrulPointIndex++;
            patrulPointIndex %= 2;
            setter.target = patrulPoints[patrulPointIndex];
        }
    }
    IEnumerator SearchTarger()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            target = Physics2D.OverlapCircle(transform.position, agroRadius, mask);
            if (target != null)
            {
                aiPath.maxSpeed *= 1.4f;
                
                setter.target = target.transform;
                break;
            }
        }
    }
}
