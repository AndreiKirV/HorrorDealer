using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MinionMB : MonoBehaviour, IPointerUpHandler
{
    public NavMeshAgent Agent;

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnMouseEnter()
    {
        Debug.Log("Enter");
    }

    private void OnMouseDown()
    {
        Debug.Log("down");
    }

    private void OnMouseUp()
    {
        Debug.Log("up");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");
    }
}