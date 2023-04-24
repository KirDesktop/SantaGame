using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject bossBarObj;
    [SerializeField] private Slider _bossBar;
    [SerializeField] private GameObject boss;
    [SerializeField] private Transform bossSpawnPos;

    public WeaponController candy;
    public WaveManager waveManag;
    public CrystalController crystalc;

    public bool isBoss = false;
    public BossController _bossController;

    //[SerializeField] private UnityEvent[] events;
    //private int currentId = 0;

    public static GameManager Singleton;

    public Transform crystal;
    public Transform player;

    private void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
        }
    }

    public void respawn()
    {
        //Debug.Log("smert");
        waveManag.defeat();
    }
    public void summonBoss()
    {
        isBoss = true;
        Camera.main.orthographicSize = 15;
        bossBarObj.SetActive(true);
        GameObject rt =  Instantiate(boss, bossSpawnPos);
        rt.GetComponent<BossHealth>()._bar = _bossBar;
        _bossController = rt.GetComponent<BossController>();
    }

}
