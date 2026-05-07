using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    void Update()
    {
        // Rotate camera with mouse
        float mouseX = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;
        transform.Rotate(-mouseY, mouseX, 0f);

        // Raycast from camera forward
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // Change color of hit object to red
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
                rend.material.color = Color.red;
        }
    }
}

