using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class SantaController : MonoBehaviour
{
    public static SantaController Singleton;

    [System.Serializable]
    class segment
    {
        public List<string> texts = new List<string>();
        public List<bool> isSantas = new List<bool>();
        //public List<float> timeDialogs = new List<float>();
        //public List<AudioClip> voices = new List<AudioClip>();
        public UnityEvent startEvent;
        public UnityEvent lastEvent;
    }

    [SerializeField] private Detector detector;
    [SerializeField] private GameObject dialog;
    [SerializeField] private Text showText;

    [SerializeField] private List<segment> data = new List<segment>();

    public int dialogState = 0;
    public int segmentState = 0;

    [SerializeField]private bool isDialog = false;
    [SerializeField]private bool isCan = true;

    [SerializeField] private SpriteRenderer icon;
    public Sprite santa;
    public Sprite player;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    private void Update()
    {
        if (isDialog && Input.GetKeyDown(KeyCode.F))
        {
            disableDialog();
        }

        if (!WaveManager.Singleton.isWave)
        {
            if (detector.isPlayer)
            {
                if (Input.GetKeyDown(KeyCode.F) && isCan)
                {
                    enableDialog();
                }
            }
            else
            {
                disableDialog();
            }
        } else
        {
            AlertManager.Singleton.enableMoveToSantaAlert(false);
        }

        if(isDialog && Input.GetKeyDown(KeyCode.Mouse0) && isCan)
        {
            if (segmentState < data[dialogState].texts.Count-1)
            {
                segmentState++;
                showText.text = data[dialogState].texts[segmentState];
                
                if (data[dialogState].isSantas[segmentState])
                {
                    icon.sprite = santa;
                }
                else
                {
                    icon.sprite = player;
                }
            }
            else
            {
                data[dialogState].lastEvent.Invoke();
                //WaveManager.Singleton.startWave(false);

                dialog.SetActive(false);

                isDialog = false;
                segmentState = 0;

                //dialogState++;
            }
        }
    }
    private void enableDialog()
    {
        //Debug.Log("common");
        data[dialogState].startEvent.Invoke();
        AlertManager.Singleton.enableMoveToSantaAlert(false);

        dialog.SetActive(true);

        showText.text = data[dialogState].texts[segmentState];
        isDialog = true;
    }
    private void disableDialog()
    {
        AlertManager.Singleton.enableMoveToSantaAlert(true);
        dialog.SetActive(false);

        isDialog = false;
        segmentState = 0;
    }

    public void setDialogActive(bool isActive)
    {
        isCan = isActive;

        if(isCan == false)
        {
            disableDialog();
        }
    }

    public void defeat()
    {
        dialogState--;
    }
}
