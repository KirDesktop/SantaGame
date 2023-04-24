using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private float invTime;
    [SerializeField] private Animator anim;

    private float timeToDamage;

    public int currentHealth = 10;

    [SerializeField] private int maxHealth = 10;

    public void takeDamage(int damage)
    {
        GameManager.Singleton.player.GetComponent<PlayerMovement>().anim.SetTrigger("damage");
        if(Time.time >= timeToDamage)
        {
            timeToDamage = Time.time + invTime;
            currentHealth -= damage;
            updateHealth();

            anim.SetTrigger("damage");

            this.GetComponent<AudioSource>().Play();
        }

        if (currentHealth <= 0)
        {
            GameManager.Singleton.respawn();
        }
    }
    private void Awake()
    {
        currentHealth = maxHealth;
        _bar.maxValue = maxHealth;

        updateHealth();
    }
    public void updateHealth()
    {
        _bar.value = currentHealth;
    }

    public void heal()
    {
        currentHealth = maxHealth;
        updateHealth();
    }
}
