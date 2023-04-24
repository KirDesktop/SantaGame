using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] public bool isIllusion = false;
    [SerializeField] public Slider _bar;
    //[SerializeField] private float invTime;

    // private float timeToDamage;

    public int currentHealth = 10;

    [SerializeField] private int maxHealth = 10;

    public virtual void takeDamage(int damage)
    {
        //if (Time.time >= timeToDamage)
        //{
        //timeToDamage = Time.time + invTime;
        if (!isIllusion)
        {
            currentHealth -= damage;
            updateHealth();

            if (currentHealth <= 0)
            {
                death();
            }
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
        _bar.value = (float)((float)currentHealth*((float)(1/ (float)maxHealth)));
    }

    private void death()
    {
        WaveManager.Singleton.winWave();
        this.GetComponent<BossController>().destroyBoss();
    }
}
