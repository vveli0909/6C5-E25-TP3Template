using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EscapeRoomAgent : Agent
{
    [SerializeField] private Transform switchTransform;
    [SerializeField] private Transform exitTransform;
    [SerializeField] private DoorSwitch doorSwitch;
    [SerializeField] private FloorColorManager floorFeedback;
    [SerializeField] private float speed = 5f;
    private Rigidbody rb;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // Position de départ fixe
        transform.localPosition = new Vector3(-2, 1, 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Observations :
        sensor.AddObservation(transform.localPosition);             // 3
        sensor.AddObservation(switchTransform.localPosition);       // 3
        sensor.AddObservation(exitTransform.localPosition);         // 3
        sensor.AddObservation(doorSwitch.IsDoorOpen() ? 1f : 0f);   // 1
        // Total : 10 (on peut simplifier plus tard à 7 ou 9 si besoin)
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized;

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        // Récompense légère pour encourager l'action rapide
        AddReward(-0.001f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxisRaw("Horizontal");
        continuousActionsOut[1] = Input.GetAxisRaw("Vertical");
        Debug.Log($"[Heuristic] X: {continuousActionsOut[0]}, Z: {continuousActionsOut[1]}");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("goal"))
        {
            if (doorSwitch.IsDoorOpen())
            {
                AddReward(+1f);
                floorFeedback.SetSuccess();
            }
            else
            {
                AddReward(-1f);
                floorFeedback.SetFail();
            }
            doorSwitch.CloseDoor();
            EndEpisode();
        }
    }
}
