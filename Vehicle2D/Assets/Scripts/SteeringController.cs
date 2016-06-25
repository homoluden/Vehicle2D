using UnityEngine;

public class SteeringController : MonoBehaviour
{
    private Rigidbody2D _carBody;
    private float _minForce = 0.0f;

    public float SteeringForce = 100.0f;
    public float TargetAngle = 0.0f;
    public float MaxAngle = 60.0f;
    public Rigidbody2D LeftWheel;
    public Rigidbody2D RightWheel;
    public HingeJoint2D LeftHinge;
    public HingeJoint2D RightHinge;
    
	void Start ()
	{
	    _carBody = GetComponent<Rigidbody2D>();
        _minForce = SteeringForce * 0.05f;
        MaxAngle = Mathf.Abs(MaxAngle);
	}
	
	void Update ()
    {
        //Debug.Log(string.Format("Left: {0}   ||   Right: {1}", angleLeft, angleRight));

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
	    {
            // Turning Left
            TargetAngle = Mathf.Min(MaxAngle, TargetAngle + 0.6f);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            // Turning Right
            TargetAngle = Mathf.Max(-MaxAngle, TargetAngle - 0.6f);
        }

        RotateWheels();
    }

    private void RotateWheels()
    {
        float angleLeft = LeftHinge.jointAngle;
        float angleRight = RightHinge.jointAngle;

        //Debug.Log(string.Format("Angle Left: {0}   ||   Angle Right: {1}", angleLeft, angleRight));

        if (angleRight > 180.0f)
        {
            angleRight = 360.0f - angleRight;
        }

        if (angleLeft > 180.0f)
        {
            angleLeft = 360.0f - angleLeft;
        }

        angleLeft = TargetAngle - angleLeft;
        angleRight = TargetAngle - angleRight;
        
        LeftHinge.motor = new JointMotor2D { motorSpeed = angleLeft * 7.5f, maxMotorTorque = LerpClamp(_minForce, SteeringForce, angleLeft / MaxAngle) };
        RightHinge.motor = new JointMotor2D { motorSpeed = angleRight * 7.5f, maxMotorTorque = LerpClamp(_minForce, SteeringForce, angleRight / MaxAngle) };
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
