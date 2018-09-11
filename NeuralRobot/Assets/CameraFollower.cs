using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var target = GameObject.Find("Box001").transform;
        if(target != null)
	        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

	}
}
