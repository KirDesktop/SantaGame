using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject jumpP;
    [SerializeField] private Transform jumpPSpawn;

    [SerializeField] public Animator anim;
    public bool isConfused = false;
    public bool isDoubleJumping = false;
    //[SerializeField]private int jumpCount = 0;
    #region SerializeField Private Fileds

    [SerializeField] private GroundChecker _groundCheck;
    [SerializeField] private Transform graphics;



    [SerializeField] public float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpHeigtDouble;

    public bool isGrappled = false;

    #endregion

    #region Private Fields

    private Rigidbody2D _rb2d;

    private float _moveInput;

    //[SerializeField] private bool isJumping = false;
    //[SerializeField] private bool isJump = false;
    //[SerializeField] private bool sdd;

    #endregion

    #region MonoBehaviour Callbacks

    private bool once = false;
    private bool isG = false;
    private void Awake()
    {
        _rb2d = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isConfused)
        {
            _moveInput = -Input.GetAxisRaw("Horizontal");
        }
        else
        {
            _moveInput = Input.GetAxisRaw("Horizontal");
        }

        if (_moveInput != 0)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        if (_groundCheck.isGround && !once && !isG)
        {
            anim.SetBool("isJumping", false);
            isG = true;
        }

        if ((_groundCheck.isGround && ((Input.GetKey(KeyCode.Space)&&!isConfused) || 
            ((Input.GetKey(KeyCode.S) && isConfused))) && !isGrappled && !once))
        {
            anim.SetBool("isJumping", true);
            once = true;
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpHeight);
            _rb2d.drag = 0f;
            StartCoroutine("jump");

            Instantiate(jumpP, jumpPSpawn);
        }

        if (_moveInput > 0)
        {
            graphics.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (_moveInput < 0)
        {
            graphics.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

    }

    public void enableDoubleJump()
    {
        isDoubleJumping = true;
        jumpHeight = jumpHeigtDouble;
    }

    private IEnumerator jump()
    {
        yield return new WaitForSeconds(0.1f);
        once = false;
        isG = false;
    }

    private void FixedUpdate()
    {
        if (isGrappled) return;

        _rb2d.velocity = new Vector3(_moveInput * speed, _rb2d.velocity.y);
    }

    #endregion
}
