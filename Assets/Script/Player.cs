using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
   

    [SerializeField]
    private Stat mana;

    

    private float initiMana = 50;

    [SerializeField]
    private Transform[] exitPoints;

    private int exitIndex = 2;

    private SpellBook spellBook;

    private Vector3 min, max;

    protected override void Start()
    {
        spellBook = GetComponent<SpellBook>();
        
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

   

    private IEnumerator Attack()
    {
        Spell newSpell = spellBook.CastSpell(0);

        if (!isAttacking)
        {
            isAttacking = true;
            myAnimator.SetBool("attack", true);

            yield return new WaitForSeconds(newSpell.MyCastTime);

            Vector2 temp = new Vector2(myAnimator.GetFloat("x"), myAnimator.GetFloat("y"));

            FireSpell spell = Instantiate(newSpell.MySpellPrefab, transform.position, Quaternion.identity).GetComponent<FireSpell>();
            spell.SetUp(temp, ChooseSpellDirection());
            spell.Initialize(newSpell.MyDamage);

            StopAttack();
        }

    }

   

    private IEnumerator AttackSword()
    {
        Spell newSpell = spellBook.CastSpell(1);

        if (!isAttackingSword)
        {
            isAttackingSword = true;
            myAnimator.SetBool("attackSword", true);

            yield return new WaitForSeconds(newSpell.MyCastTime);
            Debug.Log("kkk");


            Vector2 temp = new Vector2(myAnimator.GetFloat("x"), myAnimator.GetFloat("y"));

            SwordSlash swordSlash = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SwordSlash>();
            swordSlash.SetUp(temp, ChooseSlashDirection());
            swordSlash.Initialize(newSpell.MyDamage);

            StopAttackSword();

            
        }
    }

    private IEnumerator AttackRasen()
    {
        Spell newSpell = spellBook.CastSpell(2);

        if (!isAttackingRasen)
        {
            isAttackingRasen = true;
            myAnimator.SetBool("attack2", true);

            yield return new WaitForSeconds(newSpell.MyCastTime);

            Vector2 temp = new Vector2(myAnimator.GetFloat("x"), myAnimator.GetFloat("y"));

            RasenSpell rasenSpell = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<RasenSpell>();
            rasenSpell.SetUp(temp, ChooseSlashDirection());
            rasenSpell.Initialize(newSpell.MyDamage);

            StopAttackRasen();


        }
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

    public virtual void StopAttack()
    {
        spellBook.StopCasting();

        isAttacking = false;
        myAnimator.SetBool("attack", isAttacking);
    }

    public virtual void StopAttackSword()
    {
        spellBook.StopCasting();

        isAttackingSword = false;
        myAnimator.SetBool("attackSword", isAttackingSword);
    }

    public virtual void StopAttackRasen()
    {
        spellBook.StopCasting();

        // StopCoroutine(attackRoutine);
        isAttackingRasen = false;
        myAnimator.SetBool("attack2", isAttackingRasen);
    }

    
}
