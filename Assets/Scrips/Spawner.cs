using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    float timetospawn = 5f;
    float elapsedTime;
    public GameObject enemyPrefab;

    [SerializeField]
    int enemycount=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
       if(elapsedTime >=timetospawn)
     { 
         SpawnEnemy(elapsedTime);
     }
    }


    public void SpawnEnemy(float _time)
    {
        if(enemycount < 6 )
        {
            float posX = transform.position.x;
            float posY = transform.position.y;
            Vector3 spawnPosition = new Vector3(Random.Range(posX-3 ,posX+3),Random.Range(posY-3,posY+3),0);
            GameObject enemy = Instantiate(enemyPrefab,spawnPosition,transform.rotation* Quaternion.Euler(0,180,0));
            enemy.tag ="Enemy";
            elapsedTime =0;
            CountEnemy(1);
        }
        else
        {
            elapsedTime=0;
        }
        
    }

    public void CountEnemy(int _value)
    {
        enemycount += _value;
    }
}
