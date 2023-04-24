using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public bool isPlayer = false;
    public bool isCrystal = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
        }
        if (collision.CompareTag("crystal"))
        {
            isCrystal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
        }
        if (collision.CompareTag("crystal"))
        {
            isCrystal = false;
        }
    }
}
