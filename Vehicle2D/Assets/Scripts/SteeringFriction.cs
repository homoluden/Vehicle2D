using UnityEngine;
using System.Collections;

public class SteeringFriction : MonoBehaviour
{
    private Rigidbody2D _body;

    public float FrictionFactor = 0.01f;

	void Start () {
        _body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        var locVel = transform.InverseTransformDirection(_body.velocity);
        var dx = -locVel.x;

        _body.AddForce(FrictionFactor * dx * _body.mass * Vector2.right, ForceMode2D.Force);
    }
}
