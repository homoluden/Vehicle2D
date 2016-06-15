using UnityEngine;
using System.Collections;

public class SteeringController : MonoBehaviour
{
    private Rigidbody2D _carBody;
    private float _minForce = 0.0f;

    public float Force = 0.5f;
    public float MaxAngle = 60.0f;
    public Rigidbody2D LeftWheel;
    public Rigidbody2D RightWheel;
    
	void Start ()
	{
	    _carBody = GetComponent<Rigidbody2D>();
        _minForce = Force * 0.1f;
        MaxAngle = Mathf.Abs(MaxAngle);
	}
	
	void Update ()
    {
        float angleLeft = Quaternion.Angle(_carBody.transform.rotation, LeftWheel.transform.rotation);
	    
	    float angleRight = Quaternion.Angle(_carBody.transform.rotation, RightWheel.transform.rotation);
	    
        var angleAvg = Mathf.Abs((angleLeft + angleRight) * 0.5f);
        
        var force = LerpClamp(_minForce, Force, (Mathf.Abs(angleAvg - MaxAngle)) / MaxAngle);

        //Debug.Log(string.Format("Left: {0}   ||   Right: {1}", angleLeft, angleRight));

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
	    {
            // Turning Left
            LeftWheel.AddTorque(force * LeftWheel.mass, ForceMode2D.Impulse);
            RightWheel.AddTorque(force * RightWheel.mass, ForceMode2D.Impulse);
            _carBody.AddTorque(-force * (LeftWheel.mass + RightWheel.mass) * 0.5f, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            // Turning Right
            LeftWheel.AddTorque(-force * LeftWheel.mass, ForceMode2D.Impulse);
            RightWheel.AddTorque(-force * RightWheel.mass, ForceMode2D.Impulse);
            _carBody.AddTorque(force * (LeftWheel.mass + RightWheel.mass) * 0.5f, ForceMode2D.Impulse);
        }
    }
    
    private float LerpClamp(float min, float max, float t)
    {
        if (min >= max || t >= 1.0f)
        {
            return max;
        }

        if (t <= 0.0f)
        {
            return min;
        }

        var diff = max - min;

        return min + diff * t;
    }
}
