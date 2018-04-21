using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour 
{
	[SerializeField] Rigidbody craneArm;
	[SerializeField] float craneMaxSpeed;
	[SerializeField] float craneSmoothTime;
	[SerializeField] float craneMinY = -4;
	[SerializeField] float craneMaxY = 4;
	[SerializeField] float craneYSpeed = 6;
	[SerializeField] float craneYAccel = 6;
	[SerializeField] float craneYDeccel = 6;
	[SerializeField] float craneMouseOffset = 5f;

	[SerializeField] HingeJoint hinge;

	Vector3 craneArmTargetPos;
	Vector3 craneArmVelocity;

	float direction = 0;

	void Update()
	{
		var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		float enter;
		Vector3 targetPos;
		if( (new Plane(Vector3.up, 0)).Raycast( ray, out enter) )
		{
			targetPos = ray.GetPoint(enter);
			this.craneArmTargetPos.x = targetPos.x;
			this.craneArmTargetPos.z = targetPos.z - craneMouseOffset;
		}

		if(Input.GetMouseButton(0))
		{
			direction += Time.deltaTime * craneYAccel;
		}
		else if(Input.GetMouseButton(1))
		{
			direction -= Time.deltaTime * craneYAccel;
		}
		else
		{
			direction = Mathf.MoveTowards( direction, 0, Time.deltaTime * craneYDeccel );
		}
		direction = Mathf.Clamp(direction,-1,1);

		craneArmTargetPos.y = Mathf.Clamp( craneArmTargetPos.y + (direction * Time.deltaTime * craneYSpeed) ,craneMinY, craneMaxY);
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		var craneArmCurrPos = this.craneArm.transform.position;
		craneArmCurrPos = Vector3.SmoothDamp( 
			craneArmCurrPos, this.craneArmTargetPos, ref craneArmVelocity, craneSmoothTime, craneMaxSpeed, Time.fixedDeltaTime );

		
		this.craneArm.transform.position = craneArmCurrPos;
		if(Input.GetKey(KeyCode.Space))
		{
			GameObject.Destroy(this.hinge);
		}
	}

	/*
	void FixedUpV1()
	{
		if(Input.GetKey(KeyCode.Q))
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
		}
	}
	*/

	/*void FixedUpV2()
	{
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
		}
	}*/


	/*

	[SerializeField] Rigidbody craneNeck;
	[SerializeField] Rigidbody craneBase;
	[SerializeField] float forceAmountCraneBase;
	[SerializeField] Transform forcePosition;

	[SerializeField] Rigidbody kinemat;
	[SerializeField] HingeJoint hinge;

	void FixedUpdate()
	{
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
	}*/
}
