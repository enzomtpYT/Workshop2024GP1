using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : MonoBehaviour
{
    public float duration = .5f;
    public bool finished = true;

    Vector3 startPos;

    float easeOutExpo(float x)
    {
        return x == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * x);
    }

    IEnumerator MovementCoroutine(Vector3 position)
    {
        finished = false;
        startPos = this.transform.position;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float progress = easeOutExpo((Time.time - startTime) / duration);
            this.transform.position = Vector3.Lerp(startPos, position, progress);
            yield return null;
        }
        finished = true;
    }

    public void MoveTo(Vector3 position) {
        StartCoroutine(MovementCoroutine(position));
    }

    private void Update() {
        // if (Input.anyKeyDown && finished) { // works poggeg
        //     Debug.Log("tut");
        //     finished = false;
        //     MoveTo(this.transform.position + new Vector3(1, 0, 0));
        // }
    }
}
