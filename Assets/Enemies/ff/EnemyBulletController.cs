using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] private GameObject fireP;
    [SerializeField] private int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerHealth>().takeDamage(damage);

            if (fireP != null)
            {
                Instantiate(fireP, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
        if (collision.CompareTag("crystal"))
        {
            //CrystalController.Singleton.takeDamage(damage);
            GameManager.Singleton.crystalc.takeDamage(damage);

            if (fireP != null)
            {
                Instantiate(fireP, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
    }
}
