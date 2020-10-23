using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent (typeof(CircleCollider2D))]
public class FlaskBehaviour : MonoBehaviour
{
    [Header("Rigidbody объекта")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Скорость полета")]
    [SerializeField] private float speed;

    [Header("Animator объекта")]
    [SerializeField] private Animator animator;

    private float _direction = 1f;

    private void Awake()
    {
        PlayerController.CreateFlask += SetDirection;
    }
    void Start()
    {
        
        rb.velocity = transform.right * speed * _direction;
        PlayerController.CreateFlask -= SetDirection;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        switch (collision.tag)
        {
            case "Wall": Explosion();
                break;
            case "Destroibale Door":  Explosion();
                break;
            case "Closed Door": Explosion();
                break;
            case "Enemy":
                Explosion();
                break;
        }
            
    }

    private void SetDirection(float direction)
    {
        _direction = direction;
    }
    private void Explosion()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        animator.SetTrigger("Explosion");

        Destroy(gameObject, 0.8f);
    }
}
