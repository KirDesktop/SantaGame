using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertManager : MonoBehaviour
{
    public static AlertManager Singleton;
    [SerializeField] private GameObject toSanta;
    [SerializeField] private GameObject text;
    [SerializeField] private Text t;

    private void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
        }

        setDur(2);
    }

    public void enableMoveToSantaAlert(bool isActive)
    {
        //Debug.Log(isActive);
        toSanta.SetActive(isActive);
    }

    private float dur;
    public void setDur(float d)
    {
        dur = d;
    }

    public void setTextAlert(string message)
    {
        StartCoroutine(showText(dur));
        text.SetActive(true);
        text.GetComponent<Animator>().SetTrigger("show");
        t.text = message;
    }

    private IEnumerator showText(float dur)
    {
        yield return new WaitForSeconds(dur);

        text.SetActive(false);
    }
}
