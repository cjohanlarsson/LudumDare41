using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour 
{
	public static Crane current;

	public enum MouseControl
	{
		None,
		Absolute,
		Relative,
		Deltas
	}

	public enum CraneControl
	{
		TranslateArm,
		RotateArm
	}

	[Header("Crane Basics")]
	[SerializeField] CraneControl craneControl;
	[SerializeField] Rigidbody craneArm;
	[SerializeField] Rigidbody buildingAttachment;

	[Header("Crane Translate Arm")]
	[SerializeField] float craneMaxSpeed;
	[SerializeField] float craneSmoothTime;

	[Header("Crane Rotate Arm")]
	[SerializeField] float craneAngularSpeed = 120f;
	[SerializeField] float craneMaxSpeedY;
	[SerializeField] float craneExtendSpeed;
	[SerializeField] float craneMinAngle;
	[SerializeField] float craneMaxAngle;
	[SerializeField] float craneMinZ = 0f;
	[SerializeField] float craneMaxZ = 30f;

	[Header("Crane Height")]
	[SerializeField] float craneYSpeed = 6;
	[SerializeField] float craneYAccel = 6;
	[SerializeField] float craneYDeccel = 6;
	[SerializeField] float craneMinY = -4;
	[SerializeField] float craneMaxY = 4;


	[Header("Mouse Controls")]
	[SerializeField] MouseControl mouseControlMode;
	[SerializeField] float relativeMouseSpeed = 1f;

	[Header("Debug Stuff")]
	[SerializeField] Transform debugTargetCube;

	Vector3 craneArmTargetPos;
	Vector3 craneArmVelocity;
	Vector3 prevMousePos;
	float craneArmYVel;

	private Building attachedBuilding;
	public Building AttachedBuilding { get { return this.attachedBuilding; } }
	float direction = 0;
	float craneAngle = 0f;

	void Awake()
	{
		current = this;
	}

	void Start()
	{
		prevMousePos = Input.mousePosition;
		this.craneArmTargetPos = this.craneArm.transform.position;
	}

	bool GetPosition( Vector3 screenPos, ref Vector3 pos )
	{
		var ray = Camera.main.ScreenPointToRay( screenPos );
		float enter;
		if( (new Plane(Vector3.up, 0)).Raycast( ray, out enter) )
		{
			pos = ray.GetPoint(enter);
			return true;
		}
		return false;
	}

	void Update()
	{
		var view = Camera.main.ScreenToViewportPoint(Input.mousePosition);
 		var mouseInside = !(view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1);

		if(mouseControlMode == MouseControl.Absolute)
		{
			Vector3 targetPos = Vector3.zero;
			if(GetPosition( Input.mousePosition, ref targetPos))
			{
				this.craneArmTargetPos.x = targetPos.x;
				this.craneArmTargetPos.z = targetPos.z;
			}
		}
		else if(mouseControlMode == MouseControl.Deltas)
		{
			var delta = new Vector3( Input.GetAxis("Mouse X") , 0, Input.GetAxis("Mouse Y"));
			delta.y = 0f;
			this.craneArmTargetPos += delta * relativeMouseSpeed;
		}
		else if(mouseControlMode == MouseControl.Relative)
		{
			Vector3 currPos = Vector3.zero; 
			Vector3 prevPos = Vector3.zero;
			if(GetPosition(Input.mousePosition, ref currPos) && GetPosition(prevMousePos, ref prevPos))
			{
				currPos.y = prevPos.y = 0;
				this.craneArmTargetPos += (currPos - prevPos);
			}
			prevMousePos = Input.mousePosition;

			//this.craneArmTargetPos += move * relativeMouseSpeed;
		}


		if(Input.GetKey(KeyCode.Q) || (mouseInside && Input.GetMouseButton(0)))
		{
			direction += Time.deltaTime * craneYAccel;
		}
		else if(Input.GetKey(KeyCode.A) || (mouseInside &&  Input.GetMouseButton(1)))
		{
			direction -= Time.deltaTime * craneYAccel;
		}
		else
		{
			direction = Mathf.MoveTowards( direction, 0, Time.deltaTime * craneYDeccel );
		}
		direction = Mathf.Clamp(direction,-1,1);

		craneArmTargetPos.y = Mathf.Clamp( craneArmTargetPos.y + (direction * Time.deltaTime * craneYSpeed) ,craneMinY, craneMaxY);

		if(Level.current != null)
		{
			var min = Level.current.GetMinBounds();
			var max = Level.current.GetMaxBounds();
			craneArmTargetPos.x = Mathf.Clamp( craneArmTargetPos.x, min.x, max.x );
			craneArmTargetPos.z = Mathf.Clamp( craneArmTargetPos.z, min.z, max.z );
		}
		debugTargetCube.transform.position = craneArmTargetPos;
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		Transform craneParent = this.craneArm.transform.parent;
		if(craneControl == CraneControl.TranslateArm)
		{
			var craneArmCurrPos = this.craneArm.transform.position;
			craneArmCurrPos = Vector3.SmoothDamp( 
				craneArmCurrPos, this.craneArmTargetPos, ref craneArmVelocity, craneSmoothTime, craneMaxSpeed, Time.fixedDeltaTime );

			this.craneArm.transform.position = craneArmCurrPos;
		}
		else
		{
			var delta = new Vector3( Input.GetAxis("Mouse X") , 0, Input.GetAxis("Mouse Y"));
			delta.y = 0f;
			this.craneArmTargetPos += delta * relativeMouseSpeed;

			/*var craneForward = this.craneArm.transform.forward;
			var desiredForward = (craneArmTargetPos - this.craneArm.transform.position);
			desiredForward.y = 0f;

			craneForward = Vector3.RotateTowards( 
				craneForward, 
				desiredForward, 
				craneAngularSpeed * Mathf.Deg2Rad * Time.fixedDeltaTime ,
				10f );
			this.craneArm.transform.forward = craneForward;*/

			float deltaAng = Input.GetAxis("Mouse X") * craneAngularSpeed * Time.fixedDeltaTime;
			craneAngle = Mathf.Clamp(craneAngle + deltaAng, craneMinAngle, craneMaxAngle);

			this.craneArm.transform.parent.localEulerAngles = new Vector3(0,craneAngle,0);
			//this.craneArm.transform.parent.Rotate( new Vector3(0f, deltaAng, 0f ) );

			float deltaZ = (Input.GetAxis("Mouse Y") * craneExtendSpeed * Time.fixedDeltaTime);
			var pos = this.craneArm.transform.localPosition;
			pos.z = Mathf.Clamp(deltaZ + pos.z, craneMinZ, craneMaxZ);
			this.craneArm.transform.localPosition = pos;

			Vector3 craneArmCurrPos = craneParent.position;
			craneArmCurrPos.y = Mathf.SmoothDamp( craneArmCurrPos.y, this.craneArmTargetPos.y, ref craneArmYVel, craneSmoothTime, craneMaxSpeed, Time.fixedDeltaTime );
			craneParent.position = craneArmCurrPos;
		}

		if(Input.GetKey(KeyCode.Space) && attachedBuilding != null)
		{
			GameObject.Destroy(attachedBuilding.joint);
			attachedBuilding = null;
		}

	}

	public void AttachBuilding(Building building)
	{
		attachedBuilding = building;
		building.transform.position = buildingAttachment.transform.position;
		building.joint.connectedBody = buildingAttachment;
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
