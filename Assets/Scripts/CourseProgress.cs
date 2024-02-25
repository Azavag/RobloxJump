using System;
using UnityEngine;

public class CourseProgress : MonoBehaviour
{
    public static event Action<int, Transform> EnteringCourse;
    public static event Action< Vector3> RunningCourse;
    public static event Action ExitingCourse;

    int distanceLenght;

    // Start is called before the first frame update
    void Start()
    {
        distanceLenght = (int)transform.localScale.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            EnteringCourse?.Invoke(distanceLenght, other.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RunningCourse?.Invoke(other.transform.position);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ExitingCourse?.Invoke();
        }
    }


}
