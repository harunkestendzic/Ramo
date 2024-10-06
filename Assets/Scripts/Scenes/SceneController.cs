using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;

    private void Awake(){
    Time.timeScale=1;
        if(instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel(){
        StartCoroutine(LoadLevel());
    }

        IEnumerator LoadLevel(){
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForEndOfFrame(); 
        transitionAnim.SetTrigger("Start");
    }

    public void ReloadLevelFromCheckpoint()
    {
        StartCoroutine(LoadLevelFromCheckpoint());
    }

    IEnumerator LoadLevelFromCheckpoint()
{
    transitionAnim.SetTrigger("End");
    yield return new WaitForSeconds(1);
    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    Debug.Log("Scene loading started");
    yield return new WaitForEndOfFrame();
    Debug.Log("Scene loaded");
    FindObjectOfType<Respawn>().RespawnAtCheckpoint();
    transitionAnim.SetTrigger("Start");
}

}
