using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovment : MonoBehaviour
{
    // Player movement variables
    public float speed = 6f;
    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private int floorMask;
    private float camRayLength = 100f;

    // Magic Number String Varaibles
    [SerializeField] private string floor;
    [SerializeField] private string horizontalInput;
    [SerializeField] private string verticalInput;
    [SerializeField] private string walkingAnimatorBool;

    // stamina variables
    private float maxStamina = 10f;
    private float drainRate = 2f;
    private float regen = 1f;
    public float stamina = 10f;
    public Slider staminaSlider;
    public AudioSource sprintAudio;
   
    void Awake()
    {
        // Initialize Floor Mask, Animator/Animation, and the players Rigidbody
        floorMask = LayerMask.GetMask(floor);
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        
        
    }

     void FixedUpdate()
    {
        // Get the horizontal input axis
        float horizontalPlayerMovment = Input.GetAxisRaw(horizontalInput);
        // Get the vertical input axis
        float verticalPlayerMovment = Input.GetAxisRaw(verticalInput);
        // Move the player based on input
        Move(horizontalPlayerMovment, verticalPlayerMovment);
        //Calls the Turning function to rotate the player towards the mouse position
        Turning();
        // Calls the Animating function to set the walking animation based on input
        Animating(horizontalPlayerMovment, verticalPlayerMovment);
        // Calls the Sprint function to handle sprinting logic
        Sprint();
    }
    
    ///<summary>
    /// Sets movment vector based on input and moves the player
    /// Normalzes the movment vector to make sure the player moves at a constant speed and it is consistent 
    /// Moves the player using the Rigidbody's MovePosition method
    /// /// </summary>
    void Move ( float h, float v )
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    ///<Summary>
    /// Turns the player to face the mouse position
    /// Casts a ray from the camera to the mouse position on the ground
    /// Adjusts the player's rotation to look at the point where the ray hits the ground
    /// </summary>
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }

        
    }
    /// <summary>
    /// Starts or stops the walking animation based on player input
    /// if the player is moving (h or v != 0), sets the walking animation to true
    /// </summary>

    void Animating(float h, float v)
    {
        bool walking= h != 0f ||  v != 0f;
        anim.SetBool(walkingAnimatorBool, walking);
    }

    bool isSprinting = false;

    /// <summary>
    /// Sprints the player when the left shift key is pressed
    /// Checks if the player is not already sprinting and has enough stamina
    /// Plays sprint audio when sprinting starts
    /// Sets Drain and Regen rates for stamina
    /// Clamps stamina to a maximum value
    /// Stops sprinting when stamina runs out or the left shift key is released
    /// </summary>

    void Sprint()
    {
        
        if (!isSprinting && Input.GetKey(KeyCode.LeftShift) && stamina >= maxStamina)
        {
            isSprinting = true;
            
            sprintAudio.Play();
            
        }

        if (isSprinting)
        {
            speed = 10f;
            stamina -= drainRate * Time.deltaTime;
            stamina = Mathf.Max(stamina, 0f);
            
            staminaSlider.value = stamina;

            if (stamina <= 0f || !Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = false;
            }
        }
        else
        {
            speed = 6f;
            if (stamina < maxStamina)
            {
                stamina += regen * Time.deltaTime;
                stamina = Mathf.Min(stamina, maxStamina);
                staminaSlider.value = stamina;
            }
        }
        

    }
    

}
