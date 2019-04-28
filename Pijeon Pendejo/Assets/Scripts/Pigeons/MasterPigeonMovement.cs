using UnityEngine;

public class MasterPigeonMovement : MonoBehaviour
{
	[Header("Starting Values")]
	public float startingVelocity = 12f;
	public float baseGravityForce = 30f;
	public float baseGravityTorque = 20f;

	[Header("Movement Parameters")]
	public float minimumGTForSwing = 50f;
	public float gravityForceGrowthPerSecond = 10f;
	public float gravityTorqueGrowthPerSecond = 310f;
	private float thrustForce = 120f;
	private float thrustTorque = 15f;

	[Header("Limits")]
	public float maxAngularVelocity = 50f;
	public float maxVelocity = 14f;

	private Rigidbody2D pigeonRb;
	private float currentGravityForce;
	private float currentGravityTorque;

    private Animator m_Animator;
	private GravityTorqueMode previousTorqueMode;
	private SpeedUpgrade speedStats;


    private void Start()
    {
		InitializeMasterPigeon();
    }

	private void InitializeMasterPigeon()
	{
		pigeonRb = GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponentInChildren<Animator>();

		SetSpeedStats();

        if (pigeonRb.velocity == Vector2.zero)
		{
			pigeonRb.velocity = new Vector3(startingVelocity, 0f, 0f);
			PigeonUnit.SetAsMasterPigeon(gameObject);
		}
	}

	private void SetSpeedStats()
	{
		pigeonRb.mass = speedStats.masterStats.mass;
		pigeonRb.drag = speedStats.masterStats.linearDrag;
		pigeonRb.angularDrag = speedStats.masterStats.angularDrag;

		maxVelocity = speedStats.masterStats.maxSpeed;
		thrustForce = speedStats.masterStats.thrustForce;
		thrustTorque = speedStats.masterStats.thrustTorque;
	}

    private void Update()
	{
		ProcessMovementInput();
		ClampMasterPigeonVelocity();
    }

	private void ProcessMovementInput()
	{
		if (Input.GetButton("Pigeon Thrust"))
		{
			UpdateGravityForce(GravityForceUpdateMode.Reset);
			ApplyPigeonThrust();
            m_Animator.speed = 2;
		}
		else
		{
			UpdateGravityForce(GravityForceUpdateMode.GrowForce);
			ApplyGravityForce();
            m_Animator.speed = 1;
        }
	}

    private void ClampMasterPigeonVelocity()
	{
		pigeonRb.velocity = Vector3.ClampMagnitude(pigeonRb.velocity, maxVelocity);
		pigeonRb.angularVelocity = Mathf.Clamp(pigeonRb.angularVelocity, -maxAngularVelocity, maxAngularVelocity);
	}

	private void ApplyPigeonThrust()
	{
		pigeonRb.AddForce(transform.right * thrustForce);
		pigeonRb.AddTorque(thrustTorque);
	}

	private void ApplyGravityForce()
	{
		pigeonRb.AddForce(Vector3.down * currentGravityForce);
		ApplyGravityTorque();	
	}

	private void ApplyGravityTorque()
	{
		float zAxis = transform.rotation.eulerAngles.z;

		if (zAxis > 90 && zAxis < 270)
		{
			if (previousTorqueMode == GravityTorqueMode.Forward)
			{
				previousTorqueMode = GravityTorqueMode.Backward;

				if (zAxis > 180)
				{
					if (currentGravityTorque > minimumGTForSwing)
					{
						UpdateGravityForce(GravityForceUpdateMode.Swing);
					}
					else
					{
						currentGravityTorque = 0;
					}
				}
			}

			pigeonRb.AddTorque(currentGravityTorque);
		}
		else
		{
			if (previousTorqueMode == GravityTorqueMode.Backward)
			{
				previousTorqueMode = GravityTorqueMode.Forward;

				if (zAxis > 270)
				{
					if (currentGravityTorque > minimumGTForSwing)
					{
						UpdateGravityForce(GravityForceUpdateMode.Swing);
					}
					else
					{
						currentGravityTorque = 0;
					}
				}

			}

			pigeonRb.AddTorque(-currentGravityTorque);
		}
	}

	private void UpdateGravityForce(GravityForceUpdateMode mode)
	{
		switch (mode)
		{
			case GravityForceUpdateMode.Reset:
				currentGravityForce = baseGravityForce;
				currentGravityTorque = baseGravityTorque;
				break;

			case GravityForceUpdateMode.GrowForce:
				currentGravityForce += gravityForceGrowthPerSecond * Time.fixedDeltaTime;
				currentGravityTorque += gravityTorqueGrowthPerSecond * Time.fixedDeltaTime;
				break;

			case GravityForceUpdateMode.Swing:
				currentGravityTorque = -currentGravityTorque;
				break;
		}		
	}

	private enum GravityForceUpdateMode
	{
		GrowForce,
		Reset,
		Swing,	
	}

	private enum GravityTorqueMode
	{
		Forward,
		Backward,
	}
}
