using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _defeatSound;

    [SerializeField] private Text _waveTimer;

    [SerializeField] private List<Transform> _abilitiesSpawnPoints;
    [SerializeField] private List<Transform> _enemySpawnPoints;
    [SerializeField] private List<Transform> _sunStrikerSpawnPoints;

    [SerializeField] private GameObject _sunStriker;

    public static WaveManager Singleton;

    [System.Serializable]
    class enemy
    {
        public GameObject instant;
        public float spawnRate;

        [HideInInspector] public float timeToSpawn = 0;
    }

    [System.Serializable]
    class wavesettings
    {
        public float waveDuration;
        public List<enemy> enemys;

        public List<GameObject> abilities;
        public bool isSpawnAbilities = false;
        public float spawnAbilitiesRate = 3f;

        public bool isSpawnSunStrikers;
        public float sunSpawnRate;

        public UnityEvent beforeWave;
        public UnityEvent afterWave;
        public UnityEvent defeatWave;

        public int currentProgress = 0;

        [HideInInspector] public List<GameObject> enemyPool;
    }

    private float _timeToAbilitySpawn = 0;

    private float _timeToSpawnSunStriker = 0;

    private float _timeWaveStarted;

    public bool isWave = false;
    private float timeToClose = 0;

    [SerializeField] private List<wavesettings> waves = new List<wavesettings>();
    public int currentWave = 0;

    private bool _isBoss = false;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && isWave)
        {
            winWave();
        }

        if(Time.time >= timeToClose && isWave && !_isBoss)
        {
            winWave();
        }

        if (isWave)
        {
            waves[currentWave].currentProgress = Mathf.FloorToInt(waves[currentWave].waveDuration - (Time.time - _timeWaveStarted));
            _waveTimer.text = waves[currentWave].currentProgress.ToString();

            for (int i = 0; i < waves[currentWave].enemys.Count; i++)
            {
                if(Time.time >= waves[currentWave].enemys[i].timeToSpawn)
                {
                   // Debug.Log(waves[currentWave].enemys.Count);
                    waves[currentWave].enemys[i].timeToSpawn = Time.time + waves[currentWave].enemys[i].spawnRate;


                    GameObject init = Instantiate(waves[currentWave].enemys[i].instant, 
                        _enemySpawnPoints[Random.Range(0, _enemySpawnPoints.Count)]);
                    waves[currentWave].enemyPool.Add(init);
                }
            }

            if (waves[currentWave].isSpawnAbilities)
            {
                if (Time.time > _timeToAbilitySpawn)
                {
                    _timeToAbilitySpawn = Time.time + waves[currentWave].spawnAbilitiesRate;

                    int randomAbility = Random.Range(0, waves[currentWave].abilities.Count);
                    int randomSpawnPoint = Random.Range(0, _abilitiesSpawnPoints.Count);

                    Instantiate(waves[currentWave].abilities[randomAbility], _abilitiesSpawnPoints[randomSpawnPoint]);
                }
            }

            if (waves[currentWave].isSpawnSunStrikers)
            {
                if (Time.time > _timeToSpawnSunStriker)
                {
                    _timeToSpawnSunStriker = Time.time + waves[currentWave].sunSpawnRate;

                    int randomSpawnPoint = Random.Range(0, _sunStrikerSpawnPoints.Count);

                    Instantiate(_sunStriker, _sunStrikerSpawnPoints[randomSpawnPoint]);
                }
            }

        } 
        else
        {
            _waveTimer.text = "";
        }
    }

    public void winWave()
    {
        isWave = false;
        waves[currentWave].afterWave.Invoke();
        SantaController.Singleton.setDialogActive(true);
        AlertManager.Singleton.enableMoveToSantaAlert(true);

        AlertManager.Singleton.setTextAlert("Win");

        GameManager.Singleton.crystalc.heal();
        GameManager.Singleton.player.GetComponent<PlayerHealth>().heal();

        this.GetComponent<AudioSource>().clip = _winSound;
        this.GetComponent<AudioSource>().Play();

        _clearEnemies(currentWave);


        currentWave++;
        SantaController.Singleton.dialogState++;
    }

    public void startWave()
    {
        AlertManager.Singleton.enableMoveToSantaAlert(false);

        _timeWaveStarted = Time.time;

        waves[currentWave].beforeWave.Invoke();
        timeToClose = Time.time + waves[currentWave].waveDuration;
        isWave = true;
        SantaController.Singleton.setDialogActive(false);
    }

    public void defeat()
    {
        if (GameManager.Singleton.isBoss)
        {
            GameManager.Singleton._bossController.destroyBoss();
        }

        waves[currentWave].defeatWave.Invoke();
        SantaController.Singleton.setDialogActive(true);
        AlertManager.Singleton.enableMoveToSantaAlert(true);

        if (isWave)
        {
            isWave = false;

            GameManager.Singleton.crystalc.heal();

            GameManager.Singleton.player.GetComponent<PlayerHealth>().heal();

            AlertManager.Singleton.setDur(2f);
            AlertManager.Singleton.setTextAlert("Defeat");

            this.GetComponent<AudioSource>().clip = _defeatSound;
            this.GetComponent<AudioSource>().Play();
        }
        else
        {
            GameManager.Singleton.player.GetComponent<PlayerHealth>().heal();
        }

        _clearEnemies(currentWave);
    }

    private void _clearEnemies(int currWaveId)
    {
        for (int i = 0; i < waves[currentWave].enemyPool.Count; i++)
        {
            Destroy(waves[currentWave].enemyPool[i].gameObject);
        }
    }
}
