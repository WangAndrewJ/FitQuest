using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public LayerMask playerLayerMask;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("a");
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.Damage(damage);
            Destroy(gameObject);
        }
    }
}
