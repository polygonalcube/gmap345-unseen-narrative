using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningComponent : MonoBehaviour
{
    public GameObject spawnedObject;
    
    public GameObject Spawn(Vector3 spawnPosition, GameObject parent = null, bool localPositioning = false)
    {
        GameObject newObject = Instantiate(spawnedObject, spawnPosition, Quaternion.identity);
        if (parent != null)
        {
            newObject.transform.SetParent(parent.transform);
        }
        return newObject;
    }
    /*
@export var layers:String
@export var masks:String


func set_layers(object, layer_string:String):
	var bit_array = layer_string.split("")
	var i:int = 1
	for bit in bit_array:
		object.set_collision_layer_value(i, bool(bit.to_int()))
		i += 1


func set_masks(object, mask_string:String):
	var bit_array = mask_string.split("")
	var i:int = 1
	for bit in bit_array:
		object.set_collision_mask_value(i, bool(bit.to_int()))
		i += 1


func spawn(pos:Vector3, parent = null, is_local:bool = false):
	if is_local:
		new_object.position = pos
	else:
		new_object.global_position = pos
	new_object.visible = true
	return new_object

    */
}
