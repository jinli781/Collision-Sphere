using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject powerUp;
    public GameObject frozen;
    public int enemynum;
    int num1 = 0;
    public Text level;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         enemynum = FindObjectsOfType<Enemy>().Length;
        if (enemynum == 0)
        {
            num1++;
            spawnEnemy(num1);
            SpawnBuff(num1-1);
        }
        level.text = "level: "+num1.ToString();
    }
    public Vector3 randompos()
    {
        Vector3 startPos;
        float startPosx = Random.Range(-5, +5);
        float startPosz = Random.Range(-5, 5);
        startPos = new Vector3(startPosx, 0, startPosz);
        return startPos;
    }
    public void spawnEnemy(int num)
    {
        for (int i = 1; i <= num; i++)
        {
            Instantiate(enemy, randompos(), Quaternion.identity);
        }
    }
    public void SpawnBuff(int num)
    {
        if (num != 0) { 
        Instantiate(powerUp, randompos(), Quaternion.identity);
            if (num % 3 == 0)
            {
                Instantiate(frozen, randompos(), Quaternion.identity);
            }
        }
        
    }
}
