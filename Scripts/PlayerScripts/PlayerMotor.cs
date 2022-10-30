using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour, IDamageable
{
    public static CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 4f;
    public float health = 100;

    private bool isGrounded;
    public float gravity = -9.8f;

    public float jumpHeight = 0.7f;

    private bool sprinting;

    private bool walking;

    public Animator walkAnim;

    public GameObject damageScreen;

    public GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (walking)
            speed = 18f;

        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector3 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.y = input.y;
        moveDirection.z = input.z;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Sprint()
    {
        if (controller.height > 1.5)
        {
            sprinting = !sprinting;
        }

        if (sprinting)
        {
            if (controller.height > 1)
            {
                walking = false;
                speed = speed+5;
                walkAnim.SetBool("IsMoving", true);
            }
        }
        else if (!sprinting)
        {
            walkAnim.SetBool("IsMoving", false);
            walking = true;
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        GameObject damageClone = Instantiate(damageScreen, damageScreen.transform.position, damageScreen.transform.rotation) as GameObject;
        GameObject parentObject = GameObject.Find("UI");
        damageClone.transform.parent = parentObject.transform;
        damageClone.SetActive(true);
        Destroy(damageClone, 0.2f);

        if (health <= 0)
        {
            deathScreen.SetActive(true);
        }
    }
}
