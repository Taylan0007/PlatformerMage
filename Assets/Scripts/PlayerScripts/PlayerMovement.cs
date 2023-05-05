using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     private Rigidbody2D rigidBody;
    [SerializeField] private float speed;
    public float jump;

    private float moveHorizontal;
    private float moveVertical;

    public bool isGrounded;

    [Header("Dashing")]
    [SerializeField] private float _dashingVelocity = 1f;
    [SerializeField] private float _dashingTime = 0.5f;
    private Vector2 _dashDir;
    private bool _canDash;
    private float _dashCooldown = 0.5f;

    private bool canShoot;
    public float shootCooldown = 2f;
    

    
    private void Start()
    {
        canShoot = true;
        rigidBody = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
       

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Jump");

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            
            _canDash = false;
            _dashDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (_dashDir == Vector2.zero)
            {
                _dashDir = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(Dasher());
            StopAllCoroutines();
                                      
        }

        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Signals.Instance.OnSkillUse.Invoke("FireBall");
            StartCoroutine(Cooldown());

        }

       
        
       
        if (isGrounded)
        {
            _canDash = true;
        }
    }

    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rigidBody.AddForce(new Vector2(moveHorizontal * speed, 0f), ForceMode2D.Impulse);
        }

        if (isGrounded && moveVertical > 0.1f)
        {
            rigidBody.AddForce(new Vector2(0f, moveVertical * jump), ForceMode2D.Impulse);
        }

    }

    

    private IEnumerator Dasher()
    {
        while (true)
        {
            rigidBody.velocity = _dashDir.normalized * _dashingVelocity * _dashingTime;
            yield return new WaitForSeconds(_dashCooldown);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private IEnumerator Cooldown()
    {
        while (true)
        {
            canShoot = false;
            yield return new WaitForSeconds(shootCooldown);
            canShoot = true;
        }

    }

    

}
