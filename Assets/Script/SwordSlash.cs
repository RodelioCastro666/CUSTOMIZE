using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private int sworddamage;

    private Animator anim;
    // private BoxCollider2D boxCollider;

    [SerializeField]
    private Rigidbody2D myrigidbody;

    public Transform Mytarget { get; set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        // boxCollider = GetComponent<BoxCollider2D>();
        myrigidbody = GetComponent<Rigidbody2D>();

    }

    public void Initialize(int damage)
    {
        this.sworddamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            collision.GetComponentInParent<Enemy>().TakeDamage(sworddamage);
            anim.SetTrigger("hit");
            myrigidbody.velocity = Vector2.zero;
        }
        
     
    }

    public void SetUp(Vector2 velocity, Vector3 direction)
    {
        myrigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);

    }

    public void Deactivate()
    {

    }

    
}
