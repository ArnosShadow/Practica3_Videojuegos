using System;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private ThirdPersonController controller;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        controller.OnUpdateMovement += UpdateMovementAnimations;
    }

    private void UpdateMovementAnimations(float velocityValue)
    {
        // Actualización de la animación
        anim.SetFloat("Velocity", velocityValue);
    }

    void OnDisable()
    {
        controller.OnUpdateMovement -= UpdateMovementAnimations;
    }
}
