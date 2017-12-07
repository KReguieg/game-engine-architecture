namespace VRTK.Examples
{
    using System;
    using UnityEngine;

    public class Gun : VRTK_InteractableObject
    {
        [SerializeField]
        private GameObject bullet;

        [SerializeField]
        private GameObject muzzlePoint;
        private float bulletSpeed = 1000f;
        private float bulletLife = 5f;

        [SerializeField]
        private AudioClip shotsFiredClip;

        private AudioSource audio;

        private LineRenderer lineRenderer;

        [SerializeField]
        private float damagePerShot = 8.0f; // <-- Haha Random Number by Moni^^

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            FireLaser();
            // FireBullet();
        }

        private void FireLaser()
        {
            Ray r = new Ray(muzzlePoint.transform.position, muzzlePoint.transform.forward);
            RaycastHit hit;

            if(Physics.Raycast(r, out hit))
            {
                if(hit.collider.CompareTag("Enemy"))
                {
                    hit.transform.gameObject.GetComponent<EnemyBehavior>().TakeDamage(damagePerShot);
                }
                lineRenderer.SetPosition(0, muzzlePoint.transform.position);
                lineRenderer.SetPosition(1, hit.transform.position);
            }
            
            audio.Play();
            
        }

        protected void Start()
        {
            bullet = transform.Find("C_Bullet").gameObject;
            bullet.SetActive(false);
            audio = GetComponent<AudioSource>();
            audio.clip = shotsFiredClip;
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void FireBullet()
        {
            GameObject bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;

            bulletClone.SetActive(true);
            Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * bulletSpeed);

            Destroy(bulletClone, bulletLife);
        }
    }
}