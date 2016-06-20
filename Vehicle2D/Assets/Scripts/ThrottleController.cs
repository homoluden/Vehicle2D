using UnityEngine;
using System.Collections;

public class ThrottleController : MonoBehaviour
{
    private Rigidbody2D _carBody;

    public float Force = 5.0f;
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
            var leftForce = LeftWheel.transform.up * Force;
            var rightForce = RightWheel.transform.up * Force;

            LeftWheel.AddForce(leftForce, ForceMode2D.Impulse);
            RightWheel.AddForce(rightForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            var leftForce = -LeftWheel.transform.up * Force;
            var rightForce = -RightWheel.transform.up * Force;

            LeftWheel.AddForce(leftForce, ForceMode2D.Impulse);
            RightWheel.AddForce(rightForce, ForceMode2D.Impulse);
        }
    }
}
