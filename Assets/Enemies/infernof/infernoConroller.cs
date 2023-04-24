using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infernoConroller : EnemyHealth
{
    [SerializeField] private LineRenderer lr;

    [SerializeField] private Detector _detector;

    [SerializeField] private float fireDist;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Transform grap;


    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletDestroyTime;
    [SerializeField] private float bulletForce;
    [SerializeField] private float fireRate;

    private float timeToShoot;

    private Vector2 dir;
    private Vector2 dirVelocity;
    private Vector2 shootVelocity;
    private Rigidbody2D rb2d;

    [SerializeField] private bool isLaser;
    [SerializeField] private GameObject laser;

    private Vector2 toP;
    private bool isPk;

    private void Awake()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_detector.isPlayer || !_detector.isCrystal)
        {
            if (isLaser)
            {
                isLaser = false;
                Destroy(laser.gameObject);
            }

            return;
        }

        if (((Vector2.Distance(this.transform.position, GameManager.Singleton.player.position) <= fireDist) && _detector.isPlayer)
            || ((Vector2.Distance(this.transform.position, GameManager.Singleton.crystal.position) <= fireDist) && _detector.isCrystal))
        {

            dirVelocity = new Vector2(0f, 0f);

            if (_detector.isPlayer)
            {
                toP = GameManager.Singleton.player.position;
                isPk = true;
                shootVelocity = GameManager.Singleton.player.position - firePoint.transform.position;
            }
            else
            {
                toP = GameManager.Singleton.crystal.position;
                shootVelocity = GameManager.Singleton.crystal.position - firePoint.transform.position;
                isPk = false;
                
            }

            

            shootVelocity.x = Mathf.Clamp(shootVelocity.x, -1, 1);
            shootVelocity.y = Mathf.Clamp(shootVelocity.y, -1, 1);

            if (!isLaser)
            {
                shoot();
                isLaser = true;
            }

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

            if (isLaser)
            {
                isLaser = false;
                Destroy(laser.gameObject);
            }

            dirVelocity.x = Mathf.Clamp(dir.x, -1, 1);
            dirVelocity.y = Mathf.Clamp(dir.y, -1, 1);

            shootVelocity = dirVelocity;
        }

        if (dirVelocity.x > 0)
        {
            grap.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (dirVelocity.x < 0)
        {
            grap.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    public override void takeDamage(int damage)
    {
        currentHealth -= damage;
        updateHealth();

        if (currentHealth <= 0)
        {
            if (laser != null)
            {
                Destroy(laser.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(dirVelocity.x * speed, dirVelocity.y * speed);
    }

    private void shoot()
    {
        if (Time.time >= timeToShoot)
        {
            isLaser = true;
            timeToShoot = Time.time + fireRate;

            laser = Instantiate(bullet, firePoint.position, this.transform.rotation);
            laser.GetComponent<LineRenderer>().SetPosition(0, firePoint.transform.position);
            laser.GetComponent<LineRenderer>().SetPosition(1, toP);
            laser.GetComponent<flameController>().top = this.gameObject.transform;
            laser.GetComponent<flameController>().isPl = isPk;
        }
    }
}
