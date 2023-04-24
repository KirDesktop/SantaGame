using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ffController : MonoBehaviour
{
    [SerializeField] private Detector _detector;

    [SerializeField] private float fireDist;

    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Transform grap;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletDestroyTime;
    [SerializeField] private float bulletForce;
    [SerializeField] private float fireRate;

    private float timeToShoot;

    private Vector2 dir;
    private Vector2 dirVelocity;
    private Vector2 shootVelocity;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(((Vector2.Distance(this.transform.position, GameManager.Singleton.player.position) <= fireDist) && _detector.isPlayer)
            || ((Vector2.Distance(this.transform.position, GameManager.Singleton.crystal.position) <= fireDist) && !_detector.isPlayer))

        {
            dirVelocity = new Vector2(0f, 0f);

            if (_detector.isPlayer)
            {
                shootVelocity = GameManager.Singleton.player.position - firePoint.transform.position;
            }
            else
            {
                shootVelocity = GameManager.Singleton.crystal.position - firePoint.transform.position;
            }

            shootVelocity.x = Mathf.Clamp(shootVelocity.x, -1, 1);
            shootVelocity.y = Mathf.Clamp(shootVelocity.y, -1, 1);

            shoot();
        }
        else
        {
            if (_detector.isPlayer)
            {
                dir = GameManager.Singleton.player.position - this.transform.position;
            }
            else
            {
                dir = GameManager.Singleton.crystal.position - this.transform.position;
            }

            dirVelocity.x = Mathf.Clamp(dir.x, -1, 1);
            dirVelocity.y = Mathf.Clamp(dir.y, -1, 1);

            shootVelocity = dirVelocity;
        }

        if(dirVelocity.x > 0)
        {
            grap.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if(dirVelocity.x < 0)
        {
            grap.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void shoot()
    {
        if(Time.time >= timeToShoot)
        {
            timeToShoot = Time.time + fireRate;

            GameObject bul = Instantiate(bullet, firePoint.position, this.transform.rotation);
            //bul.transform.up = dirVelocity;
            bul.GetComponent<Rigidbody2D>().AddForce(shootVelocity * bulletForce, ForceMode2D.Impulse);
            Destroy(bul, bulletDestroyTime);
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(dirVelocity.x * speed, dirVelocity.y * speed);
    }
}