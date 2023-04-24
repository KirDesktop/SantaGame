using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            collision.transform.GetComponent<EnemyHealth>().takeDamage(damage);

            Destroy(this.gameObject);
        }

        if (collision.CompareTag("boss"))
        {
            collision.transform.GetComponent<BossHealth>().takeDamage(damage);

            Destroy(this.gameObject);
        }
    }
}
