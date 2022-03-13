using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyWasPressed=false;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining=0;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //When Space/Z/UpArrow is pressed jump
        if ((Input.GetKeyDown(KeyCode.Space))||(Input.GetKeyDown(KeyCode.Z))||(Input.GetKeyDown(KeyCode.UpArrow)))
        {
            jumpKeyWasPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }
    void FixedUpdate()
    {
        if (horizontalInput != 0)
        {
            Move();
        }
        if (Physics.OverlapSphere(groundCheckTransform.position,0.1f, playerMask).Length==0)
        {
            return;
        }
        if (jumpKeyWasPressed)
        {
            
            Jump();
            jumpKeyWasPressed = false;
        }
       
       
    }
    void Jump()
    {
        float jumpPower = 5f;
        if(superJumpsRemaining>0)
        {
            jumpPower *= 2;
            superJumpsRemaining--;
        }
        rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
    }
    void Move()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }


}
