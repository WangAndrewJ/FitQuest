using UnityEngine;
using System.Collections.Generic;

public class PlayerShooter : MonoBehaviour
{
    public float fireRate = 1f;
    private float nextFire = 0f;
    public float bulletSpeed = 10f;
    public GameObject bullet;
    public EnemyManager myEnemyManager;
    public float detectRange;
    public VariableJoystick myVariableJoystick;

    private void Update()
    {
        Transform closestEnemy = GetClosestEnemy(myEnemyManager.enemies);

        if (GetClosestEnemy(myEnemyManager.enemies) != null)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, closestEnemy.position - transform.position);
        }
        else
        {
            transform.right = Vector2.right * myVariableJoystick.Vertical + Vector2.down * myVariableJoystick.Horizontal;
        }

        if (Time.time > nextFire)
        {
            Debug.Log($"Fire: {Time.time}");
            float attackSpeedMultiplier = PlayerPrefs.GetInt("Attack Speed", 20) / 20f;
            Debug.Log("ASPDM: " + attackSpeedMultiplier);
            Debug.Log("FR: " + fireRate / attackSpeedMultiplier);
            nextFire = Time.time + fireRate / attackSpeedMultiplier;
            GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
            spawnedBullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
            float attackMultiplier = PlayerPrefs.GetInt("Attack", 20) / 20f;
            PlayerBullet playerBullet = spawnedBullet.GetComponent<PlayerBullet>();
            playerBullet.damage = Mathf.RoundToInt(playerBullet.damage * attackMultiplier);
        }
    }

    Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform tMin = null;
        float minDist = detectRange;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }

        return tMin;
    }
}
