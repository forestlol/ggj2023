using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

public class Enemy_Turret : EnemyController
{
    [SerializeField]
    int damage = 1;

    [SerializeField]
    float projectile_speed = 3;

    [SerializeField]
    private GameObject enemy_bullet_Prefab;

    [SerializeField]
    float second_per_shot = 1;

    float currentSecond;

    [SerializeField]
    float radius = 10;

    [Header("Damage Effect")]
    public CFXR_ParticleText effect;

    public GameObject hitEffect;

    private void Start()
    {
        currentSecond = second_per_shot;
    }

    private void Update()
    {
        currentSecond -= Time.deltaTime;

        if(currentSecond < 0)
        {
            ShootAtPlayer();
            currentSecond = second_per_shot; 
        }
    }

    private void ShootAtPlayer()
    {
        LayerMask worldLayer = LayerMask.NameToLayer("Player");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, 1 << worldLayer);

        if (hitColliders.Length == 0)
        {
            return;
        }


        Vector3 direction;
        direction = hitColliders[0].transform.position - transform.position;
        direction.y = 0;
        transform.forward = direction;

        GameObject bullet = GameObject.Instantiate(enemy_bullet_Prefab, transform.position, Quaternion.identity);
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();

        rigid.GetComponent<EnemyBullet>().SetDamagetext(effect);
        rigid.GetComponent<EnemyBullet>().SpawnBullet(damage);
        rigid.GetComponent<EnemyBullet>().SetHitEffect(hitEffect);


        bullet.GetComponent<Rigidbody>().velocity = direction * projectile_speed;
        bullet.transform.forward = direction;
    }
}
