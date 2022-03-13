using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fin : MonoBehaviour
{

    public GameObject loadScene;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            loadScene.GetComponent<LoadScene>().Win();
        }
    }
}
