using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private Quaternion initRot;
    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initRot = transform.rotation;
        initPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = initRot;
        if(GameObject.Find("Player") != null)
        {
            transform.position = new Vector3(GetComponentInParent<Player>().transform.position.x, GetComponentInParent<Player>().transform.position.y+1, initPos.z);
        }
    }
}
