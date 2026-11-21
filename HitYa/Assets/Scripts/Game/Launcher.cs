using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Launcher : MonoBehaviour
{
    [SerializeField] private float powerMultiplier = 0.3f;
    [SerializeField] private float distanceMultiplier = 1f; // Adjusts Unity units to displayed meters
    [SerializeField] private float stopVelocityThreshold = 0.15f;
    [SerializeField] private float stopConfirmTime = 0.5f;

    public int CurrentDistance { get; private set; }
    public event Action<int> OnLanded;

    private Rigidbody2D rb;
    private float startX;
    private bool isFlying;
    private float flightTimer;
    private float stoppedTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }

    public void Launch(double angleDegrees, double power)
    {
        startX = transform.position.x;
        CurrentDistance = 0;
        flightTimer = 0f;
        stoppedTimer = 0f;
        isFlying = true;

        rb.simulated = true;

        float radians = (float)angleDegrees * Mathf.Deg2Rad;
        float speed = (float)power * powerMultiplier;

        rb.linearVelocity = new Vector2(
            Mathf.Cos(radians) * speed,
            Mathf.Sin(radians) * speed
        );
    }

    void FixedUpdate()
    {
        if (!isFlying) return;

        flightTimer += Time.fixedDeltaTime;

        // Track horizontal distance from launch point
        float rawDistance = (transform.position.x - startX) * distanceMultiplier;
        CurrentDistance = Mathf.Max(0, Mathf.FloorToInt(rawDistance));

        // Wait briefly after launch before checking if we've stopped
        if (flightTimer < 0.3f) return;

        if (rb.linearVelocity.magnitude <= stopVelocityThreshold)
        {
            stoppedTimer += Time.fixedDeltaTime;
            if (stoppedTimer >= stopConfirmTime)
            {
                isFlying = false;
                rb.linearVelocity = Vector2.zero;
                rb.simulated = false;
                OnLanded?.Invoke(CurrentDistance);
            }
        }
        else
        {
            stoppedTimer = 0f; // Reset if it bounced or kept rolling
        }
    }
}