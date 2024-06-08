using UnityEngine;

public class Rocker : MonoBehaviour
{
    private bool cooldownActive = false;

    void Start() { }
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        if (!cooldownActive && 
            (other.gameObject.name.Contains("Pinch")
            || other.gameObject.CompareTag("smallControllerTrigger")))
        {
            ToggleRocker();
        }
    }

    public void ToggleRocker()
    {
        // Find our parent and toggle its state
        var script = transform.parent.GetComponent<CircuitComponent>();
        if (script != null)
        {
            script.Toggle();

            // Set the rocker to the proper position by rotating the pivot
            var rotation = transform.localEulerAngles;
            rotation.y = -rotation.y;
            transform.localEulerAngles = rotation;

            cooldownActive = true;
            Invoke("Cooldown", 0.5f);
        }
    }

    void Cooldown()
    {
        cooldownActive = false;
    }
}
