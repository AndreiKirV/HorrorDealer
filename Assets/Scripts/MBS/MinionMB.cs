using System;
using UnityEngine;
using UnityEngine.AI;

public class MinionMB : MonoBehaviour
{
    public NavMeshAgent Agent;
    private bool _isSelected = false;
    private InputSettings _input;

    private void Awake()
    {
        _input = new InputSettings();
        _input.Enable();
    }

    private void Update()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log("down");
        _isSelected = true;
        _input.Mouse.RB.started += GoMove;
        _input.Mouse.LB.started += Deselect;
        _input.Enable();
    }

    private void GoMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!_isSelected) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Если луч пересекает объект с коллайдером, выводим точку попадания в лог
            Debug.Log($"Moving to {hit.point}");

            // Для демонстрации — перемещение миньона к точке на NavMesh
            Agent.SetDestination(hit.point);
        }
    }

    private void Deselect(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _isSelected = false;
        _input.Mouse.RB.started -= GoMove;
        _input.Mouse.LB.started -= Deselect;
    }
}