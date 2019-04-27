﻿using UnityEngine;

public class MasterPigeonMovement : MonoBehaviour
{
	[Header("Starting Values")]
	public float startingVelocity = 12f;
	public float baseGravityForce = 70f;
	public float baseGravityTorque = 1f;

	[Header("Movement Parameters")]
	public float gravityForceGrowthPerSecond = 30f;
	public float gravityTorqueGrowthPerSecond = 3f;
	public float thrustForce = 160f;
	public float thrustTorque = 20f;

	[Header("Limits")]
	public float maxAngularVelocity = 45f;
	public float maxVelocity = 14f;

	private Rigidbody2D pigeonRb;
	private float currentGravityForce;
	private float currentGravityTorque;

    private Animator m_Animator;


    private void Start()
    {
		InitializeMasterPigeon();
    }

	private void InitializeMasterPigeon()
	{
		pigeonRb = GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponentInChildren<Animator>();
		

        if (pigeonRb.velocity == Vector2.zero)
		{
			pigeonRb.velocity = new Vector3(startingVelocity, 0f, 0f);
			PigeonUnit.SetAsMasterPigeon(gameObject);
		}
	}

    private void FixedUpdate()
	{
		ProcessMovementInput();
		ClampMasterPigeonVelocity();
    }

	private void ProcessMovementInput()
	{
		if (Input.GetButton("Pigeon Thrust"))
		{
			UpdateGravityForce(GravityForceUpdateMode.GrowForce);
			ApplyPigeonThrust();
            m_Animator.speed = 2;
		}
		else
		{
			UpdateGravityForce(GravityForceUpdateMode.Reset);
			ApplyGravityForce();
            m_Animator.speed = 1;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Pigeon collided with enemy");
            Destroy(gameObject);
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
		Debug.Log(transform.forward);
		pigeonRb.AddForce(Vector3.down * currentGravityForce);

		Quaternion targetQ = Quaternion.AngleAxis(Mathf.Atan2(-1, 0) * Mathf.Rad2Deg, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetQ, currentGravityTorque * Time.fixedDeltaTime);
	}

	

	private void UpdateGravityForce(GravityForceUpdateMode mode)
	{
		if (mode == GravityForceUpdateMode.Reset)
		{
			currentGravityForce = baseGravityForce;
			currentGravityTorque = baseGravityTorque;
		}
		else
		{
			currentGravityForce += gravityForceGrowthPerSecond * Time.fixedDeltaTime;
			currentGravityTorque += gravityTorqueGrowthPerSecond * Time.fixedDeltaTime;
		}
	}

	private enum GravityForceUpdateMode
	{
		GrowForce,
		Reset,
	}
}