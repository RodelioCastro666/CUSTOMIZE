using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField]
    private float speed;



    private Animator anim;
    // private BoxCollider2D boxCollider;

    [SerializeField]
    private Rigidbody2D myrigidbody;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        // boxCollider = GetComponent<BoxCollider2D>();
        myrigidbody = GetComponent<Rigidbody2D>();

    }


    // Start is called before the first frame update
    //void Start()
    //{
    //    anim = GetComponent<Animator>();
    //}

    // Update is called once per frame
    public void SetUp(Vector2 velocity, Vector3 direction)
    {
        myrigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }



}
