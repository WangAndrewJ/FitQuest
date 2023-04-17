using UnityEngine;

public class JoystickPlayerMovement : MonoBehaviour
{
    private int speed;
    public int defaultSpeed;
    public VariableJoystick myVariableJoystick;
    public Rigidbody2D myRigidbody2D;

    private void Start()
    {
        //speed = PlayerPrefs.GetInt("Speed", defaultSpeed);
        speed = defaultSpeed;
    }

    public void FixedUpdate()
    {
        Vector2 direction = Vector2.up * myVariableJoystick.Vertical + Vector2.right * myVariableJoystick.Horizontal;
        myRigidbody2D.velocity = direction * speed;
    }
}
