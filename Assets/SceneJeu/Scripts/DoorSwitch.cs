using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Material openMat;
    [SerializeField] private Material closedMat;
    private bool doorOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("je suis entrew trigger");

        if (other.CompareTag("Player")) // Assure-toi que ton agent est tagué "Player"
        {
            doorOpen = !doorOpen;
            UpdateDoorColor();
        }
    }

    private void UpdateDoorColor()
    {
        MeshRenderer renderer = door.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = doorOpen ? openMat : closedMat;
            Debug.Log("je suis entrer ");
        }
    }

    public bool IsDoorOpen()
    {
        return doorOpen;

    }
    public void CloseDoor()
    {
        doorOpen = false;
        UpdateDoorColor();

    }
}
