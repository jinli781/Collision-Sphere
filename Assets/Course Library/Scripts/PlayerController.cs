using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Force;
    private GameObject CameraPoint;
    private Rigidbody playRb;
    public GameObject SpawnManager;
    private SpawnManager spawnManager;
    private int enemyNum;
    private Vector3 desireScale;
    public bool isPower;
    public bool isFrozen;
    public GameObject ring;
    public GameObject frozenVFX;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        CameraPoint = FindObjectOfType<RotateCamera>().gameObject;
        playRb = GetComponent<Rigidbody>();
        //  spawnManager = FindObjectOfType<SpawnManager>().gameObject.GetComponent<SpawnManager>();
        spawnManager = SpawnManager.GetComponent<SpawnManager>();
        desireScale = Vector3.zero;
        frozenVFX.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        enemyNum = 0;
        if(spawnManager.enabled!=false)
        enemyNum = spawnManager.enemynum;
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        playRb.AddForce(CameraPoint.transform.forward * Force * verticalInput *Time.deltaTime);
        playRb.AddForce(CameraPoint.transform.right * Force * horizontalInput *Time.deltaTime);
        if (enemyNum == 0)
        {
            isPower = false;
            scaleDown();
        }
        ring.SetActive(isPower);
        if (isPower)
        {
            ring.transform.position = transform.position;
        }
        frozenVFX.SetActive(isFrozen);
        if (isFrozen) {
            frozenVFX.transform.position = transform.position;
            frozenVFX.transform.localScale = Vector3.Lerp(frozenVFX.transform.localScale, desireScale, Time.deltaTime * 2f);
        }
        if (transform.position.y < -5f) { 
        isDead = true;
        }
    }
    private void scaleDown() {
        desireScale = Vector3.zero;
    }
    private void scaleUp() {
        desireScale = new Vector3(4.0f, 4.0f, 4.0f);
    }
    IEnumerator powerupcountdown()
    {
       
        yield return new WaitForSeconds(7);
      
        isPower = false;
    }
    IEnumerator Frozencountdown()
    {
        scaleUp();
        yield return new WaitForSeconds(8);
        scaleDown();
        yield return new WaitForSeconds(2);
        isFrozen = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("powerUp"))
        {
            Destroy(other.gameObject);
            isPower = true;
            StartCoroutine(powerupcountdown());
        }
        else if (other.CompareTag("Frozen")) {
            Destroy(other.gameObject);
            isFrozen = true;
            StartCoroutine(Frozencountdown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy") && isPower)
        {
            Vector3 bounceDir = (collision.transform.position - transform.position).normalized;
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(bounceDir * 15, ForceMode.Impulse);
        }
    }
}
