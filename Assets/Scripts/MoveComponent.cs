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
    /*
var ang_speed:float
@export var max_speed:Vector3 = Vector3(1,99,1)
@export var max_speed_ang:float
@export var normalize_move:bool = false


func accelerate(speed_var, dir):
	speed_var += accel * dir
	return speed_var


func decelerate(speed_var):
	speed_var += decel * sign(-speed_var)
	if abs(speed_var) <= decel:
		speed_var = 0
	return speed_var


func check_near_zero(speed_var, limit):
	if abs(speed_var) <= limit:
		speed_var = 0
	return speed_var


func move(direction, grounded:bool = false):
	
	x_speed = cap(x_speed, max_speed.x, direction.x)
	if obey_gravity:
		y_speed = cap(y_speed, max_speed.y, -1)
	else:
		y_speed = cap(y_speed, max_speed.y, direction.y)
	z_speed = cap(z_speed, max_speed.z, direction.z)
	
	if normalize_move:
		var top_speed = x_speed if abs(x_speed) > abs(z_speed) else z_speed
		#print(Vector3(x_speed, 0, z_speed).normalized())
		#print(Vector3(x_speed, 0, z_speed).normalized() * top_speed)
		#print()
		return (Vector3(x_speed, 0, z_speed).normalized() * abs(top_speed)) + Vector3(0, y_speed, 0)
	return Vector3(x_speed, y_speed, z_speed)

# to be refined when base move() is good
"""
func move_toward_target(target:Vector3, grounded:bool = false):
	#var target_pos:Vector3 = target
	var new_vec:Vector3 = (target - global_position).normalized()
	
	if abs(new_vec.x) != 0:
		x_speed += accel * new_vec.x
	else:
		x_speed += decel * sign(-x_speed)
		if abs(x_speed) <= decel:
			x_speed = 0;
	
	if obey_gravity:
		pass#y_speed = direction.y
	else:
		if abs(new_vec.y) != 0:
			y_speed += accel * new_vec.y
		else:
			y_speed += decel * sign(-y_speed)
			if abs(y_speed) <= decel:
				y_speed = 0;
	
	if abs(new_vec.z) != 0:
		z_speed += accel * new_vec.z
	else:
		z_speed += decel * sign(-z_speed)
		if abs(z_speed) <= decel:
			z_speed = 0;
	
	if abs(x_speed) > max_speed.x * abs(new_vec.x):
		x_speed = max_speed.x * new_vec.x
	if abs(y_speed) > max_speed.y:
		y_speed = max_speed.y * sign(y_speed)
	if abs(z_speed) > max_speed.z * abs(new_vec.z):
		z_speed = max_speed.z * new_vec.z
	
	if obey_gravity:
		y_speed += gravity
	if grounded:
		y_speed = 0
	
	return Vector3(x_speed, y_speed, z_speed)


func nav_toward_target(target:Vector3, grounded:bool = false):
	if nav != null:
		nav.target = target
		var new_vec:Vector3 = (nav.get_next_path_position() - global_position).normalized()
		
		if abs(new_vec.x) != 0:
			x_speed += accel * new_vec.x
		else:
			x_speed += decel * sign(-x_speed)
			if abs(x_speed) <= decel:
				x_speed = 0;
		
		if obey_gravity:
			pass#y_speed = direction.y
		else:
			if abs(new_vec.y) != 0:
				y_speed += accel * new_vec.y
			else:
				y_speed += decel * sign(-y_speed)
				if abs(y_speed) <= decel:
					y_speed = 0;
		
		if abs(new_vec.z) != 0:
			z_speed += accel * new_vec.z
		else:
			z_speed += decel * sign(-z_speed)
			if abs(z_speed) <= decel:
				z_speed = 0;
		
		if abs(x_speed) > max_speed.x * abs(new_vec.x):
			x_speed = max_speed.x * new_vec.x
		if abs(y_speed) > max_speed.y:
			y_speed = max_speed.y * sign(y_speed)
		if abs(z_speed) > max_speed.z * abs(new_vec.z):
			z_speed = max_speed.z * new_vec.z
		
		if obey_gravity:
			y_speed += gravity
		if grounded:
			y_speed = 0
		
		return Vector3(x_speed, y_speed, z_speed)


func move_ang():
	pass#return transform.basis.z *
"""

    */
}
