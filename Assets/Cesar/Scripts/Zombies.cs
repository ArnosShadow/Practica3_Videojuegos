using System;
using UnityEngine;
using UnityEngine.AI;

public class ZombiesController: MonoBehaviour
{
    [SerializeField] public float DetectionRange = 10f; //rango para detectar al jugador

    [SerializeField] public float AttackRange = 2f; //rango de ataque

    [SerializeField] public float WalkDuration = 3f; //duracion del movimiento aleatorio

    [SerializeField] public float IdleDuration = 3f; //duracion del estado idle

    [SerializeField] float randomX = UnityEngine.Random.Range(-5f, 5f);

    [SerializeField] float randomZ = UnityEngine.Random.Range(-5f, 5f);


    private Animator _animator;

    private NavMeshAgent _agent;

    private Transform _player;

    private float _walkTimer;

    private float _idleTimer;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform; //asegurate de que el player tiene la etiqueta de "Player"
        _walkTimer = WalkDuration;
        _idleTimer = IdleDuration;
    }

    void Update()
    {
        float _distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (_distanceToPlayer < AttackRange)
        {
            //atacar al jugador
            _animator.SetTrigger("Punch");
        }

        else if (_distanceToPlayer < DetectionRange) 
        {
            //correr hacia el jugador
            _agent.SetDestination(_player.position);
            _animator.SetTrigger("Running");
        }

        else
        {
            //movimiento aleatorio
            _animator.SetTrigger("Walk");
            _walkTimer -= Time.deltaTime;
            _idleTimer -= Time.deltaTime;

            if (_walkTimer <= 0)
            {
                MoveRandomly();
                _walkTimer = WalkDuration; //reiniciar el temporizador de caminar
            }

            if (_idleTimer <= 0)
            {
                _animator.SetTrigger("Idle");
                _idleTimer = IdleDuration;
            }
        }

    }

    private void MoveRandomly()
    {
        Vector3 randomDirection = new Vector3(randomX, 0, randomZ) + transform.position;

        NavMeshHit hit; //variable para almacenar el resultado de la prueba de Navmesh

        if (NavMesh.SamplePosition(randomDirection, out hit, 5, NavMesh.AllAreas)) //muestra una posicion valida en el NavMesh
        {
            _agent.SetDestination(hit.position); //mover al anemigo a la posicion seleccionada

            _animator.SetTrigger("Walk");
        }

        _idleTimer = IdleDuration;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //aqui puedes añadir logica
            //adicional
        }
    }

}
