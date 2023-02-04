using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 5;

    [HideInInspector]
    public GameObject bulletHitEffect;

    [HideInInspector]
    public CFXR_ParticleText damageText;


    public void SetDamagetext(CFXR_ParticleText effect)
    {
        damageText = effect;
    }
    public void SpawnBullet(int _damage)
    {
        damage = _damage;
    }

    public void SetHitEffect(GameObject effect)
    {
        bulletHitEffect = effect;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Unit>().DoDamage(damage);

            if (bulletHitEffect != null)
            {
                Vector3 spawnPosition = transform.position + new Vector3(0, 0.25f, 0);
                Vector3 damageSpawnPosition = transform.position + new Vector3(0, 1f, 0);
                Instantiate(bulletHitEffect, spawnPosition, Quaternion.identity);
                CFXR_ParticleText temp = Instantiate(damageText, damageSpawnPosition, Quaternion.identity);
                temp.UpdateText(damage.ToString());
                Destroy(temp.gameObject, .5f);
            }
            Destroy(gameObject);
        }
    }
}
