using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public float maxSpeed = 10f;
    Rigidbody2D myRB;
    bool facingRight = false;

    Animator anim;


    bool grounded;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    int jumpForce = 10;

    public float move;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move = Input.GetAxis("Horizontal");

        anim.SetFloat("speed", Mathf.Abs(move));

        myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);

        //Revisar si esta tocando el suelo
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("grounded", grounded);

        if (move < 0 && !facingRight)
        {
            Flip();
        }
        else if (move > 0 && facingRight)
        {
            Flip();
        }

    }

    void Update(){
        if(grounded && Input.GetKeyDown(KeyCode.UpArrow)){
            //anim.SetBool("grounded", false);
            myRB.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
