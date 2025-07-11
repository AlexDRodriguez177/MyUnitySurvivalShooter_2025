using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovment : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    public float stamina = 10f;
    private float maxStamina = 10f;
    private float drainRate = 2f;
    private float regen = 1f;
    public Slider staminaSlider;
    
    public AudioSource sprintAudio;
   
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        
        
    }

     void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
        Sprint();
    }

    void Move ( float h, float v )
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }
    
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
    void Animating(float h, float v)
    {
        bool walking= h != 0f ||  v != 0f;
        anim.SetBool("IsWalking", walking);
    }

    bool isSprinting = false;

 

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
