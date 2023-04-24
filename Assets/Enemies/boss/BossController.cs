using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BossHealth
{
    public List<GameObject> enemyPool;

    [SerializeField] private Detector det;
    [SerializeField] private int bodyDamage;

    [SerializeField] private Transform gun1;
    [SerializeField] private Transform gun2;
    [SerializeField] private Transform fp1;
    [SerializeField] private Transform fp2;

    [SerializeField] private float rotationGunIdleSpeed;

    //Boss state 1 - normal speed
    //Boss state 2 - increase speed of all
    //Phase 1 - moving between points and shooting to player simplist fire
    //Phase 2 - Random spamming circleFire
    //Phase 3 - moving between 2 dots and summons sunstrikers from the sky
    //Phase 4 - summon enemies
    //Phase 5 - summon 2 miniBosses
    //Phase 6 - summon less enemies and add negative effects
    //Phase 7 - Create Illusions and flying and shooting(fire illusions also exist)

    [Header("General")]
    [SerializeField] private bool isNew = true;
    [SerializeField] private int curpid;
    [SerializeField] private float minDist;
    [SerializeField] private float bulletSpread;
    [SerializeField] private float gunSpread;
    [SerializeField] private float timeToShoot;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private Transform pointsTop;

    [Header("Phase 1")]
    [SerializeField] private List<Transform> points1;
    [SerializeField] private float speed1;
    [SerializeField] private GameObject bullet1;
    [SerializeField] private float fireRate1;
    [SerializeField] private float bulletForce1;
    [SerializeField] private float bulletDestroyTime1;

    [Header("Phase 2")]
    [SerializeField] private Transform point2;
    [SerializeField] private GameObject bullet2;
    [SerializeField] private float fireRate2;
    [SerializeField] private float speed2;
    [SerializeField] private float bulletForce2;
    [SerializeField] private float bulletDestroyTime2;

    [Header("Phase 3")]
    [SerializeField] private List<Transform> points3;
    [SerializeField] private float bulletDestroyTime3;
    [SerializeField] private GameObject bullet3;
    [SerializeField] private float speed3;
    [SerializeField] private float fireRate3;

    [Header("Phase 4")]
    [SerializeField] private List<Transform> points4;
    [SerializeField] private List<GameObject> enemies4;
    [SerializeField] private float spawnRate4;

    [Header("Phase 5")]
    [SerializeField] private GameObject miniBoss5;
    [SerializeField] private List<Transform> points5;

    [Header("Phase 6")]
    [SerializeField] private GameObject effect6;
    [SerializeField] private List<Transform> points6;
    [SerializeField] private List<GameObject> enemies6;
    [SerializeField] private float spawnRate6;

    [Header("Phase 7")]
    [SerializeField] private List<Transform> points7;
    [SerializeField] private float speed7;
    [SerializeField] private GameObject bullet7;
    [SerializeField] private float fireRate7;
    [SerializeField] private float bulletForce7;
    [SerializeField] private float bulletDestroyTime7;
    [SerializeField] private GameObject illusionBoss7;
    [SerializeField] private List<Transform> illusionSpawnPoints7;
    private List<GameObject> bosses7 = new List<GameObject>();

    [SerializeField] private float timeToNextPhase;

    [SerializeField] private int phase = 0;

    [SerializeField] private Vector2 dir;
    [SerializeField] private List<float> timeToPhases;

    Vector2 toPos;
    Vector2 spreadRot;
    private bool isOnce = false;

    //private int cp = 0;


    private void Update()
    {
        if (isIllusion)
        {
            dir = (GameManager.Singleton.player.position - this.transform.position).normalized;

            gun1.rotation *= Quaternion.Euler(0f, 0f, rotationGunIdleSpeed);
            gun2.rotation *= Quaternion.Euler(0f, 0f, -rotationGunIdleSpeed);

            return;
        }


        pointsTop.transform.position = GameManager.Singleton.player.position;

        dir = (GameManager.Singleton.player.position - this.transform.position).normalized;
        

        if (phase == 0)
        {
            phase = Random.Range(1, 8);
            isNew = true;
            isOnce = false;
            timeToShoot = 0;

            timeToNextPhase = Time.time + timeToPhases[phase - 1];
            //timeToNextPhase = Time.time + timeToPhases[phase - 1];

            effect6.SetActive(false);
        }

        if (phase == 1)
        {
            if (isNew)
            {
                curpid = Random.Range(0, points1.Count);
                toPos = points1[curpid].position;
                isNew = false;
                spreadRot = new Vector2(0f, Random.Range(-gunSpread, gunSpread));
            }

            gun1.up = dir + spreadRot;
            gun2.up = dir - spreadRot;

            this.transform.Translate((toPos - (Vector2)this.transform.position) * Time.deltaTime * speed1);
            if (Vector2.Distance(this.transform.position, toPos) <= minDist)
            {
                isNew = true;
            }

            shoot(bullet1, bulletForce1, bulletDestroyTime1, fireRate1, fp1, gun1);
            shoot(bullet1, bulletForce1, bulletDestroyTime1, fireRate1, fp2, gun2);
        }

        if (phase == 2)
        {
            if (isNew)
            {
                toPos = point2.position;
            }

            gun1.rotation *= Quaternion.Euler(0f, 0f, rotationGunIdleSpeed);
            gun2.rotation *= Quaternion.Euler(0f, 0f, -rotationGunIdleSpeed);

            this.transform.Translate((toPos - (Vector2)this.transform.position) * Time.deltaTime * speed2);

            shoot(bullet2, bulletForce2, bulletDestroyTime2, fireRate2, fp1, gun1);
            shoot(bullet2, bulletForce2, bulletDestroyTime2, fireRate2, fp2, gun2);
        }

        if (phase == 3)
        {
            if (isNew)
            {
                curpid = Random.Range(0, points1.Count);
                toPos = points1[curpid].position;
                isNew = false;
                spreadRot = new Vector2(0f, Random.Range(-gunSpread, gunSpread));
            }

            gun1.rotation *= Quaternion.Euler(0f, 0f, rotationGunIdleSpeed);
            gun2.rotation *= Quaternion.Euler(0f, 0f, -rotationGunIdleSpeed);

            this.transform.Translate((toPos - (Vector2)this.transform.position) * Time.deltaTime * speed3);
            if (Vector2.Distance(this.transform.position, toPos) <= minDist)
            {
                isNew = true;
            }

            Transform spawnSunPos = points3[Random.Range(0, points3.Count)];
            shoot(bullet3, 0, bulletDestroyTime3, fireRate3, spawnSunPos, spawnSunPos);
        }

        if(phase == 4)
        {
            gun1.rotation *= Quaternion.Euler(0f, 0f, rotationGunIdleSpeed);
            gun2.rotation *= Quaternion.Euler(0f, 0f, -rotationGunIdleSpeed);

            Transform spawnEnPos = points4[Random.Range(0, points4.Count)];
            shoot(enemies4[Random.Range(0, enemies4.Count)], 0, 0, spawnRate4, spawnEnPos, spawnEnPos);
        }

        if (phase == 5)
        {
            if (isNew)
            {
                Transform spawnEnPos = points5[Random.Range(0, points5.Count)];

                shoot(miniBoss5, 0, 0, 0, spawnEnPos, spawnEnPos);
                shoot(miniBoss5, 0, 0, 0, spawnEnPos, spawnEnPos);
                isNew = false;
            }
        }

        if (phase == 6)
        {
            gun1.rotation *= Quaternion.Euler(0f, 0f, rotationGunIdleSpeed);
            gun2.rotation *= Quaternion.Euler(0f, 0f, -rotationGunIdleSpeed);

            if (isNew)
            {
                effect6.SetActive(true);
                isNew = false;
            }

            Transform spawnEnPos = points6[Random.Range(0, points6.Count)];
            shoot(enemies6[Random.Range(0, enemies4.Count)], 0, 0, spawnRate6, spawnEnPos, spawnEnPos);
        }

        if(phase == 7 && !isIllusion)
        {
            if (!isOnce)
            {
                isOnce = true;

                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        bosses7.Add(Instantiate(illusionBoss7, illusionSpawnPoints7[i]));
                    }
                    catch
                    {

                    }
                }
            }
            if (isNew)
            {
                curpid = Random.Range(0, points7.Count);
                toPos = points7[curpid].position;
                spreadRot = new Vector2(0f, Random.Range(-gunSpread, gunSpread));
                isNew = false;
            }

            gun1.up = dir + spreadRot;
            gun2.up = dir - spreadRot;

            this.transform.Translate((toPos - (Vector2)this.transform.position) * Time.deltaTime * speed1);
            if (Vector2.Distance(this.transform.position, toPos) <= minDist)
            {
                isNew = true;
            }

            shoot(bullet7, bulletForce7, bulletDestroyTime7, fireRate7, fp1, gun1);
            shoot(bullet7, bulletForce7, bulletDestroyTime7, fireRate7, fp2, gun2);
        }

        if (Time.time >= timeToNextPhase)
        {
            if (phase == 7)
            {
                for (int i = 0; i < 3; i++)
                {
                    Destroy(bosses7[i].gameObject);
                }
                bosses7 = null;
                bosses7 = new List<GameObject>();
            }
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

    private void shoot(GameObject bul, float force, float des, float rate, Transform bulSpaw, Transform gunTop)
    {
        if (Time.time >= timeToShoot)
        {
            timeToShoot = Time.time + rate;

            GameObject init = Instantiate(bul, bulSpaw.position, gunTop.transform.rotation);
            init.GetComponent<Rigidbody2D>().AddForce(dir * force + new Vector2(0f, Random.Range(-bulletSpread, bulletSpread)), ForceMode2D.Impulse);
            //enemyPool.Add(init);

            if (des > 0)
            {
                Destroy(init, des);
            }
        }
    }

    public void destroyBoss()
    {
        //for (int i = 0; i < enemyPool.Count; i++)
        //{
        //    if (enemyPool[i].gameObject != null)
        //    {
        //        Destroy(enemyPool[i].gameObject);
        //    }
        //}

        Destroy(this.gameObject);
    }
}
