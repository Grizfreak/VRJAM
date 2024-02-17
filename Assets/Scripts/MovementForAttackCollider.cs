using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementForAttackCollider : MonoBehaviour
{
    //get transform of the camera
    public Transform camera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //get the same position as the camera but with an offset of -0.3 in Y
        transform.position = new Vector3(camera.position.x, camera.position.y - 0.25f, camera.position.z);
        //get the same rotation as the camera but only in Y
        transform.rotation = Quaternion.Euler(0, camera.rotation.eulerAngles.y, 0);
    }


}
