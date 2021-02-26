using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton : MonoBehaviour
{
    [SerializeField] GameObject[] blockade = default;
    [SerializeField] Sprite usedLeverSprite = default;

    bool used = false;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used)
        {
            foreach (var block in blockade)
            {
                block.GetComponent<Animator>().SetTrigger("fade");
                Destroy(block, 1f);
            }
            GetComponent<SpriteRenderer>().sprite = usedLeverSprite;
            used = true;
        }        
    }
}
