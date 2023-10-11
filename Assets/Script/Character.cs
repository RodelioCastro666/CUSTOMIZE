using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    protected Vector2 direction;

    protected Animator myAnimator;

    private Rigidbody2D myRigidbody2D;

    protected Coroutine attackRoutine;

    protected bool isAttacking = false;

    protected bool isAttackingSword = false;

    public bool Ismoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        myRigidbody2D.velocity = direction.normalized * speed;
    }

    public void HandleLayers()
    {

        if (isAttacking)
        {
            ActivateLayer("Attack");
            direction = Vector2.zero;


        }
        else if (isAttackingSword)
        {
            ActivateLayer("AttackSword");
            direction = Vector2.zero;
        }
        else if (Ismoving)
        {

            ActivateLayer("Walk");

            myAnimator.SetFloat("y", direction.y);
            myAnimator.SetFloat("x", direction.x);

            //StopAttack();
            StopAttackSword();

        }
        else
        {
            ActivateLayer("Idle");
        }
    }


    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    public void StopAttack()
    {


        // StopCoroutine(attackRoutine);
        isAttacking = false;
        myAnimator.SetBool("attack", isAttacking);


    }

    public void StopAttackSword()
    {


        // StopCoroutine(attackRoutine);
        isAttackingSword = false;
        myAnimator.SetBool("attackSword", isAttackingSword);


    }



}
