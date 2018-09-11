using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	private Rigidbody2D rbBody;
    private Transform headTransform;
    private float upwardsAngle;

    //[HideInInspector]
    public NeuralNetwork Nn;

	// Use this for initialization
	void Start () {
	    rbBody = GameObject.Find("Box001").GetComponent<Rigidbody2D>();
	    headTransform = GameObject.Find("Object004").transform;
	    upwardsAngle = transform.rotation.eulerAngles.x;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    if (headTransform == null || rbBody == null)
	    {
	        rbBody = GameObject.Find("Box001").GetComponent<Rigidbody2D>();
	        headTransform = GameObject.Find("Object004").transform;
        }
	    if (headTransform != null && rbBody != null)
	    {
	        float[] inputs = new float[2];
	        inputs[0] = rbBody.velocity.x;
	        inputs[1] = headTransform.GetComponent<Rigidbody2D>().angularVelocity;

	        //inputs[1] %= 360f;
	        //if (inputs[1] < 0)
	        //{
	        //    inputs[1] += 360f;
	        //}

	        //inputs[1] += 90f;
	        //inputs[1] /= 90f;
            //Debug.Log(inputs[1]);

	        float[] output = Nn.FeedForward(inputs);
	        rbBody.angularVelocity = output[0] * 500;
            //rbBody.AddTorque(output[0] * 500);
	    }
	}
}
