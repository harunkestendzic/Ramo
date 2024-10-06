using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    [SerializeField] private Transform point;
    private Health enHealth;

    void Awake()
    {
        enHealth = GetComponent<Health>();
    }

    public void EnemyRespawn()
    {
        
        transform.position = point.position;
        enHealth.Respawn();
    }
    
}
