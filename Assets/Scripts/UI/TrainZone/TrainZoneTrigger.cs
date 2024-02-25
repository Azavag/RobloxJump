using System;
using UnityEngine;

public class TrainZoneTrigger : MonoBehaviour
{
    [SerializeField]
    private TrainZone trainZone;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            trainZone.MovePlayerToTrainZone();
        }
    }
 
}
