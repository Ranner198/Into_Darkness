using UnityEngine;

using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    public float Range = 100;
    public float ShootingDelay = 0.1f;

    private Camera _camera;
    private ParticleSystem _particle;
    private LayerMask _shootableMask;
    private float _timer;

    void Start()
    {
        _camera = Camera.main;
        _particle = GetComponentInChildren<ParticleSystem>();
        Cursor.lockState = CursorLockMode.Locked;
        _shootableMask = LayerMask.GetMask("Shootable");
        _timer = 0;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && _timer >= ShootingDelay)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _timer = 0;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, Range, _shootableMask))
        {
            print("hit " + hit.collider.gameObject);
            _particle.Play();

            EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
            EnemyClass enemy = new EnemyClass(12, 100); 

            if (enemy != null)
            {
                //enemy.KnockBack();
            }
            if (health != null)
            {
                health.TakeDamage(1);
            }
        }
    }
}