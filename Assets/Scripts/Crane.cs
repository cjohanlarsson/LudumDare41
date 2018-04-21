using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour 
{
	[SerializeField] Rigidbody craneNeck;
	[SerializeField] Rigidbody craneBase;
	[SerializeField] float forceAmountCraneBase;
	[SerializeField] Transform forcePosition;

	[SerializeField] Rigidbody kinemat;
	[SerializeField] HingeJoint hinge;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		/*if(Input.GetKey(KeyCode.Q))
		{
			craneNeck.AddForceAtPosition( forcePosition.transform.right  * forceAmountCraneBase , forcePosition.transform.position );
		}
		else if(Input.GetKey(KeyCode.W))
		{
			craneNeck.AddForceAtPosition( forcePosition.transform.right  * (-1 * forceAmountCraneBase) , forcePosition.transform.position );
		}

		if(Input.GetKey(KeyCode.RightArrow))
		{
			craneBase.AddForce( craneBase.transform.right * forceAmountCraneBase );
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			craneBase.AddForce( craneBase.transform.right * (-1 * forceAmountCraneBase) );
		}
		else if(Input.GetKey(KeyCode.DownArrow))
		{
			craneBase.AddForce( craneBase.transform.forward * (-1 * forceAmountCraneBase) );
		}
		else if(Input.GetKey(KeyCode.UpArrow))
		{
			craneBase.AddForce( craneBase.transform.forward * ( forceAmountCraneBase) );
		}*/


		if(Input.GetKey(KeyCode.Q))
		{
			kinemat.transform.Rotate( new Vector3(0,-45f,0) * Time.fixedDeltaTime );
		}
		else if(Input.GetKey(KeyCode.W))
		{
			kinemat.transform.Rotate( new Vector3(0,45f,0) * Time.fixedDeltaTime );
		}

		if(Input.GetKey(KeyCode.A))
		{
			kinemat.transform.position += (Time.fixedDeltaTime * 4f * Vector3.up);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			kinemat.transform.position += (Time.fixedDeltaTime * 4f * Vector3.down);
		}

		if(Input.GetKey(KeyCode.RightArrow))
		{
			kinemat.transform.position += (Time.fixedDeltaTime * 4f * Vector3.right);
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			kinemat.transform.position += (Time.fixedDeltaTime * -4f * Vector3.right);
		}

		if(Input.GetKey(KeyCode.DownArrow))
		{
			kinemat.transform.position += ( Time.fixedDeltaTime * -4f * Vector3.forward );
		}
		else if(Input.GetKey(KeyCode.UpArrow))
		{
			kinemat.transform.position += ( Time.fixedDeltaTime * 4f * Vector3.forward );
		}

		if(Input.GetKey(KeyCode.Space))
		{
			GameObject.Destroy(this.hinge);
		}
		/*
		if(Input.GetKey(KeyCode.RightArrow))
		{
			craneBase.AddForce( this.transform.right * forceAmountCraneBase );
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			craneBase.AddForce( this.transform.right * (-1 * forceAmountCraneBase) );
		}
		else if(Input.GetKey(KeyCode.DownArrow))
		{
			craneBase.AddForce( this.transform.forward * (-1 * forceAmountCraneBase) );
		}
		else if(Input.GetKey(KeyCode.UpArrow))
		{
			craneBase.AddForce( this.transform.forward * ( forceAmountCraneBase) );
		}*/
	}
}
