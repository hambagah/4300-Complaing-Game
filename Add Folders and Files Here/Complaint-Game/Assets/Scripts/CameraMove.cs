using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 difference;
    private Vector3 resetCamera;
    public bool drag = false;

    private void Start()
    {
        resetCamera = Camera.main.transform.position;
    }
    
    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (drag == false)
            {
                drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else 
        {
            drag = false;
        }

        if(drag)
        {
            Camera.main.transform.position = origin - difference;
            Camera.main.transform.position = new Vector3(
            Mathf.Clamp(Camera.main.transform.position.x, -41, 41),
            Mathf.Clamp(Camera.main.transform.position.y, -3, 3), transform.position.z);
            //Realistically constraints should be X = 30 and Y = 22
        }

        if(Input.GetMouseButton(2))
            Camera.main.transform.position = resetCamera;
    }
}
