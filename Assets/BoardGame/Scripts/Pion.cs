using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public Transform startPosition;
    public Transform[] boardPositions;
    private int currentPositionIndex = 0;
    public DiceRoller diceRoller;
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float jumpDuration = 0.5f;
    private bool isMoving = false;
    private int moveSteps = 0;

    void Start()
    {
        ResetPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && !isMoving)
        {
            int diceResult = diceRoller.diceResult;
            if (diceResult > 0)
            {
                moveSteps = diceResult;
                StartCoroutine(MovePawn());
            }
        }
    }

    void ResetPosition()
    {
        if (startPosition != null)
        {
            transform.position = startPosition.position;
            currentPositionIndex = 0;
        }
    }

    IEnumerator MovePawn()
    {
        isMoving = true;

        while (moveSteps > 0 && currentPositionIndex < boardPositions.Length - 1)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = boardPositions[++currentPositionIndex].position;
            yield return StartCoroutine(JumpToPosition(startPos, endPos));

            moveSteps--;
        }

        isMoving = false;
    }

    IEnumerator JumpToPosition(Vector3 startPosition, Vector3 endPosition)
    {
        float timeElapsed = 0f;
        Vector3 midpoint = (startPosition + endPosition) / 2 + Vector3.up * jumpHeight;

        while (timeElapsed < jumpDuration)
        {
            float t = timeElapsed / jumpDuration;
            transform.position = Vector3.Lerp(
                Vector3.Lerp(startPosition, midpoint, t),
                Vector3.Lerp(midpoint, endPosition, t),
                t
            );
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
    }
}
