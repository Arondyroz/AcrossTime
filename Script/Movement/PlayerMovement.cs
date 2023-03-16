using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    protected Player player;
    protected AudioSource sfx;
    protected float footStepTime = 0.5f;

    private void Awake()
    {
        player = GetComponent<Player>();
        sfx = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isPlaying) return;

        isGrounded = Physics.Linecast(groundCheck.position, groundCheck.position + Vector3.down * groundDistance, groundMask);

        if (isGrounded && velocity.y < -2)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (player.teleporting)
        {
            controller.SimpleMove(Vector3.zero);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);

            if (move != Vector3.zero)
            {
                if (footStepTime < 0 && isGrounded)
                {
                    sfx.pitch = Random.Range(0.5f, 2.0f);
                    sfx.Play();

                    footStepTime = Random.Range(0.5f, 0.8f);
                }
            }            
        }

        if (footStepTime > 0)
        {
            footStepTime -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            sfx.pitch = Random.Range(1.5f, 3.0f);
            sfx.Play();

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y = velocity.y + gravity * Time.deltaTime;

        if (player.teleporting)
        {
            controller.SimpleMove(Vector3.zero);
        }
        else
        {
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
