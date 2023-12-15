using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Inventory inventory;
    public float moveSpeed = 5f; // Adjust the speed in the Unity Editor if needed


    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            inventory.AddItem(item);
            Destroy(other.gameObject); // Remove the item from the scene
        }
    }

    private void Update()
    {
        // Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Movement
        Vector2 movement = new Vector2(horizontal, vertical);
        movement.Normalize(); // Normalize to prevent faster diagonal movement

        transform.position = movement * moveSpeed;
    }
}
