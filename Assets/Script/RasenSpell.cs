using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasenSpell : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Animator anim;
   

    [SerializeField]
    private Rigidbody2D myrigidbody;

    public Transform Mytarget { get; set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        // boxCollider = GetComponent<BoxCollider2D>();
        myrigidbody = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("rasenhit");
            myrigidbody.velocity = Vector2.zero;
        }


    }

    public void SetUp(Vector2 velocity, Vector3 direction)
    {
        myrigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);

    }
}
