using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWController : MonoBehaviour
{
    [SerializeField] private GameObject damageP;
    [SerializeField] private Detector _detector;
    [SerializeField] private Detector _bodyDetector;

    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Transform grap;

    private Vector2 dir;
    private Vector2 dirVelocity;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_detector.isPlayer)
        {
            dir = GameManager.Singleton.player.position - this.transform.position;
        }
        else
        {
            dir = GameManager.Singleton.crystal.position - this.transform.position;
        }

        if (dir.x > 0)
        {
            dirVelocity.x = 1f;
            grap.transform.rotation = Quaternion.Euler(0f,180f,0f);
        }
        else if(dir.x == 0)
        {
            dirVelocity.x = 0f;
        }
        else
        {
            dirVelocity.x = -1f;
            grap.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (_bodyDetector.isPlayer)
        {
            GameManager.Singleton.player.GetComponent<PlayerHealth>().takeDamage(damage);
            Instantiate(damageP, this.transform);
        }
        if (_bodyDetector.isCrystal)
        {
            GameManager.Singleton.crystalc.takeDamage(damage);
            Instantiate(damageP, this.transform);
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(dirVelocity.x * speed, rb2d.velocity.y);
    }

}
