using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Bullet EnemyBulletPrefab;
    GameController gameController;

    Rigidbody2D rb;
    public Transform firepos;
    Spawner spawner;
    int AmmoCount=50;
     public float enemySpeed = 1f; 

    public int MaxHealth = 100;
    public int CurrentHealth;
    public Slider slider;
    public Gradient gradient;
    public Image imageHealth;
    Vector2 direction;

    float TimeToShoot = 2f;
    float elapsedTime=0;

    // Start is called before the first frame update

    private void Awake() 
    {
        spawner = FindObjectOfType<Spawner>();
        gameController = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
        slider.maxValue = MaxHealth;
        slider.value =MaxHealth;
        imageHealth.color = gradient.Evaluate(1f);
        direction = (transform.localRotation * Vector2.right).normalized;
        
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= TimeToShoot)
        
        Shoot();
        EnemyMoveSin();
        EnemyMove();
        Autodestructor();
    }



    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;


        if(CurrentHealth <=0 )
        {
            
            Destroy(gameObject);
            spawner.CountEnemy(-1);
            gameController.OnKillSpawn(transform.position);

        }

        slider.value = CurrentHealth;
        imageHealth.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void Shoot()
    {
        
        GameObject bulletobj = Instantiate(EnemyBulletPrefab.gameObject, firepos.position, Quaternion.identity);
        Bullet bullet =bulletobj.GetComponent<Bullet>();
        bullet.direction = direction;
        bullet.isEnemy =true;
        bullet.speed =5f;
        bullet.Damage =10;

        AmmoCount--;
        
        elapsedTime =0;
    }

    public void EnemyMoveSin()
    {
        Vector2 pos = transform.position;
        
        float sin = Mathf.Sin(pos.x*5)*0.1f;
        pos.y += sin;
        transform.position =pos;
    }
    public void EnemyMove()
    {
        Vector2 pos = transform.position;
        pos.x -= enemySpeed*Time.deltaTime;
        transform.position =pos;
    }
    
    private void OnTriggerEnter2D(Collider2D other ) 
    {      
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(),other);
        
    }


    public void Autodestructor()
    {
            if(transform.position.x > Camera.main.orthographicSize + 10
           ||transform.position.x < -Camera.main.orthographicSize -10
           ||transform.position.y > Camera.main.orthographicSize +10
           ||transform.position.y < -Camera.main.orthographicSize -10 )
        {
            Destroy(gameObject);
            spawner.CountEnemy(-1);
        }
    }

}
