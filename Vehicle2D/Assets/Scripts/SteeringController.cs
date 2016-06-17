using UnityEngine;
using System.Collections;
using System;

public class SteeringController : MonoBehaviour
{
    private Rigidbody2D _carBody;
    private float _minForce = 0.0f;

    public float SteeringForce = 0.5f;
    public float SteeringFriction = 0.5f;
    public float TargetAngle = 0.0f;
    public float MaxAngle = 60.0f;
    public Rigidbody2D LeftWheel;
    public Rigidbody2D RightWheel;
    
	void Start ()
	{
	    _carBody = GetComponent<Rigidbody2D>();
        _minForce = SteeringForce * 0.1f;
        MaxAngle = Mathf.Abs(MaxAngle);
	}
	
	void Update ()
    {
     //   float angleLeft = Quaternion.Angle(_carBody.transform.rotation, LeftWheel.transform.rotation);
	    
	    //float angleRight = Quaternion.Angle(_carBody.transform.rotation, RightWheel.transform.rotation);
	    
     //   var angleAvg = Mathf.Abs((angleLeft + angleRight) * 0.5f);
        
     //   var force = LerpClamp(_minForce, SteeringForce, (Mathf.Abs(angleAvg - MaxAngle)) / MaxAngle);

        //Debug.Log(string.Format("Left: {0}   ||   Right: {1}", angleLeft, angleRight));

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
	    {
            // Turning Left
            TargetAngle = Mathf.Min(MaxAngle, TargetAngle + 0.04f);
            //LeftWheel.AddTorque(force * LeftWheel.mass, ForceMode2D.Force);
            //RightWheel.AddTorque(force * RightWheel.mass, ForceMode2D.Force);
            //_carBody.AddTorque(-force * (LeftWheel.mass + RightWheel.mass) * 0.5f, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            // Turning Right
            TargetAngle = Mathf.Max(-MaxAngle, TargetAngle - 0.04f);
            //LeftWheel.AddTorque(-force * LeftWheel.mass, ForceMode2D.Force);
            //RightWheel.AddTorque(-force * RightWheel.mass, ForceMode2D.Force);
            //_carBody.AddTorque(force * (LeftWheel.mass + RightWheel.mass) * 0.5f, ForceMode2D.Force);
        }

        RotateWheels();
        ApplySteeringFriction();
    }

    private void RotateWheels()
    {
        float angleLeft = Quaternion.Angle(_carBody.transform.rotation, LeftWheel.transform.rotation);
        float angleRight = Quaternion.Angle(_carBody.transform.rotation, RightWheel.transform.rotation);

        Debug.Log(string.Format("Angle Left: {0}   ||   Angle Right: {1}", angleLeft, angleRight));

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

        LeftWheel.AddTorque(angleLeft / MaxAngle * LeftWheel.mass, ForceMode2D.Force);

        RightWheel.AddTorque(angleRight / MaxAngle * RightWheel.mass, ForceMode2D.Force);
    }

    private void ApplySteeringFriction()
    {
        var avgLatDir = ((LeftWheel.transform.right + RightWheel.transform.right) * 0.5f).normalized;
        var latVel = Vector3.Project(_carBody.velocity, avgLatDir);

        _carBody.AddForceAtPosition(latVel * SteeringFriction * (LeftWheel.mass + RightWheel.mass), new Vector2(0.0f, 0.6f), ForceMode2D.Force);
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
