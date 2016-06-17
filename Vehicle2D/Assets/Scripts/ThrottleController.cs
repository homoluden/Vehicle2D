using UnityEngine;
using System.Collections;

public class ThrottleController : MonoBehaviour
{
    private Rigidbody2D _carBody;

    public float Force = 5.0f;
    public float RotationBalance = 0.1f;
    public Rigidbody2D LeftWheel;
    public Rigidbody2D RightWheel;

    // Use this for initialization
    void Start () {
        _carBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            var force = Vector2.up * Force * LeftWheel.mass;
            //LeftWheel.AddRelativeForce(, ForceMode2D.Force);
            //RightWheel.AddRelativeForce(Vector2.up * Force * RightWheel.mass, ForceMode2D.Force);

            _carBody.AddForce(force, ForceMode2D.Impulse);
            //_carBody.AddTorque(-RotationBalance * (LeftWheel.mass + RightWheel.mass), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            var force = Vector2.down * Force * LeftWheel.mass;
            //LeftWheel.AddRelativeForce(Vector2.down * Force * LeftWheel.mass, ForceMode2D.Force);
            //RightWheel.AddRelativeForce(Vector2.down * Force * RightWheel.mass, ForceMode2D.Force);

            _carBody.AddForce(force, ForceMode2D.Impulse);
            //_carBody.AddTorque(RotationBalance * (LeftWheel.mass + RightWheel.mass), ForceMode2D.Force);
        }
    }
}
