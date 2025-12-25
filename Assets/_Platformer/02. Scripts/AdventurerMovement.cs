using System;
using Platformer;
using UnityEngine;

public class AdventurerMovement : MonoBehaviour, IMovement
{
    [field: SerializeField] public InputType inputType { get; set; }

    private SoundManager sound;

    private Animator anim;
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;

    private float h, v;
    
    private bool isLadder;
    public bool IsLadder
    {
        get
        {
            return isLadder;
        }
        set
        {
            isLadder = value;

            if (isLadder)
                rb.gravityScale = 0;
            else
                rb.gravityScale = 1;
        }
    }

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpPower = 7f;

    private IInteractable currentInteractable;

    void Start()
    {
        sound = FindFirstObjectByType<SoundManager>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (inputType == InputType.Keyboard)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetButtonDown("Jump"))
            Jump();
    }

    void FixedUpdate()
    {
        SetAnimation();
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        InteractionEvent(other, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        InteractionEvent(other, false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        InteractionEvent(other.collider, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        InteractionEvent(other.collider, false);
    }

    private void InteractionEvent(Collider2D other, bool isEnter)
    {
        if (isEnter)
        {
            if (currentInteractable != other.GetComponent<IInteractable>())
            {
                if (currentInteractable != null)
                    currentInteractable.UnInteract();

                currentInteractable = other.GetComponent<IInteractable>();
            }

            if (currentInteractable != null)
                currentInteractable.Interact(transform);
        }
        else
        {
            if (currentInteractable != null)
                currentInteractable.UnInteract ();
        }
    }

    public void InputJoystick(float h, float v)
    {
        if (inputType == InputType.Joystick)
        {
            this.h = h;
            this.v = v;
        }
    }

    public void Move()
    {
        rb.linearVelocityX = h * moveSpeed;

        if (IsLadder)
            rb.linearVelocityY = v * jumpPower;
    }

    public void Jump()
    {
        if (anim.GetBool("IsGround"))
        {
            sound.SoundOneShot("Attack1");

            anim.SetBool("IsGround", false);
            anim.SetTrigger("Jump");
            rb.AddForceY(jumpPower, ForceMode2D.Impulse);
        }
    }

    public void SetAnimation()
    {
        if (h != 0)
        {
            float scaleX = 0f;
            if (h > 0)
                scaleX = 1;
            else if (h < 0)
                scaleX = -1;

            // float scaleX = h > 0 ? 1 : -1;

            transform.localScale = new Vector3(scaleX, 1, 1);
        }

        if (v < -0.45f) // Crouch 상태
        {
            coll.offset = new Vector2(coll.offset.x, 0.55f);
            coll.size = new Vector2(coll.size.x, 1.1f);
            moveSpeed = 1f;
        }
        else if (v >= -0.45f) // Idle 상태
        {
            coll.offset = new Vector2(coll.offset.x, 0.87f);
            coll.size = new Vector2(coll.size.x, 1.6f);
            moveSpeed = 3f;
        }

        anim.SetFloat("AxisX", h);
        anim.SetFloat("AxisY", v);
    }

    public void FootStep()
    {
        sound.SoundOneShot("Footstep Field");
    }

    public void SetGroundAnimation(bool isGround)
    {
        if (isGround)
        {
            sound.SoundOneShot("Footstep Grass");
            anim.SetBool("IsGround", true);
        }
        else
        {
            anim.SetBool("IsGround", false);
        }
    }
}