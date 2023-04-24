using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunStrikerController : MonoBehaviour
{
    [SerializeField] private Detector det;
    [SerializeField] private int damage;
    [SerializeField] private float angSpread = 30;
    [SerializeField] private float speed;
    private Vector2 startDir;
    [SerializeField] private float destroyTime;

    //[SerializeField] private GameObject damageP;

    private void Awake()
    {
        float rA = Random.Range(90-angSpread, 90+angSpread);
        startDir = -transform.up * Mathf.Cos(rA) + transform.right * Mathf.Sin(rA);
        if (startDir.y > 0)
        {
            startDir = -startDir;
        }

        this.transform.up = -startDir;

        Destroy(this.gameObject, destroyTime);
    }

    private void Update()
    {
        if (det.isPlayer)
        {
            GameManager.Singleton.player.GetComponent<PlayerHealth>().takeDamage(damage);

            Destroy(this.gameObject);
        }

        if (det.isCrystal)
        {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        this.GetComponent<Rigidbody2D>().velocity = startDir * speed;
    }
}
