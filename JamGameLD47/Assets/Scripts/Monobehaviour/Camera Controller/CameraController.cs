using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Точки, из которых пускается raycast")]
    [SerializeField] private Transform[] raycastPoints;

    [Header("Радиус raycast")]
    [SerializeField] private float radius;

    [Header("Нормальная скорость движения камеры")]
    [SerializeField] private float speed;

    [Header("Ускоренная скорость движения камеры")]
    [SerializeField] private float speedUp;

    [Header("Маска на персонажа")]
    [SerializeField] private LayerMask mask;

    

    private bool canMove = false;

    private float raycastInterval = 0.3f;

    private float moveSpeed;

    private bool checkDialog = false;


    private void Awake()
    {
        ScenesManager.MoveCamera += SetCanMove;
    }
    private void Start()
    {
        
        moveSpeed = speed;
        StartCoroutine(CheckPlayer());
        StartCoroutine(StartMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (checkDialog ) CheckDialog();

        if (canMove && transform.position.x < 127.5f) transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    private void CheckDialog()
    {
        canMove = !DialogManager.instance.GetDialogSrart();
    }
    private void SetCanMove (bool value)
    {
        canMove = value;
    }
    IEnumerator CheckPlayer()
    {
        while(true)
        {
            foreach (var point in raycastPoints)
            {
                if (Physics2D.OverlapCircle(point.position, radius, mask))
                {
                    moveSpeed = speedUp;
                    break;
                }
                else moveSpeed = speed;
            }
            yield return new WaitForSeconds(raycastInterval);
        }
        
        
    }
    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(5);
        canMove = true;
        checkDialog = true;
    }
    private void OnDisable()
    {
        ScenesManager.MoveCamera -= SetCanMove;
    }


}
