using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    public float knockbackTime = 0.2f;
    public float hitDirectionForce = 10f;
    public float constForce = 5f;
    public float inputForce = 7.5f;
    public AnimationCurve knockbackForceCurve;

    public bool isBeingKnockedBack { get; private set; }

    private Rigidbody2D rb;
    private Coroutine knockbackCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void CallKnockback(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        if (knockbackCoroutine != null)
            StopCoroutine(knockbackCoroutine);

        knockbackCoroutine = StartCoroutine(KnockbackAction(hitDirection, constantForceDirection, inputDirection));
    }

    private IEnumerator KnockbackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        float elapsedTime = 0f;
        float time = 0f;

        isBeingKnockedBack = true;

        Vector2 hitForce;
        Vector2 constantForce;
        Vector2 knockbackForce;
        Vector2 combinedForce;

        hitForce = hitDirection * hitDirectionForce;
        constantForce = constantForceDirection * constForce;
        
        while (elapsedTime < knockbackTime)
        {
            elapsedTime += Time.fixedDeltaTime;
            knockbackForce = hitForce + constantForce;

            if (inputDirection != 0)
                combinedForce = knockbackForce + new Vector2(inputDirection, 0f);
            else
            {
                combinedForce = knockbackForce;
            }

            rb.linearVelocity = combinedForce * knockbackForceCurve.Evaluate(time); 

            yield return new WaitForFixedUpdate();
        }

        isBeingKnockedBack = false;
    }
}
