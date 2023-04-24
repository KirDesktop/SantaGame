using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniBosController : MonoBehaviour
{
    [SerializeField] private int bodyDamage;
    [SerializeField] private Transform gunTop;
    [SerializeField] private Transform bulletSpawn;

    [Header("Phase 1")]
    [SerializeField] private List<Transform> points1;
    [SerializeField] private Transform pointsTop;
    [SerializeField] private float speed1;
    [SerializeField] private float minDist;
    [SerializeField]private int curpid;
    [SerializeField]private bool isNew = true;
    [SerializeField] private float bulletSpread;

    [Header("Phase 2")]
    [SerializeField] private List<Transform> points2;
    [SerializeField] private GameObject bullet1;
    [SerializeField] private float fireRate1;
    [SerializeField] private float speed2;
    [SerializeField] private float bulletForce1;
    [SerializeField] private float bulletDestroyTime1;
    [SerializeField]private float timeToShoot;

    [Header("Phase 3")]
    [SerializeField] private GameObject bullet2;
    [SerializeField] private float fireRate2;
    [SerializeField] private float bulletForce2;
    [SerializeField] private float bulletDestroyTime2;


    [SerializeField]private float timeToNextPhase;

    [SerializeField] private int phase = 0;

    [SerializeField]private Vector2 dir;
    [SerializeField] private List<float> timeToPhases;

    //[SerializeField] private bool isPlayer = false;
    // [SerializeField] private bool isCrystal = false;
    [SerializeField] private Detector det;

    private Vector2 toPos;

    private void Update()
    {
        pointsTop.transform.position = GameManager.Singleton.player.position;

        dir = (GameManager.Singleton.player.position - this.transform.position).normalized;
        gunTop.up = dir;

        if (phase == 0)
        {
            phase = Random.Range(1,4);
            isNew = true;
            timeToNextPhase = Time.time + timeToPhases[phase-1];
        }

        if (phase == 1)
        {
            if (isNew)
            {
                curpid = Random.Range(0, points1.Count);
                toPos = points1[curpid].position;
                isNew = false;
            }

            //this.transform.position = Vector2.Lerp(this.transform.position, points1[curpid].position, lerpStep);
            this.transform.Translate((toPos - (Vector2)this.transform.position) * Time.deltaTime * speed1);
            if (Vector2.Distance(this.transform.position, toPos) <= minDist)
            {
                isNew = true;
            }
        }

        if (phase == 2)
        {
            if (isNew)
            {
                curpid = Random.Range(0, points2.Count);
                toPos = points2[curpid].position;
                isNew = false;
            }

            //this.transform.position = Vector2.Lerp(this.transform.position, points2[curpid].position, lerpStep);

            this.transform.Translate((toPos-(Vector2)this.transform.position) * Time.deltaTime * speed2);
            if (Vector2.Distance(this.transform.position, toPos) <= minDist)
            {
                isNew = true;
                //Debug.Log("new1");
            }

            shoot(bullet1, bulletForce1, bulletDestroyTime1, fireRate1);
        }
        if (phase == 3)
        {
            if (isNew)
            {
                curpid = Random.Range(0, points2.Count);
                toPos = points2[curpid].position;
                isNew = false;
            }

            this.transform.Translate((toPos - (Vector2)this.transform.position) * Time.deltaTime * speed2);
            if (Vector2.Distance(this.transform.position, toPos) <= minDist)
            {
                //Debug.Log("new2");
                isNew = true;
            }

            shoot(bullet2, bulletForce2, bulletDestroyTime2, fireRate2);
        }

        if(Time.time >= timeToNextPhase)
        {
            phase = 0;
        }

        if (det.isPlayer)
        {
            GameManager.Singleton.player.GetComponent<PlayerHealth>().takeDamage(bodyDamage);
        }
        if (det.isCrystal)
        {
            GameManager.Singleton.crystalc.takeDamage(bodyDamage);
        }
    }

    private void shoot(GameObject bul, float force, float des, float rate)
    {
        if(Time.time >= timeToShoot)
        {
            timeToShoot = Time.time + rate;

            GameObject init = Instantiate(bul, bulletSpawn.position, gunTop.transform.rotation);
            init.GetComponent<Rigidbody2D>().AddForce(dir * force + new Vector2(0f, Random.Range(-bulletSpread, bulletSpread)), ForceMode2D.Impulse);
            Destroy(init, des);
        }
    }
}
