using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed= 15f;
    Rigidbody2D rb;

    public int Damage =20;
    public Vector2 direction = Vector2.right;
    Vector2 velocity;
    public bool isEnemy =false;
    private void Awake() 
    {
        rb= gameObject.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        bulletMove();
        if(transform.position.x > Camera.main.orthographicSize + 50
           ||transform.position.x < -Camera.main.orthographicSize -5
           ||transform.position.y > Camera.main.orthographicSize +30
           ||transform.position.y < -Camera.main.orthographicSize -30 )
        {
            Destroy(gameObject);
        }
    }
    
        private void OnTriggerEnter2D(Collider2D other)  
    {
        if(other.tag == "Enemy" && !isEnemy)
        {
            other.GetComponent<Enemy>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if(other.tag =="Player"&& isEnemy)
        {
            other.gameObject.GetComponent<Player>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if(other.tag =="Player" && !isEnemy)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),other);
            
        }
        else if(other.tag =="Enemy" && isEnemy)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),other);
            
        }
        else
        {
            return;
        }

        

    }
        public void bulletMove()
        {
            Vector2 pos = transform.position;
            velocity = direction *speed;
            pos += velocity * Time.deltaTime;
            transform.position = pos;

        }
}
