using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;

    

    private float initiHealth = 100;

    private float initiMana = 50;

    [SerializeField]
    private Transform[] exitPoints;

    private int exitIndex = 2;

    [SerializeField]
    private GameObject[] spellPrefab;

    private Vector3 min, max;

    protected override void Start()
    {
        health.Initialized(initiHealth, initiHealth);
        mana.Initialized(initiMana, initiMana);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {

        GetInput();

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x),
            Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);

        base.Update();

        
    }



    private void GetInput()
    {
        direction = Vector2.zero;


        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");


        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 0;
        }
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
        }
        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
        }
        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }




        
        
        
    }

    public void SetLimits(Vector3 min,Vector3 max)
    {
        this.min = min;
        this.max = max;
    }

    private void MoveCharacter()
    {

    }

    private IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            myAnimator.SetBool("attack", true);

            yield return new WaitForSeconds(0.5f);

            CastSpell();

            StopAttack();
        }

    }

    public void CastSpell()
    {
        Vector2 temp = new Vector2(myAnimator.GetFloat("x"), myAnimator.GetFloat("y"));

        Spell spell = Instantiate(spellPrefab[0], transform.position, Quaternion.identity).GetComponent<Spell>();
        spell.SetUp(temp, ChooseSpellDirection());

    }

    private IEnumerator AttackSword()
    {
        if (!isAttackingSword)
        {
            isAttackingSword = true;
            myAnimator.SetBool("attackSword", true);

            yield return new WaitForSeconds(0.3f);
            Debug.Log("kkk");


            SwordSlash();

            StopAttackSword();

            
        }
    }

    private IEnumerator AttackRasen()
    {
        if (!isAttackingRasen)
        {
            isAttackingRasen = true;
            myAnimator.SetBool("attack2", true);

            yield return new WaitForSeconds(0.3f);



            Rasen();

            StopAttackRasen();


        }
    }

    public void Rasen()
    {
        Vector2 temp = new Vector2(myAnimator.GetFloat("x"), myAnimator.GetFloat("y"));

        RasenSpell rasenSpell= Instantiate(spellPrefab[2], exitPoints[exitIndex].position, Quaternion.identity).GetComponent<RasenSpell>();
        rasenSpell.SetUp(temp, ChooseSlashDirection());
    }


    public void SwordSlash()
    {
        Vector2 temp = new Vector2(myAnimator.GetFloat("x"), myAnimator.GetFloat("y"));

        SwordSlash swordSlash = Instantiate(spellPrefab[1], exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SwordSlash>();
        swordSlash.SetUp(temp, ChooseSlashDirection());
    }   

    Vector3 ChooseSpellDirection()
    {
        float temp = Mathf.Atan2(myAnimator.GetFloat("y"), myAnimator.GetFloat("x")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp+180);
    }

    Vector3 ChooseSlashDirection()
    {
        float temp = Mathf.Atan2(myAnimator.GetFloat("y"), myAnimator.GetFloat("x")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    public void CastRasen()
    {
        StartCoroutine(AttackRasen());
    }

    public void CastSword()
    {
        StartCoroutine(AttackSword());
    }
    public void CastFire()
    {
        StartCoroutine(Attack());
    }
}
