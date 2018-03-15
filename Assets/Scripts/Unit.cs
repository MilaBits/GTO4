using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Ownable
{

    public float movementSpeed = 1f;

    private bool move;
    private Transform target;

    public void Move(Transform target) {
        transform.parent = target.transform;
        StartCoroutine(MoveToPosition(transform.position, target.transform.position, movementSpeed));
    }

    IEnumerator MoveToPosition(Vector3 startPosition, Vector3 targetPosition, float time) {
        float startTime = Time.time;
        while (Time.time < startTime + time) {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (Time.time - startTime) / time);
            yield return null;
        }
        transform.position = targetPosition;
    }
}
