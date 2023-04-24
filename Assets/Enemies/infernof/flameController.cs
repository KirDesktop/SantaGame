using UnityEngine;

public class flameController : MonoBehaviour
{
    [SerializeField] private int damage = 0;
    [SerializeField] private int increaseDamagePer;
    [SerializeField] private float increaseRate;
    [SerializeField] private float shootRate;
    [SerializeField] private GameObject damageP;

    public Transform top;

    private float timeToIncrease;
    private float timeToShoot;

    public bool isPl = false;
    private void Update()
    {
        this.GetComponent<LineRenderer>().SetPosition(0, top.position);

        if (isPl)
        {
            Instantiate(damageP, GameManager.Singleton.player.position, Quaternion.identity);
            this.GetComponent<LineRenderer>().SetPosition(1, GameManager.Singleton.player.transform.position);
        }
        else
        {
            Instantiate(damageP, GameManager.Singleton.crystal.position, Quaternion.identity);
            this.GetComponent<LineRenderer>().SetPosition(1, GameManager.Singleton.crystal.transform.position);
        }

        if (Time.time >= timeToIncrease)
        {
            timeToIncrease = Time.time + increaseRate;

            damage += increaseDamagePer;
        }

        if(Time.time >= timeToShoot)
        {
            timeToShoot = Time.time + shootRate;

            if (isPl)
            {
                GameManager.Singleton.player.GetComponent<PlayerHealth>().takeDamage(damage);
            }
            else
            {
                GameManager.Singleton.crystalc.takeDamage(damage);
            }
        }
    }
}
