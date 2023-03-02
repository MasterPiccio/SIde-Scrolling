using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public List<GameObject> Powerups = new List<GameObject>();



    
    public void OnKillSpawn(Vector3 _position)
    {   
        int randomChance;
        randomChance = Random.Range(0, 100);

        if(randomChance>50)
        {
            int rndPowerUp = Random.Range(0,Powerups.Count);
            GameObject powerup = Instantiate(Powerups[rndPowerUp], _position, Quaternion.identity);
            switch(rndPowerUp)
            {
                case 0:
                    powerup.name = "Ammo";
                    break;
                case 1:
                    powerup.name = "Heal";
                    break;
                case 2:
                    powerup.name ="SpeedUp";
                    break;

            }
        }
    }
            

        



}
