using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemyRb;
    private Vector3 dir;
    private bool isFrozen;
    public float force = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        dir = (player.transform.position - transform.position).normalized;
        if (transform.position.y >= -0.5f)
        {
            if (!isFrozen)
            {
                force = 600f;
                enemyRb.AddForce(dir * force*Time.deltaTime);
            }
            else
            {
                force = 600f;
                enemyRb.AddForce( dir * force*Time.deltaTime);
                Vector3 enemyDir = (enemyRb.velocity).normalized;
                enemyRb.AddForce(-enemyDir * 550 * Time.deltaTime);
                //enemyRb.AddForce(-dir * 5.0f);
               // enemyRb.AddForce(dir * force);
            }
        }
        if (transform.position.y < -25f) 
            Destroy(gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FrozenVFX"))
        {
            isFrozen = true;
            //enemyRb.AddForce(-dir * 15f);
            //force = 0.1f;
            Debug.Log("froze");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FrozenVFX"))
        {
            isFrozen = false;
            //enemyRb.AddForce(-dir * 15f);
            //force = 0.1f;
            Debug.Log("finish");
        }
    }
}
