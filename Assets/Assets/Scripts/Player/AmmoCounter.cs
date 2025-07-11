using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public class AmmoCounter : MonoBehaviour
{
    //references 
    public PlayerShooting playerShooting;
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI reservedAmmo;
    public static AmmoCounter Instance;


    private void Awake()
    {
        // Set this to be the one Instance that other scripts can use
        Instance = this;
        
    }
    // Update every frame and display ammo
    void Update()
    {
        ShowAmmo();
    }

    //display/change ammo text with current ammo and reserved ammo
    public void ShowAmmo()
    {
        // current Ammo/Reserved ammo
        currentAmmo.text = $" {playerShooting.currentAmmo} / {playerShooting.reservedAmmo}";
        
    
}
}
