using UnityEngine;
using System.Collections;

public class Tire : MonoBehaviour {

    private Rigidbody2D _body;

    public float Friction = 600.0f;

	// Use this for initialization
	void Start () {
        _body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        var lateralDir = _body.transform.right;
        var lateralVel = Vector3.Project(_body.velocity, lateralDir);
        _body.AddForce(-lateralVel * _body.mass * Friction);
	}
}
