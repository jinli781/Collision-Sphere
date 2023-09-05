using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject spawnmanager;
    private SpawnManager spawnManager;
    public GameObject player;
    public GameObject starButton;
    public GameObject end;
    public GameObject restart;
    private PlayerController playerController;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>().gameObject.GetComponent<PlayerController>();
        spawnManager = spawnmanager.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isDead = playerController.isDead;
        if (isDead)
        {
            end.SetActive(true);
            restart.SetActive(true);
            spawnManager.enabled = false;
           // spawnManager.SetActive(false);
        }
    }
        public void GameStart() {
        spawnManager.enabled = true;
        //spawnManager.SetActive(true);
        starButton.SetActive(false);
    }
    public void GameReload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
