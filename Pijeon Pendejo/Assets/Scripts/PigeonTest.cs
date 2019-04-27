using UnityEngine;

public class PigeonTest : MonoBehaviour
{
     public float speed;
     private Rigidbody2D rb;
     private Vector2 mv;
 
     void Start()
     {
         rb = GetComponent<Rigidbody2D>();
     }
 
     void Update()
     {
         Vector2 mi = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
         mv = mi.normalized * speed;
     }
 
     private void FixedUpdate()
     {
         rb.MovePosition(rb.position + mv * Time.fixedDeltaTime);
     }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Pigeon collided with enemy");
            Destroy(gameObject);
        }
    }
}
