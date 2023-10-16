using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    [SerializeField]
    private float initiHealth;

    protected Vector2 direction;

    protected Animator myAnimator;

    private Rigidbody2D myRigidbody2D;

    protected Coroutine attackRoutine;

    [SerializeField]
    protected Stat health;

    [SerializeField]
    protected Transform hitBox;

    protected bool isAttacking = false;

    protected bool isAttackingSword = false;

    protected bool isAttackingRasen = false;



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
        health.Initialized(initiHealth, initiHealth);

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
        else if (isAttackingRasen)
        {
            ActivateLayer("Attack2");
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

    public virtual void StopAttack()
    {
        isAttacking = false;
        myAnimator.SetBool("attack", isAttacking);
    }

    public virtual void StopAttackSword()
    {
        isAttackingSword = false;
        myAnimator.SetBool("attackSword", isAttackingSword);
    }

    public virtual void StopAttackRasen()
    {
        // StopCoroutine(attackRoutine);
        isAttackingRasen = false;
        myAnimator.SetBool("attack2", isAttackingRasen);
    }

    public virtual void TakeDamage(float damage)
    {
        health.MyCurrentValue -= damage;

        if(health.MyCurrentValue <= 0)
        {
            myAnimator.SetTrigger("die");
        }
    }

}
