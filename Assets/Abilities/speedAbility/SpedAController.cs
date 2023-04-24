using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpedAController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rd;
    [SerializeField] private GameObject vig;
    [SerializeField] private Detector det;
    [SerializeField] private float movSpeedPlus;
    [SerializeField] private float bulletSpeedPlus;
    [SerializeField] private float dur;
    private float timeToOff;
    private bool isRun = false;

    [SerializeField] private GameObject abP;

    private void Update()
    {
        if (det.isPlayer && !isRun)
        {
            isRun = true;
            timeToOff = Time.time + dur;
            //Debug.Log("why");
            //Debug.Log(timeToOff.ToString() +"   ssd   " +  Time.time.ToString());
            GameManager.Singleton.candy.fireRate += bulletSpeedPlus;
            GameManager.Singleton.player.GetComponent<PlayerMovement>().speed += movSpeedPlus;
            vig.SetActive(true);
            rd.sprite = null;

        }
        if((Time.time >= timeToOff) && isRun)
        {
            //Debug.Log("aboba");
            GameManager.Singleton.candy.fireRate -= bulletSpeedPlus;
            GameManager.Singleton.player.GetComponent<PlayerMovement>().speed -= movSpeedPlus;

            Instantiate(abP, this.transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
