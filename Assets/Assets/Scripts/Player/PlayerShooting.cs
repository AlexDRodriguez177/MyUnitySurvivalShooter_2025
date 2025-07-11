using System.Net;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerShooting : MonoBehaviour
{
// Gun Shooting System
public int dPS = 20;
public float timeBetweenBullets = .15f;
public float range = 100f;

float timer;
Ray shootRay;
RaycastHit shootHit;
int shootableMask;
ParticleSystem gunParticles;
LineRenderer gunLine;
AudioSource gunAudio;
Light gunLight;
float effectDispalyTime = .2f;
public bool isShooting;



    //Ammo system 

    public int maxAmmo=10;
    public int currentAmmo;
    public float reloadTime = 1.5f;
    public int reservedAmmo=30;
    public bool isReloading = false;
    public int maxReserveAmmo = 50;

    public AudioSource reloadSounds;
    public AudioSource noAmmoSound;
private void Awake()
{
    // Inital references
    shootableMask = LayerMask.GetMask("Shootable");
    gunParticles = GetComponent<ParticleSystem>();
    gunLine = GetComponent<LineRenderer>();
    gunAudio = GetComponent<AudioSource>();
    
    gunLight = GetComponent<Light>();
    
    isShooting = false;
    currentAmmo = maxAmmo;
    }
void Update()
{
        // time since last shot
        timer += Time.deltaTime;
        
        // If reloading, block all other actions
        if (isReloading)
            return;

        //Manual Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentAmmo == maxAmmo)
            {
                // Already full, do nothing
                return;
            }

            if (reservedAmmo <= 0 || currentAmmo<=0)
            {
                // No ammo left to reload with
                DisabledEffects();
                noAmmoSound.Play();
                return;
            }

            // Normal reload
            DisabledEffects();
            reloadSounds.Play();
            StartCoroutine(Reload());
            return;
        }

        if (currentAmmo <= 0)
        {
            

            if (reservedAmmo > 0)
            {
                //Last clip still reloads
                DisabledEffects();
                reloadSounds.Play();
                StartCoroutine(Reload());
            }
            else
            { 
                //no more ammo disables effects and plays no ammo when player tries to fire 
                DisabledEffects();
                if (!isReloading && Input.GetButtonDown("Fire1") )
                { 
                    
                    noAmmoSound.Play();
                }
            }
            return;
        }
        
        // Shooting aka firing gun and disables effects between each shot
        if (!isShooting && Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                isShooting = true;
            }
            

            
        }
        
        //Auto disable effects
        if (timer >= timeBetweenBullets * effectDispalyTime)
        {
        DisabledEffects();
        isShooting = false;
        }
}

//Shooting Logic
void Shoot()
{
   
    timer = 0f;
    //Play shooting effects
    gunAudio.Play();
    gunLight.enabled = true;
    gunParticles.Stop();
    gunParticles.Play();
    gunLine.enabled = true;
    gunLine.SetPosition(0, transform.position);

    shootRay.origin = transform.position;
    shootRay.direction = transform.forward;

        //Consume Ammo
        currentAmmo--;

        // Raycast to see if we hit something
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
    {
        EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
                // Deal damage to enemy

                enemyHealth.TakeDamage(dPS, shootHit.point);
        }
        gunLine.SetPosition(1, shootHit.point);
    }
    else
    {
            // Missed shot extend line to max range

            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
    }
}
    //Reload Logic
    IEnumerator Reload()
    {
        isReloading = true;
        if (reservedAmmo <= 0 || currentAmmo == maxAmmo)
        {
            // No ammo to reload with OR already full
            yield break;

        }
        
        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = maxAmmo - currentAmmo;

        if (reservedAmmo >= neededAmmo)
        {
            reservedAmmo -= neededAmmo;
            currentAmmo = maxAmmo;
            isReloading = false;
            
        }
        else
        {
            //Not enough to fully reload
            currentAmmo += reservedAmmo;
            reservedAmmo = 0;
            isReloading = false;
        }
    }
    // ammo pickup never reaches over 50
    public void AddAmmo(int amount)
    {
        //add the ammo picked up
        reservedAmmo += amount;
        
        //clamp so ammo never reaches above 50 or bellow 0
        reservedAmmo = Mathf.Clamp(reservedAmmo, 0, maxReserveAmmo);
    }

    public void DisabledEffects()
{
     // disables effects and particles
    gunLine.enabled = false;
    gunLight.enabled = false;
}

}