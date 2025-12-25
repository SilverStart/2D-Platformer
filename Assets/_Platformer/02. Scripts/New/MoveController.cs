using Platformer.New;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private IMoveable currentMoveable;

    private IJumpable currentJumpable;

    private IDashable currentDashable;

    private IGrappleable currentGrappleable;

    private float h;

    [SerializeField] private float doubleTapTimeThreshold = 0.3f;

    private float lastLeftTapTime;
    private float lastRightTapTime;

    [Header("Components")] public Camera mainCamera; // 메인 카메라

    // Update is called once per frame
    void Update()
    {
        DashInput();
        MoveInput();
        JumpInput();
        GrappleInput();
    }

    private void MoveInput()
    {
        h = 0f;
        if (currentMoveable == null) return;
        if (currentDashable?.IsDashing == true) return;
        if (currentGrappleable?.IsGrappling == true) return;

        h = Input.GetAxisRaw("Horizontal");
    }

    private void JumpInput()
    {
        if (currentMoveable == null) return;
        if (currentGrappleable?.IsGrappling == true) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentJumpable.Jump();
        }
    }

    private void DashInput()
    {
        if (currentDashable == null) return;
        if (currentGrappleable?.IsGrappling == true) return;
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            float timeSinceLastTap = Time.time - lastRightTapTime;
            if (timeSinceLastTap <= doubleTapTimeThreshold)
            {
                currentDashable.Dash(Vector2.right.x, 0);
            }

            lastRightTapTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            float timeSinceLastTap = Time.time - lastLeftTapTime;
            if (timeSinceLastTap <= doubleTapTimeThreshold)
            {
                currentDashable.Dash(Vector2.left.x, 0);
            }

            lastLeftTapTime = Time.time;
        }
    }

    private void GrappleInput()
    {
        if (currentGrappleable == null) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            currentGrappleable.ShootGrapple(mainCamera.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentGrappleable.CutGrapple();
        }
    }

    private void FixedUpdate()
    {
        if (h != 0) currentMoveable?.Move(h, 0);
    }

    public void SetMoveable(IMoveable moveable)
    {
        currentMoveable = moveable;
    }

    public void SetJumpable(IJumpable jumpable)
    {
        currentJumpable = jumpable;
    }

    public void SetDashable(IDashable dashable)
    {
        currentDashable = dashable;
    }

    public void SetGrappleable(IGrappleable grappleable)
    {
        currentGrappleable = grappleable;
    }
}