using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    protected AudioSource sfx;

    private void Awake()
    {
        sfx = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && GameManager.instance.isPlaying)
        {
            sfx.pitch = Random.Range(1.0f, 3.0f);
            sfx.Play();

            GameManager.instance.isPlaying = false;

            Invoke("NextLevel", 1.0f);
        }
    }

    public void NextLevel()
    {
        LevelManager.instance.NextLevel();
    }
}
