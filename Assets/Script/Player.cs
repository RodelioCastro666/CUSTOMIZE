using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;

    private GameObject A;

    private float initiHealth = 100;

    private float initiMana = 50;

    [SerializeField]
    private Transform[] exitPoints;

    private int exitIndex = 2;

    [SerializeField]
    private GameObject[] spellPrefab;

    

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




        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack());
        }
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

        Spell spell = Instantiate(spellPrefab[0], exitPoints[exitIndex].position, Quaternion.identity).GetComponent<Spell>();
        spell.SetUp(temp, ChooseSpellDirection());

    }

    Vector3 ChooseSpellDirection()
    {
        float temp = Mathf.Atan2(myAnimator.GetFloat("y"), myAnimator.GetFloat("x")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp + 180);
    }
}
