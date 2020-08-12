using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton : MonoBehaviour
{
    [SerializeField] GameObject blockade = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(blockade);
        Destroy(gameObject);
    }
}
