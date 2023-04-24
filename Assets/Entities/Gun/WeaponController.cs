using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject shootP;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject bullet;
    [SerializeField] public float fireRate;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDestroyTime;

    private float timeToShoot = 0;
    private void Update()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
        transform.up = direction;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(Time.time >= timeToShoot)
            {
                timeToShoot = Time.time + fireRate;

                shoot(direction);
            }
        }
    }

    private void shoot(Vector2 dir)
    {
        GameObject init = Instantiate(bullet, spawnPos.position, this.transform.rotation);
        init.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed, ForceMode2D.Impulse);
        Destroy(init, bulletDestroyTime);

        Instantiate(shootP, spawnPos.position, this.transform.rotation);

        anim.SetTrigger("shoot");
    }
}
