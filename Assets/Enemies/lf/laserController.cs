using UnityEngine;

public class laserController : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject damageP;
    public bool isPl = false;
    private void Start()
    {
        if (isPl)
        {
            GameManager.Singleton.player.GetComponent<PlayerHealth>().takeDamage(damage);
            Instantiate(damageP, GameManager.Singleton.player.position, Quaternion.identity);
        }
        else
        {
            GameManager.Singleton.crystalc.takeDamage(damage);
            Instantiate(damageP, GameManager.Singleton.crystal.position, Quaternion.identity);
        }
    }
}
