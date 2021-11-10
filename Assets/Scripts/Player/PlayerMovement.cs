using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D boxCollider;

    [Header("Movement and jumping")]
    public float speed;
    public float jumpForce;
    public float jumpButtonReleaseDamping;
    public float extraHeight;
    public float hangTime;
    private float hangCounter;
    private float h;

    [Space(10)]
    [Header("Aiming transforms")]
    public Transform aimHorizontal;
    public Transform aimUp;
    public Transform aimDiagonally;
    public Transform aimDucking;
    public Transform aimDownjumping;
    public Transform currentAimingPoint;

    private bool isAimingDiagonally;

    [Header("Animation")]
    public Animator torso;
    public Animator legs;

    [SerializeField]
    private LayerMask platformLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentAimingPoint = aimHorizontal;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // Change the onground animation
        ChangeTorsoAnimation("onGround", IsGrounded());
        ChangeLegsAnimation("onGround", IsGrounded());

        #region walk animation
        if (Input.GetAxis("Horizontal") != 0)
        {
            ChangeTorsoAnimation("isRunning", true);
            ChangeLegsAnimation("isRunning", true);
        }
        else
        {
            ChangeTorsoAnimation("isRunning", false);
            ChangeLegsAnimation("isRunning", false);
        }
        #endregion

        #region crouch and aiming down
        if (Input.GetAxis("Vertical") < 0)
        {
            // For crouching
            if (IsGrounded())
            {
                ChangeTorsoAnimation("isCrouching", true);
                ChangeLegsAnimation("isCrouching", true);
            }
            // For Aiming down in the air
            else if (!IsGrounded())
            {
                ChangeTorsoAnimation("isCrouching", true);
                ChangeLegsAnimation("isCrouching", false);
            }
        }
        else
        {
            ChangeTorsoAnimation("isCrouching", false);
            ChangeLegsAnimation("isCrouching", false);
        }
        #endregion

        #region aiming up
        if (Input.GetAxis("Vertical") > 0)
        {
            ChangeTorsoAnimation("aimingUp", true);
        }
        else
        {
            ChangeTorsoAnimation("aimingUp", false);
        }
        #endregion

        #region groundCheck
        //check for grounded and add some hangtime to still jump if not on platform annymore
        if (IsGrounded())
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
        #endregion

        #region jumping
        //jumping
        if (Input.GetButtonDown("Jump") && hangCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpButtonReleaseDamping);
        }
        #endregion

        #region flipping
        //flipping the transform to face the correct side to shoot
        if (GetHorizontalAxis() >= 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (GetHorizontalAxis() <= -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        #endregion

        #region Shoot
        // shooting the weapon
        if (Input.GetAxis("Fire1") >= 0.001f)
        {
            Shoot();
        }
        #endregion
    }

    private void FixedUpdate()
    {
        //moving
        transform.Translate(h, 0, 0);
    }

    private bool IsGrounded()
    {
        Vector2 size = new Vector2(boxCollider.bounds.size.x - 0.1f, boxCollider.bounds.size.y);
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, size, 0f, Vector2.down, extraHeight, platformLayerMask);

        Color raycolor;
        if (raycastHit.collider != null)
        {
            raycolor = Color.green;
        }
        else
        {
            raycolor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.y + extraHeight), raycolor);

        return raycastHit.collider != null;
    }

    void Shoot()
    {
        print("PEW POW");
    }

    // For changing torso animations
    void ChangeTorsoAnimation(string action, bool value)
    {
        torso.SetBool(action, value);
    }

    // For changing legs animations
    void ChangeLegsAnimation(string action, bool value)
    {
        legs.SetBool(action, value);
    }

    private float GetHorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }

    private float GetVerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }
}
