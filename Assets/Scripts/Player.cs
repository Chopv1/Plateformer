using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyWasPressed = false;
    private bool canJump = true;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    public int superJumpsRemaining = 0;
    private bool canMove = true;
    private bool canAttack = true;

    private int maxHp;
    private int hp;
    private int damage;
    public int ennemiMort=0;
    
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask ennemiMask;
    public GameObject over;
    public GameObject showSuperJump;
    public GameObject showEnnemi;
    


    // Start is called before the first frame update
    void Start()
    {
        damage = 20;
        maxHp = 100;
        hp = maxHp;
        rigidbodyComponent = GetComponent<Rigidbody>();
        showJump();
        EnnemiMort();
    }

    // Update is called once per frame
    void Update()
    {
        //When Space/Z/UpArrow is pressed jump
        if ((Input.GetKeyDown(KeyCode.Z)) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            jumpKeyWasPressed = true;
        }
        if(canMove)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }
        Die();
        if ((Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0)) && canAttack) 
        {
            Attaque();
        }
        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            SuperJump();
        }
        showJump();
        EnnemiMort();
    }
    public void SuperJump()
    {
        if (superJumpsRemaining > 0)
        {
            float jumpPower = 5f;

            jumpPower *= 2;
            superJumpsRemaining--;
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);

        }
    }
    void FixedUpdate()
    {
        if (horizontalInput != 0)
        {
            
            Move();
        }

        if (jumpKeyWasPressed && canJump)
        {
            canJump = false;
            Jump();
        }
        

    }
    void Jump()
    {
        float jumpPower = 5f;
        rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
    }
    void Move()
    {
        if (horizontalInput > 0)
        {
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        }
        rigidbodyComponent.velocity = new Vector3(horizontalInput*3f, rigidbodyComponent.velocity.y, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            jumpKeyWasPressed = false;
            canJump = true;
            Debug.Log("Can Jump");
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

    public void Attaque()
    {
        Collider[] colliders = Physics.OverlapSphere(attackPoint.transform.position ,attackRange, ennemiMask);
        Debug.Log(colliders.Length);

        foreach (Collider collider in colliders)
        {
            collider.gameObject.GetComponent<Ennemi>().TakeDamage(damage);
            Debug.Log("Attaque");
        }
    }
    public void Die()
    {
        if(hp<=0 || transform.position.y < -5)
        {
            hp = 0;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().mass = 0;
            canMove = false;
            canJump = false;
            canAttack = false;
            over.GetComponent<LoadScene>().Over();

        }
    }
    private void OnDrawGizmosSelected()
    {
        if(attackPoint ==null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0) Invoke(nameof(Die), 0.5f);
    }
    public void GG()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().mass = 0;
        canMove = false;
        canJump = false;
        canAttack = false;
    }

    public void showJump()
    {
        showSuperJump.GetComponent<Text>().enabled = true;
        showSuperJump.GetComponent<Text>().text = $"Nombre de super jump restant: {superJumpsRemaining}";
    }
    public void EnnemiMort()
    {
        showEnnemi.GetComponent<Text>().enabled = true;
        showEnnemi.GetComponent<Text>().text = $"Nombre ennemie tuée: {ennemiMort}";
    }
    public void UnMort()
    {
        ennemiMort += 1;
    }
}
