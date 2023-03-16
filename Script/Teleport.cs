using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject objFuture;
    public GameObject objPast;

    protected Collider colliderFuture;
    protected Collider colliderPast;

    private void Awake()
    {
        colliderFuture = objFuture.GetComponent<Collider>();
        colliderPast = objPast.GetComponent<Collider>();
    }

    private void Update()
    {
        if (LevelManager.instance.isFuture == true)
        {
            objPast.transform.localPosition = objFuture.transform.localPosition; // new Vector3(objFuture.transform.localPosition.x, objPast.transform.localPosition.y, objFuture.transform.localPosition.z);
            objPast.transform.rotation = objFuture.transform.rotation;// new Vector3(objFuture.transform.eulerAngles.x, objFuture.transform.eulerAngles.y, objFuture.transform.eulerAngles.z);
        }
        else
        {
            objFuture.transform.localPosition = objPast.transform.localPosition;// new Vector3(objPast.transform.localPosition.x, objFuture.transform.localPosition.y, objPast.transform.localPosition.z);
            objFuture.transform.rotation = objPast.transform.rotation;// new Vector3(objPast.transform.eulerAngles.x, objPast.transform.eulerAngles.y, objPast.transform.eulerAngles.z);
        }

        colliderFuture.isTrigger = !LevelManager.instance.isFuture;
        colliderPast.isTrigger = !colliderFuture.isTrigger;
    } 
}
