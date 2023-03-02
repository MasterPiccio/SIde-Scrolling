using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    Player player;
    public Text ammoCount;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        updateUI();
        
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }


    void updateUI()
    {
        ammoCount.text = player.ammocount.ToString();
    }
}
