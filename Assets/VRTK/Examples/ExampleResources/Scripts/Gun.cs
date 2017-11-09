namespace VRTK.Examples
{
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

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            FireBullet();
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
            Ray r = new Ray(muzzlePoint.transform.position, muzzlePoint.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(r, out hit))
            {
                lineRenderer.SetPosition(0, muzzlePoint.transform.position);
                lineRenderer.SetPosition(1, hit.transform.position);
            }
            
            bulletClone.SetActive(true);
            Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * bulletSpeed);

            audio.Play();
            Destroy(bulletClone, bulletLife);
        }
    }
}