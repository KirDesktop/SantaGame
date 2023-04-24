using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleFire : MonoBehaviour
{
    [SerializeField] private float instantTime;
    [SerializeField]private float timeToInstant;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField]private float timeToDie;

    [SerializeField]private bool isActive = false;

    private void Awake()
    {
        timeToInstant = Time.time + instantTime;
        timeToDie = Time.time + lifeTime;
    }

    private void Update()
    {
        if((Time.time >= timeToInstant) && !isActive)
        {
            isActive = true;
        }
        //Debug.Log(Time.time.ToString());

        if((Time.time >= timeToDie) && isActive)
        {
            //Debug.Log("what");
            Destroy(this.gameObject);
        }

        if (isActive)
        {
            this.GetComponent<Rigidbody2D>().velocity = (GameManager.Singleton.player.position - this.transform.position).normalized * speed;
        }
    }

    private void FixedUpdate()
    {
        this.transform.rotation *= Quaternion.Euler(0f,0f,rotateSpeed);
    }
}
