using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton : MonoBehaviour
{
    [SerializeField] GameObject[] blockade = default;
    [SerializeField] Sprite[] usedLeverSprite = default;

    bool used = false;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used)
        {
            foreach (var block in blockade)
            {
                Destroy(block);
            }
            if (gameObject.name == "Red Button")
            {
                GetComponent<SpriteRenderer>().sprite = usedLeverSprite[0];
            }
            else if (gameObject.name == "Green Button")
            {
                GetComponent<SpriteRenderer>().sprite = usedLeverSprite[1];
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = usedLeverSprite[2];
            }
            used = true;
        }        
    }
}
