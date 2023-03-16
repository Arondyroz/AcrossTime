using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlayer : MonoBehaviour
{
    public new GameObject camera;
    public GameObject collide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camera.SetActive(collide == null);
    }

    private void OnTriggerStay(Collider other)
    {
        collide = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        collide = null;
    }
}
