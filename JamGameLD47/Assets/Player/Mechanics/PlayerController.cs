using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Характеристики персонажа")]
    [SerializeField] private Character_SO stats;

    [Header("Rigidbody персонажа")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Точка для raycast-a в пол")]
    [SerializeField] private Transform raycastPoint;

    [Header("Радиус raycasta")]
    [SerializeField] private float radiusForRaycast = 0.3f;

    [Header("Маска для земли")]
    [SerializeField] private LayerMask mask;

    [Header("Animator персонажа")]
    [SerializeField] private Animator animator;

    [Header("Sprite Rendered, который отображает все спрайты")]
    [SerializeField] private SpriteRenderer rend;

    [Header("Время чека на смерть")]
    [SerializeField] private float checkInterval = 1f;

    [Header("Точка спавна фласки")]
    [SerializeField] private Transform firePoint;

    [Header("Длина raycasta")]
    [SerializeField] private float distance=0.5f;

    public static Action<float> CreateFlask;
    public static Action PlayerDeath;

    public readonly DeathState deathState = new DeathState();
    public readonly MoveState moveState = new MoveState();

    private BaseState currentState;

    private GameObject flask;

    private float direction = 1;

    private Vector3 scale;

    private bool canMove = true;

    private bool isDead = false;

    private float attackRate = 0;


    public bool IsDead => isDead;

    private void Start()
    {
        flask = Resources.Load("Flask", typeof(GameObject)) as GameObject;
        StartCoroutine(CheckPlayer());
        DialogManager.DialogStart += ChangePosibility;

        Enemy.HitPlayer += Death;
        scale = transform.localScale;
        TransitionToState(moveState);
    }

    private void Update()
    {
        if (canMove) currentState.Update(this);
        else if (!isDead) SetAnimation(0);
        if (Input.GetMouseButtonDown(0) && CanAttack()) Attack();
        attackRate -= Time.deltaTime;
    }

    private bool Grounded()
    {
        if (Physics2D.OverlapCircle(raycastPoint.position, radiusForRaycast, mask)) return true;
        else return false;
    }

    private bool CanAttack()
    {
        if (!IsDead && canMove && attackRate < 0)
        {            
            attackRate = stats.AttackSpeed;
            return true;
        }
        else return false;
    }

    IEnumerator CheckPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            if (!rend.isVisible) isDead = true;
        }
        
    }

    private void Attack()
    {
        
        animator.SetTrigger("Attack");
        Instantiate(flask, firePoint.position, firePoint.rotation);
        if (CreateFlask != null) CreateFlask(direction);
        
    }    

    public void SetAnimation( float horizontal)
    {

        if (!Grounded()) animator.SetBool("Jump", true);
        else animator.SetBool("Jump", false);
        if (Grounded()) animator.SetFloat("Horizontal", horizontal);


    }

    public void SetMove( float horizontal)
    {

        if (horizontal != 0) direction = Mathf.Sign(horizontal);

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && Grounded())
            rb.AddForce(Vector2.up * stats.JumpForce, ForceMode2D.Impulse);

        transform.localScale = new Vector3(scale.x * direction, scale.y, scale.z);

        if (!Physics2D.Raycast(transform.position, transform.right * direction, distance, mask)) 
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * horizontal, stats.Speed * Time.deltaTime);
    }

    

    private  void ChangePosibility(bool value)
    {
        SetAnimation(0);
        canMove = !value;
    }

    private void OnDisable()
    {
        DialogManager.DialogStart -= ChangePosibility;
        Enemy.HitPlayer -= Death;
    }


    public void TransitionToState(BaseState state)
    {
        currentState = state;
        currentState.EnterState(this);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death") || collision.CompareTag("Enemy")) Death();
    }
    public void Death()
    {

        animator.SetBool("Death", true);
        canMove = false;
        if (PlayerDeath != null) PlayerDeath();
        StopAllCoroutines();

    }




}
