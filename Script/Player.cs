using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ShadowPlayer shadowPlayer;

    public ParticleSystem teleportVFX;
    public AudioSource teleportSFX;

    public Transform timeEye;

    public bool canTeleport = true;
    public bool showTime = false;
    public bool teleporting { get; protected set; }

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.A) && teleporting == false)
        //{
        //    transform.Translate(Vector2.left * 5 * 0.1f);
        //    transform.localScale = new Vector3(-1, 1, 1);
        //}
        //if (Input.GetKey(KeyCode.W) && teleporting == false)
        //{
        //    transform.Translate(Vector2.up * 5 * 0.1f);
        //}
        //if (Input.GetKey(KeyCode.D) && teleporting == false)
        //{
        //    transform.Translate(Vector2.right * 5 * 0.1f);
        //    transform.localScale = new Vector3(1, 1, 1);
        //}
        //if (Input.GetKey(KeyCode.S) && teleporting == false)
        //{
        //    transform.Translate(Vector2.down * 5 * 0.1f);
        //}

        canTeleport = shadowPlayer.collide == null;

        if (Input.GetKeyUp(KeyCode.E) && teleporting == false)
        {
            StartCoroutine(Disableteleport());
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            showTime = !showTime;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            GameManager.instance.Restart();
        }

        if (showTime)
        {
            timeEye.localRotation = Quaternion.RotateTowards(timeEye.localRotation, Quaternion.identity, Time.deltaTime * 200);
        }
        else
        {
            timeEye.localRotation = Quaternion.RotateTowards(timeEye.localRotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 200);
        }

        shadowPlayer.transform.rotation = transform.rotation;// new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        if (Vector3.Distance(shadowPlayer.transform.localPosition, transform.localPosition) > 0.1f)
            shadowPlayer.transform.localPosition = transform.localPosition;

        //if (LevelManager.instance.isFuture)
        //{
        //    camPast.transform.localPosition = transform.localPosition;// new Vector3(transform.position.x, -1000, transform.position.z);
        //}
        //else
        //{
        //    camPast.transform.localPosition = transform.localPosition;// new Vector3(transform.position.x, 0, transform.position.z);
        //}
    }

    private void OnTriggerEnter(Collider objects)
    {
        //if (objects.gameObject.CompareTag("Wall"))
        //{
        //    Debug.Log("Stuck");
        //}
    }

    public void Teleport()
    {
        if (!canTeleport) return;

        //Vector3 tempPos = transform.localPosition;
        transform.parent = null;
        shadowPlayer.transform.parent = null;// transform.parent; 

        LevelManager.instance.Teleport();

        if (LevelManager.instance.isFuture)
        {
            transform.parent = LevelManager.instance.pastLevel;
            shadowPlayer.transform.parent = LevelManager.instance.futureLevel;
            //transform.localPosition = tempPos;// new Vector3(transform.position.x, -998, transform.position.z);

            LevelManager.instance.isFuture = false;
        }
        else
        {
            transform.parent = LevelManager.instance.futureLevel;
            shadowPlayer.transform.parent = LevelManager.instance.pastLevel;
            //transform.localPosition = tempPos; //new Vector3(transform.position.x, 2, transform.position.z);

            LevelManager.instance.isFuture = true;
        }

        if (teleportVFX)
            teleportVFX.Play(true);
        if (teleportSFX)
        {
            teleportSFX.pitch = Random.Range(1.0f, 3.0f);
            teleportSFX.Play();
        }
    }

    IEnumerator Disableteleport()
    {
        teleporting = true;

        yield return new WaitForSeconds(0.2f);

        Teleport();

        yield return new WaitForSeconds(0.5f);

        teleporting = false;
    }
}
