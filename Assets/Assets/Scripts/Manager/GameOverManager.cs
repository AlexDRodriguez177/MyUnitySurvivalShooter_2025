using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float restartDelay = 5f;
    Animator anim;
    float restartTimer;

     private void Awake()
    {
        anim= GetComponent<Animator>();
    }
    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            restartTimer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Restarting Level...");
                SceneManager.LoadScene(0);

            }
        }

    }
}
