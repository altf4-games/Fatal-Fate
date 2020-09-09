using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBase : MonoBehaviour
{
    [SerializeField] private List<AxleInfo> axleInfos;
    [SerializeField] private float maxMotorTorque = 280; 
    [SerializeField] private float maxSteeringAngle = 30;
    [HideInInspector] public Transform path;
    [HideInInspector] public TrafficSystem tf;

    private List<Transform> nodes;
    private int currectNode = 0;
    private WheelCollider wheelFL;
    private WheelCollider wheelFR;
    private WheelCollider wheelRL;
    private WheelCollider wheelRR;
    private float currentSpeed;
    private float maxSpeed = 100f;

    private void Start()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                wheelFL = axleInfo.leftWheel;
                wheelFR = axleInfo.rightWheel;
            }
            if (axleInfo.motor)
            {
                wheelRL = axleInfo.leftWheel;
                wheelRR = axleInfo.rightWheel;
            }
        }

        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }

        maxMotorTorque = Random.Range(maxMotorTorque - 40, maxMotorTorque + 65);
    }

    public void FixedUpdate()
    {
        ApplyLocalPositionToVisuals(wheelFL);
        ApplyLocalPositionToVisuals(wheelFR);
        ApplyLocalPositionToVisuals(wheelRL);
        ApplyLocalPositionToVisuals(wheelRR);

        ApplySteer();
        Drive();
        CheckWaypointDistance();
        DestroyOnReach();
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteeringAngle;

        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelRR.radius * wheelRR.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed)
        {
            wheelRL.motorTorque = maxMotorTorque;
            wheelRR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
        }
    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currectNode].position) < 0.5f)
        {
            if (currectNode == nodes.Count - 1)
            {
                currectNode = 0;
            }
            else
            {
                currectNode++;
            }
        }
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    private void DestroyOnReach()
    {
        if(Vector3.Distance(transform.position, nodes[0].position) <= 2)
        {
            tf.traffic.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}