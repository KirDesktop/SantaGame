using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tangerineController : MonoBehaviour
{
    [SerializeField] private GameObject vig;
    [SerializeField] private float heal = 1;

    [SerializeField] private float dur;
    private float timeToOff;
    [SerializeField] private Detector det;
    private bool isRun = false;
    [SerializeField] private GameObject abP;
    private void Update()
    {
        if (det.isPlayer && !isRun)
        {
            isRun = true;
            timeToOff = Time.time + dur;
            vig.SetActive(true);
            GameManager.Singleton.player.GetComponent<PlayerHealth>().currentHealth += (int)heal;
            GameManager.Singleton.player.GetComponent<PlayerHealth>().updateHealth();
        }
        if ((Time.time >= timeToOff) && isRun)
        {
            Instantiate(abP, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
