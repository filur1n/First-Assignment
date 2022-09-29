using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonMovement : MonoBehaviour
{
        //Movement for the Character so it moves smoothly and with the camera
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
        
        //this is for gravity
    private Vector3 velocity;
    public float gravity = -9.81f;
    
    //groundcheck

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float JumpHeight = 3f;
    public int damage = 1;
    


    private bool isGrounded;

    [SerializeField] private int playerIndex;
    [SerializeField] private PlayerTurn playerTurn;
    [SerializeField] private Transform shootingStartPosition;
    private GameObject projectile;
    
    
    //health
    public int maxHealth = 20;
    public int currentHealth;

    public HealthBar healthBar;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    
    

    public void TakeDamage(int damage)
    {
        Debug.Log("DamageTaken");
        currentHealth -= damage; 
        healthBar.SetHealth(currentHealth);
    }

    private void Awake()
    {
        projectile = TurnManager.GetInstance().GetProcetilePrefab();
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (currentHealth <= 0)
            {
                this.gameObject.SetActive(false);
                SceneManager.LoadScene(0);
            }

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (TurnManager.GetInstance().IsItPlayerTurn(playerIndex))
            {
                Movement();
                Fire();
            }
            
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            
            
            
        }

        void Fire()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                GameObject mewProjectile = Instantiate(projectile, shootingStartPosition.position, shootingStartPosition.rotation);
                //mewProjectile.transform.position = shootingStartPosition.position;
                mewProjectile.GetComponent<Projectile>().Initialize();
                TurnManager.GetInstance().ChangeTurn();
            }
        }
        void Movement()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
            }
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
    }
}
