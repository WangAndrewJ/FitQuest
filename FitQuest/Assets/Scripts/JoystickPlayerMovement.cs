using UnityEngine;

public class JoystickPlayerMovement : MonoBehaviour
{
    private int speed;
    public int defaultSpeed;
    public VariableJoystick variableJoystick;
    public Rigidbody2D myRigidbody2D;

    private void Start()
    {
        //speed = PlayerPrefs.GetInt("Speed", defaultSpeed);
        speed = defaultSpeed;
    }

    public void FixedUpdate()
    {
        Vector2 direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;
        myRigidbody2D.velocity = direction * speed;
    }
}
