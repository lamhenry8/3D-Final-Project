using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorOpen : MonoBehaviour
{
    [SerializeField] private string openParameter = "IsOpen";

    private Animator animator;
    private bool isOpen;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        if (isOpen)
        {
            return;
        }

        isOpen = true;
        animator.SetBool(openParameter, true);
    }
}