using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    //[SerializeField] private Slider _bar;
    //[SerializeField] private float invTime;

   // private float timeToDamage;

    public int currentHealth = 10;

    [SerializeField] private GameObject deathP;
    [SerializeField] private int maxHealth = 10;

    private void Update()
    {
        
    }

    public virtual void takeDamage(int damage)
    {
        //if (Time.time >= timeToDamage)
        //{
            //timeToDamage = Time.time + invTime;
        currentHealth -= damage;
        updateHealth();

        if (currentHealth <= 0)
        {
            death();
        }
        //}
    }
    private void Awake()
    {
        currentHealth = maxHealth;
        //_bar.maxValue = maxHealth;
    }
    public void updateHealth()
    {
        //_bar.value = currentHealth;
    }

    private void death()
    {
        GameObject dP = Instantiate(deathP, this.transform.position, Quaternion.identity);
        //dP.transform.localScale = new Vector3(1f, 1f, 1f);
        Destroy(this.gameObject);
    }
}
