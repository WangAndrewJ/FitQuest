using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage;
    public int enemyMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyMask)
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.Damage(damage);
        }

        Destroy(gameObject);
    }
}
