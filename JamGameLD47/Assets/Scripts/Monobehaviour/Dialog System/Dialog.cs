using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [TextArea(3,20)]
    [SerializeField] private string[] text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var parametr = collision.GetComponent<PlayerController>();
        if (parametr != null)
        {
            DialogManager.instance.SetText(text);
            Destroy(gameObject, 1f);
        }
        
    }
}
