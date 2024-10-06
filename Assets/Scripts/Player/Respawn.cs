using UnityEngine;
using UnityEngine.SceneManagement;


public class Respawn : MonoBehaviour
{
    public int isRewarded=0;
    private Health playerHealth;
    private Rigidbody2D body;
    //public Vector2 Checkpoint;
    ///[SerializeField] GameObject Hero;
    private UIManager uiManager;

    [SerializeField] private AudioClip kiss;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        uiManager = FindObjectOfType<UIManager>();      
    }

    public void RespawnAtCheckpoint()
    {

           uiManager.GameOver();
           //PlayerPrefs.SetInt("isRewarded",1);
           //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            collision.GetComponent<Collider2D>().enabled = false;
            if(collision.GetComponent<Animator>() != null){
                collision.GetComponent<Animator>().SetTrigger("checked");
                SoundManager.instance.PlaySound(kiss);    
            }
        }
    }

  /*  public void ReloadLevelFromCheckpointWrapper()
    {
            SceneController.instance.ReloadLevelFromCheckpoint();
    }*/
}
