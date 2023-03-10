using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : Bullet
{
    List<GameObject> m_EnemyControllers = new List<GameObject>();

    private new void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        Explode();
    }

    void Explode()
    {
        AudioManager.instance.PlaySound("explosion", transform);

        if(m_EnemyControllers.Count == 0)
        {
            if (bulletHitEffect != null)
            {
                Vector3 spawnPosition = transform.position + new Vector3(0, 0.25f, 0);
                Instantiate(bulletHitEffect, spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            int damage = m_EnemyControllers.Count * bulletDamage;
            for (int i = 0; i < m_EnemyControllers.Count; ++i)
            {
                m_EnemyControllers[i].GetComponent<EnemyController>().DoDamage(bulletDamage);
            }

            if (bulletHitEffect != null)
            {
                Vector3 spawnPosition = transform.position + new Vector3(0, 0.25f, 0);
                Vector3 damageSpawnPosition = transform.position + new Vector3(0, 1f, 0);
                Instantiate(bulletHitEffect, spawnPosition, Quaternion.identity);
                CFXR_ParticleText temp = Instantiate(damageText, damageSpawnPosition, Quaternion.identity);
                temp.UpdateText(damage.ToString());
                Destroy(temp.gameObject, 0.5f);
            }

        }
    }

    public new void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            m_EnemyControllers.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            m_EnemyControllers.Remove(other.gameObject);
        }
    }

    new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
            Destroy(this.gameObject);
        }
    }
}
