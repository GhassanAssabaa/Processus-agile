using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BreakableBlock : MonoBehaviour
{
    [Range(1, 3)]
    [SerializeField] private int hitsNeededBeforeBreak = 1;
    private int totalHits = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            totalHits++;
            if (totalHits == hitsNeededBeforeBreak)
            {
                //Debug.Log("destroyed");
                FindObjectOfType<GameManager>().CountBlocks();
                Destroy(this.gameObject);
            }
        }
    }
}
