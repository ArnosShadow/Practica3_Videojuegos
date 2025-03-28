using System;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] Camera TPC;
    [SerializeField] Camera FPC;
    private bool isFirstPerson = false; // Comienza en tercera persona
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TPC.enabled = true;
        FPC.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Detecta si se presiona la tecla TAB
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        isFirstPerson = !isFirstPerson; // Cambia el estado

        // Activa una cámara y desactiva la otra
        TPC.enabled = !isFirstPerson;
        FPC.enabled = isFirstPerson;
    }
}
