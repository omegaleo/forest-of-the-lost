using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && Player.instance.isLanternOn)
        {
            collision.GetComponent<EnemyAI>().Dead();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && Player.instance.isLanternOn)
        {
            collision.GetComponent<EnemyAI>().Dead();
        }
    }
}
