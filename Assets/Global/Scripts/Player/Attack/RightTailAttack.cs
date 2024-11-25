using System.Collections;
using UnityEngine;

public class RightTailAttack : TailAttack
{
    public bool shouldrun = true;

    public void Start()
    {
        Enter();
        StartCoroutine(SmoothTurnCoroutine(-180, player.tailTurnDuration));
    }

    private IEnumerator SmoothTurnCoroutine(float degrees, float duration)
    {
        float elapsedTime = 0f;
        float initialRotation = tailTransform.eulerAngles.y;
        float targetRotation = initialRotation + degrees;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentAngle = Mathf.Lerp(
                initialRotation,
                targetRotation,
                elapsedTime / duration
            );
            Turn(currentAngle - tailTransform.eulerAngles.y);
            yield return null;
        }
        if (shouldrun)
        {
            shouldrun = false;
            StartCoroutine(SmoothTurnCoroutine(180, player.tailTurnDuration));
        }
        else
        {
            Exit();
        }
    }
}
