using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public CharacterController charCon;
	
	public float accel;
    public float decel;
	public float maxSpeed;
	public bool isNormalized;

	public float xSpeed;
    public float ySpeed;
    public float zSpeed;
	public float angularSpeed;

    public float Accelerate(float speedVar, float axis)
    {
        speedVar += accel * axis * Time.deltaTime;
        return speedVar;
    }

    public float Decelerate(float speedVar)
    {
        speedVar += decel * GameManager.gm.Sign(-speedVar) * Time.deltaTime;
	    if (Mathf.Abs(speedVar) <= decel)
		{
			speedVar = 0f;
		}
		return speedVar;
    }

    public float CheckNearZero(float speedVar)
    {
        if(Mathf.Abs(speedVar) < 0.01f)
        {
            return 0f;
        }
        else
        {
            return speedVar;
        }
    }

    public float Cap(float speedVar)
    {
        if(speedVar > maxSpeed)
        {
            return maxSpeed;
        }
        else if(speedVar < -maxSpeed)
        {
            return -maxSpeed;
        }
        else
        {
            return speedVar;
        }
    }

    public void BoundXY(float xBound, float yBound)
    {
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -yBound)
        {
            transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
        }
        else if (transform.position.y > yBound)
        {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        }
    }

    public void ResetY()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    public void Move(Vector3 moveDir)
    {
		//acceleration and deceleration
        if (Mathf.Abs(moveDir.x) != 0f)
        {
            xSpeed = Accelerate(xSpeed, moveDir.x);
        }
		else
		{
			xSpeed = Decelerate(xSpeed);
		}
		if (Mathf.Abs(moveDir.y) != 0f)
        {
            ySpeed = Accelerate(ySpeed, moveDir.y);
        }
		else
		{
			ySpeed = Decelerate(ySpeed);
		}
		if (Mathf.Abs(moveDir.z) != 0f)
        {
            zSpeed = Accelerate(zSpeed, moveDir.z);
        }
		else
		{
			zSpeed = Decelerate(zSpeed);
		}

		//prevents the object from going too fast
		xSpeed = Cap(xSpeed);
		ySpeed = Cap(ySpeed);
		zSpeed = Cap(zSpeed);

		//final movement; if isNormalized is true, diagonal movement will not be faster
		if (isNormalized)
		{
			float highestSpeed = xSpeed;
			if (Mathf.Abs(ySpeed) > Mathf.Abs(highestSpeed))
			{
				highestSpeed = ySpeed;
			}
			if (Mathf.Abs(zSpeed) > Mathf.Abs(highestSpeed))
			{
				highestSpeed = zSpeed;
			}
			//transform.position += new Vector3(xSpeed, ySpeed, zSpeed).normalized * Mathf.Abs(highestSpeed) * Time.deltaTime;
			charCon.Move(new Vector3(xSpeed, ySpeed, zSpeed).normalized * Mathf.Abs(highestSpeed) * Time.deltaTime);
		}
		else
		{
			//transform.position += new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime;
			charCon.Move(new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime);
		}
    }

    public void MoveAngularly(Vector3 moveDir)
    {
		/*
        if (Mathf.Abs(moveDir.magnitude) != 0f)
        {
            angularSpeed = Accelerate(angularSpeed, moveDir.magnitude);
        }
		else
		{
			angularSpeed = Decelerate(angularSpeed);
		}
		*/
		//prevents the object from going too fast
		angularSpeed = Cap(angularSpeed);

		//transform.position += moveDir/*TransformDirection(moveDir)*/ * angularSpeed * Time.deltaTime;
		charCon.Move(moveDir * angularSpeed * Time.deltaTime);
    }
}
