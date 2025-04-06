using System;
using UnityEngine;
using UnityEngine.AI;

public class ArcherController : MonoBehaviour
{
    [SerializeField] public float DetectionRange = 15f;
    [SerializeField] public float AttackRange = 10f;
    [SerializeField] public float RetreatDistance = 3f;
    [SerializeField] public float ShootCooldown = 5f;
    [SerializeField] public float health = 50f;

    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowSpawnPoint;

    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform _player;
    private float _shootTimer;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _shootTimer = ShootCooldown;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        _shootTimer -= Time.deltaTime;

        if (distanceToPlayer < AttackRange)
        {
            _agent.SetDestination(transform.position); // No avanzar
            transform.LookAt(_player); // Mirar al jugador

            if (_shootTimer <= 0f)
            {
                ShootArrow();
                Retreat();
                _shootTimer = ShootCooldown;
            }

            _animator.SetTrigger("Idle");
        }
        else if (distanceToPlayer < DetectionRange)
        {
            _agent.SetDestination(_player.position);
            _animator.SetTrigger("Crouched Walking");
        }
        else
        {
            _agent.SetDestination(transform.position);
            _animator.SetTrigger("Idle");
        }
    }

    private void ShootArrow()
    {
        _animator.SetTrigger("Shoot");

        if (arrowPrefab != null && arrowSpawnPoint != null)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = arrow.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = (_player.position - arrowSpawnPoint.position).normalized;
                rb.linearVelocity = direction * 20f; // velocidad de la flecha, ajustable
            }
        }
    }

    private void Retreat()
    {
        _animator.SetTrigger("Running Backward");
        Vector3 dirFromPlayer = (transform.position - _player.position).normalized;
        Vector3 retreatPos = transform.position + dirFromPlayer * RetreatDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(retreatPos, out hit, 2f, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // La arquera muere de un toque.
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetTrigger("Die");
            _agent.enabled = false; // desactivar el agente de navmesh para que no se mueva al morir
            Destroy(gameObject, 2f); // destruir el objeto despues de 2 segundos
        }
    }
}
