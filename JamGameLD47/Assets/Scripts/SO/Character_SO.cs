using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Create Character", order = 1)]
public class Character_SO : ScriptableObject
{
    [Header("Скорость бега"), Range(0,20)]
    [SerializeField] private float speed;

    [Header("Скорость атаки"), Range (0,20)]
    [SerializeField] private float attackRate;

    [Header("Сила прыжка"), Range(0,20)]
    [SerializeField] private float jumpForce;

    public float Speed => speed;

    public float AttackSpeed => attackRate;

    public float JumpForce => jumpForce;
}
