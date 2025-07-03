using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //public Transform John;
    public Transform Inti;

    void Update()
    {
        /*if (John != null)
        {
            Vector3 position = transform.position;
            position.x = John.position.x;
            transform.position = position;
        */

        if (Inti != null)
        {
            Vector3 position = transform.position;
            position.x = Inti.position.x;
            transform.position = position;
        }
    }
}
