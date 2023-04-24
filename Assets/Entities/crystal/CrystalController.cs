using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalController : MonoBehaviour
{
    public static CrystalController Singleton;
    [SerializeField] private Slider bar;
    [SerializeField] private float invTime;

    public int currentHealth = 30;

    [SerializeField] private int maxHealth = 30;

    private float timeToDamage;

    private void Awake()
    {
        bar.maxValue = maxHealth;
        currentHealth = maxHealth;

        updateHealth();

        if(Singleton != null)
        {
            Singleton = this;
        }
    }

    public void takeDamage(int damage)
    {
        if (Time.time >= timeToDamage)
        {
            timeToDamage = Time.time + invTime;
            currentHealth -= damage;
            updateHealth();

            this.GetComponent<AudioSource>().Play();
        }

        if (currentHealth <= 0)
        {
            
            WaveManager.Singleton.defeat();
        }
    }

    private void updateHealth()
    {
        bar.value = currentHealth;
    }

    public void heal()
    {
        currentHealth = maxHealth;
        updateHealth();
    }
}
