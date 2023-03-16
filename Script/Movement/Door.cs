using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    public GameObject interactText;
    public GameObject canvas;
    private void Start()
    {
        anim = GetComponent<Animator>();
    
    }


    void Update()
    {
      if(IsTextActive)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                anim.SetBool("Opened", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvas.SetActive(false);
        }
    }

    private bool IsTextActive
    {
        get
        {
            return interactText.activeInHierarchy;
        }
    }
}
