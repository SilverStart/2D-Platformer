using System;
using Platformer;
using UnityEngine;

public class TownMovement : MonoBehaviour, IMovement
{
    [field:SerializeField]
    public InputType inputType { get; set; }

    private SoundManager sound;
    
    private Animator anim;
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;

    private IInteractable currentInteractable;
    
    private float h, v;

    [SerializeField] private float moveSpeed = 3f;
    
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
    }

    void FixedUpdate()
    {
        SetAnimation();
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentInteractable = other.GetComponent<IInteractable>();
        
        if (currentInteractable != null)
            currentInteractable.Interact(transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentInteractable != null)
            currentInteractable.UnInteract();
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
        Vector2 dir = new Vector2(h, v);
        rb.linearVelocity = dir * moveSpeed;
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

        anim.SetFloat("AxisX", h);
        anim.SetFloat("AxisY", v);
    }

    public void Jump()
    {
        // Town에서는 사용 X
    }
    public void FootStep()
    {
        sound.SoundOneShot("Footstep Field");
    }
}