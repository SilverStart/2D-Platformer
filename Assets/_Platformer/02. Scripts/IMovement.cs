using UnityEngine;

public interface IMovement
{
    InputType inputType { get; set; }
    
    void InputJoystick(float h, float v);
    void Move();
    void SetAnimation();
    void Jump();
}