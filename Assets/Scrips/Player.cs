using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{


public float MoveSpeed = 12f; 
public Rigidbody2D rb;
public Transform firepos;
public GameObject bulletPrefab;
public Slider slider;
public Gradient gradient;
public Image imageHealth;
public int MaxHealth = 100;
public int CurrentHealth;


[SerializeField]
int AmmoCount =100;


public int ammocount 
{get {return AmmoCount;}
 set {if (AmmoCount+value >100)
            {AmmoCount=100; }
            else
            {
                AmmoCount += 20;
            }} }
float TimeToShoot = 0.5f;
float elapsedTime=0;

float UpBound = 4f, LowerBound = -4f, LeftBound=-8f, RightBound=8f;
Vector3 pos;


    // Start is called before the first frame update
private void Awake() 
{
    CurrentHealth = MaxHealth;
    slider.maxValue = MaxHealth;
    slider.value =MaxHealth;
    imageHealth.color = gradient.Evaluate(1f);
    rb = GetComponent<Rigidbody2D>();


    
}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            CurrentHealth +=-20;
        }
        elapsedTime += Time.deltaTime;
        move();
        if(Input.GetKeyDown(KeyCode.Space) && AmmoCount>=1 && elapsedTime >= TimeToShoot)
        {

            shoot();
            
        }
        
        
        
    }




    public void move()
    {
        pos = transform.position;
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*MoveSpeed,Input.GetAxis("Vertical")*MoveSpeed);
        if(pos.x >= RightBound)
        {
            pos.x = RightBound;
            
        }
        if(pos.x <= LeftBound)
        {
            pos.x =LeftBound;
            
        }
        if(pos.y <= LowerBound)
        {
            pos.y = LowerBound;
            
        }
        if(pos.y >= UpBound)
        {
            pos.y =UpBound;
            
        }
        transform.position = new Vector3(pos.x,pos.y,pos.z);
    }

    void shoot()
    {
        GameObject bulletobj = Instantiate(bulletPrefab, firepos.position, Quaternion.identity);
        Bullet bullet =bulletobj.GetComponent<Bullet>();
        bullet.isEnemy =false;
        elapsedTime =0;
        AmmoCount--;
    }

     public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;


        if(CurrentHealth <=0 )
        {
            
            Destroy(gameObject);


        }

        slider.value = CurrentHealth;
        imageHealth.color = gradient.Evaluate(slider.normalizedValue);
    } 


private void OnTriggerEnter2D(Collider2D other) 
{

    if(other.tag == "PowerUp")
    {

    switch (other.name)
    {
        case "Ammo":
            ammocount += 20;
            break;
        case "Heal":
            CurrentHealth += 20;
            if(CurrentHealth>100)
            {
                CurrentHealth =100;
            }
            slider.value = CurrentHealth;
            imageHealth.color = gradient.Evaluate(slider.normalizedValue);
            break;
        case "SpeedUp":
            if(MoveSpeed != 18f)
            {
                MoveSpeed = 18f;
                StartCoroutine(ResetSpeed());
            }
            break;
    
    }
    Destroy(other.gameObject);
    }
    else
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(),other);
    }

    
}

IEnumerator ResetSpeed()
{
    yield return new WaitForSeconds (15f);
    MoveSpeed =12f;
}
}
