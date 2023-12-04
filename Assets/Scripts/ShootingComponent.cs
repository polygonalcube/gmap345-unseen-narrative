using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    public Vector3 speed;
    public float angularSpeed;
    float shotDelay;
    public float shotDelaySet;
    public AudioSource shotSound;

    public enum Mode
    {
        NORMAL,
        ANGULAR,
        TARGET
    }

    public int bulletCount = 1;

    public bool isSpread = false;
    public float spreadAngleSize = 45f;

    public bool isRadial = false;

    public bool isAlternating = false;
    public bool onAlternativeCycle = false;
    
    void Start()
    {
        shotDelay = 0f;
    }

    void Update()
    {
        shotDelay -= Time.deltaTime;
    }

    public void Shoot(Vector3 startingPosition, Vector3 positionOffset, Vector3 shotDirection, Vector3 targetPosition, string newTag = "Enemy Damage", Mode shotStyle = Mode.ANGULAR)
    {
        if (shotDelay <= 0)
        {
            if (shotSound != null)
                shotSound.Play();
            if (isSpread)
            {
                if (isAlternating && onAlternativeCycle)
                {
                    for (int i = 0; i < bulletCount - 1; i++)
                    {
                        //GameObject newProjectile = spawner.Spawn(startingPosition);
                        GameObject newProjectile = GameManager.gm.RetrieveBullet(startingPosition, newTag);
                        float originalAngle = shotDirection.y;//Vector2.SignedAngle(Vector2.up, new Vector2(shotDirection.x, shotDirection.z));
                        //Debug.Log("originalAngle:", originalAngle);
                        float shotAngle = 0f;
                        //                gets to the left of the angle spread  gets the interval (explains "- 1")          specific lane
                        shotAngle = (originalAngle - (spreadAngleSize / 2)) + ((spreadAngleSize / (float)(bulletCount - 1)) * ((float)i + 0.50001f));
                        newProjectile.transform.eulerAngles = new Vector3(0f, shotAngle, 0f);
                        if (newProjectile.TryGetComponent<MoveComponent>(out MoveComponent mover))
                        {
                            mover.angularSpeed = angularSpeed;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < bulletCount; i++)
                    {
                        //GameObject newProjectile = spawner.Spawn(startingPosition);
                        GameObject newProjectile = GameManager.gm.RetrieveBullet(startingPosition, newTag);
                        float originalAngle = shotDirection.y;//Vector2.SignedAngle(Vector2.up, new Vector2(shotDirection.x, shotDirection.z));
                        //Debug.Log("originalAngle:", originalAngle);
                        float shotAngle = 0f;
                        //                gets to the left of the angle spread  gets the interval (explains "- 1")          specific lane
                        shotAngle = (originalAngle - (spreadAngleSize / 2)) + ((spreadAngleSize / (float)(bulletCount - 1)) * ((float)i + 0.00001f));
                        newProjectile.transform.eulerAngles = new Vector3(0f, shotAngle, 0f);
                        if (newProjectile.TryGetComponent<MoveComponent>(out MoveComponent mover))
                        {
                            mover.angularSpeed = angularSpeed;
                        }
                    }
                }
            }
            else
            {
                GameObject newProjectile = GameManager.gm.RetrieveBullet(startingPosition, newTag);
                float originalAngle = shotDirection.y;
                newProjectile.transform.eulerAngles = new Vector3(0f, originalAngle, 0f);
                if (newProjectile.TryGetComponent<MoveComponent>(out MoveComponent mover))
                {
                    mover.angularSpeed = angularSpeed;
                }
            }
            shotDelay = shotDelaySet;
            onAlternativeCycle = !onAlternativeCycle;
        }
    }
    /*
    public void Shoot(Vector3 startingPosition, Mode shotStyle = Mode.NORMAL, Vector3? positionOffset = null, Vector3? shotDirection = null, 
    Vector3? targetPosition = null)
    {
        if (shotDelay <= 0)
        {
            GameObject newProjectile = spawner.Spawn(startingPosition);
            
            if (newProjectile.TryGetComponent<MoveComponent>(out MoveComponent mover))
            {
                switch (shotStyle)
                {
                    case Mode.NORMAL:
                        mover.xSpeed = speed.x;
                        mover.ySpeed = speed.y;
                        mover.zSpeed = speed.z;
                        break;
                    case Mode.ANGULAR:
                        mover.angularSpeed = angularSpeed;
                        break;
                    case Mode.TARGET:
                        newProjectile.transform.LookAt((Vector3)targetPosition);
                        mover.angularSpeed = angularSpeed;
                        break;
                }
            }
            shotDelay = shotDelaySet;
        }
    }

func shoot(start_pos, x_plus = 0, y_plus = 0, z_plus = 0):
	if (delay <= 0):
		var new_bullet = spawn.spawn(start_pos)
		if new_bullet.mov != null:
			new_bullet.mov.x_speed = speed.x + x_plus
			new_bullet.mov.y_speed = speed.y + y_plus
			new_bullet.mov.z_speed = speed.z + z_plus
		else:
			print("mov is null!")
		delay = delay_set


#func shoot_ang(start_pos, shoot_dir):
#	if (delay <= 0):
#		var new_bullet = spawn.spawn(start_pos)
#		new_bullet.is_angular = true
#		if new_bullet.mov != null:
#			new_bullet.mov.x_speed = speed.x + x_plus
#			new_bullet.mov.y_speed = speed.y + y_plus
#			new_bullet.mov.z_speed = speed.z + z_plus
#		else:
#			print("mov is null!")
#		delay = delay_set


#func shoot_at_target(start_pos, target):
#	if (delay <= 0):
#		var new_bullet = bullet.instantiate()
#		get_tree().get_root().get_node("World").add_child(new_bullet)
#		new_bullet.global_position = start_pos
#		new_bullet.is_angular = true
#		new_bullet.look_at(target.global_position)
#		set_layers(new_bullet, layers)
#		if new_bullet.mov != null:
#			new_bullet.mov.ang_speed = speed_ang
#		else:
#			print("mov is null!")
#		delay = delay_set


func shoot_at_position(start_pos, target):
	if (delay <= 0):
		var new_bullet = spawn.spawn(start_pos)
		new_bullet.is_angular = true
		new_bullet.look_at(target)
		if new_bullet.mov != null:
			new_bullet.mov.ang_speed = speed_ang
		else:
			print("mov is null!")
		delay = delay_set

    */
}
