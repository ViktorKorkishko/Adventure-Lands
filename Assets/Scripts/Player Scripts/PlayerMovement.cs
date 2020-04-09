using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{

    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;

    //TODO HEALTH Break off the health system into its own component
    /*
    public FloatValue currentHealth;
    public mySignal playerHealthSignal;
    */
    public VectorValue startingPosition;

    //TODO INVENTORY Break off the player inventory into its own componrnt
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;

    //TODO Player HEALTH hit should be part of the health system?
    public mySignal playerHit;

    //TODO MAGIC Player magic should be part of magic system
    public mySignal reduceMagic;

    //TODO IFRAME Break off the iFrame stuff into its own script
    [Header("IFrame Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    //TODO ABILITY Break this off with the player ability system
    [Header("Projectile stuff")]
    public GameObject projectile;
    public Item bow;

    void Start()
    {
        transform.position = startingPosition.initialValue;
        currentState = PlayerState.walk;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("horizontal", 0f);
        animator.SetFloat("vertical", -1f);
    }

    void Update()
    {
        if (currentState == PlayerState.interact)
        {
            return;
        }
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Weapon Attack") &&
        currentState != PlayerState.attack &&
        currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        //TODO ABILITY
        else if (Input.GetButtonDown("Ability") &&
        currentState != PlayerState.attack &&
        currentState != PlayerState.stagger)
        {
            if (playerInventory.CheckForItem(bow))
            {
                StartCoroutine(SecondAttackCo());
            }
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    //TODO ABILITY
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator SecondAttackCo()
    {
        animator.SetBool("fire", true);
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();
        animator.SetBool("fire", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    //TODO ABILITY it should be part of the ability itself
    private void MakeArrow()
    {
        if (playerInventory.currentMagic > 0)
        {
            Vector2 temp = new Vector2(animator.GetFloat("horizontal"), animator.GetFloat("vertical"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.SetUp(temp, ChooseArrowDirection());
            playerInventory.ReduceMagic(arrow.magicCost);
            reduceMagic.Raise();
        }
    }

    //TODO ABILITY this should also be part of the ability 
    public Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("vertical"), animator.GetFloat("horizontal")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }


    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receiveItem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("horizontal", change.x);
            animator.SetFloat("vertical", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        myRigidBody.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
    }

    // TODO KNOCKBACK move the knockbakc to its own script
    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
        //TODO HEALTH
        /*
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            //TODO HEALTH
            playerHit.Raise();

        }
        else
        {
            this.gameObject.SetActive(false);
        }
        */
    }

    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();
        if (myRigidBody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }

    // TODO IFRAME move the player flashing to its own script
    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while(temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
}
