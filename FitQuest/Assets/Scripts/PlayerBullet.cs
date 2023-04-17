using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage;
    public int enemyMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);

        if (collision.gameObject.layer == enemyMask)
        {
            Debug.Log("b");
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.Damage(damage);
        }

        Destroy(gameObject);
    }
}
