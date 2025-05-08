using UnityEngine;
using GameCreator.Runtime.Inventory.UnityUI;
using UnityEngine.UI;

public class RotateGameObjects : MonoBehaviour
{
    public GameObject[] gameObjects;
    public int middleIndex = 1;

    // Public fields for key mappings with default values for mouse wheel
    public KeyCode scrollUpKey = KeyCode.Mouse3; // Default for mouse wheel up
    public KeyCode scrollDownKey = KeyCode.Mouse4; // Default for mouse wheel down

    private void Start()
    {
        UpdateObjectProperties();
    }

    private void Update()
    {
        // Check for custom key input or mouse scroll
        if (Input.GetKeyDown(scrollUpKey) || Input.mouseScrollDelta.y > 0)
        {
            RotateObjects(true);
        }
        else if (Input.GetKeyDown(scrollDownKey) || Input.mouseScrollDelta.y < 0)
        {
            RotateObjects(false);
        }
    }

    private void RotateObjects(bool scrollUp)
    {
        if (scrollUp)
        {
            middleIndex = (middleIndex - 1 + gameObjects.Length) % gameObjects.Length;
        }
        else
        {
            middleIndex = (middleIndex + 1) % gameObjects.Length;
        }

        UpdateObjectProperties();
    }

    private void UpdateObjectProperties()
    {
        // Reset all objects to default properties first
        for (int i = 0; i < gameObjects.Length; i++)
        {
            CanvasGroup canvasGroup = gameObjects[i].GetComponent<CanvasGroup>();
            RectTransform rectTransform = gameObjects[i].GetComponent<RectTransform>();
            BagEquipUI bagEquipUI = gameObjects[i].GetComponent<BagEquipUI>();

            canvasGroup.alpha = 0.6f;
            rectTransform.sizeDelta = new Vector2(100, 100);

            if (bagEquipUI != null)
            {
                bagEquipUI.inputEnabled = false;
            }
        }

        // Set the middle object properties
        int previousIndex = (middleIndex - 1 + gameObjects.Length) % gameObjects.Length;
        int nextIndex = (middleIndex + 1) % gameObjects.Length;

        CanvasGroup middleCanvasGroup = gameObjects[middleIndex].GetComponent<CanvasGroup>();
        RectTransform middleRectTransform = gameObjects[middleIndex].GetComponent<RectTransform>();
        BagEquipUI middleBagEquipUI = gameObjects[middleIndex].GetComponent<BagEquipUI>();

        middleCanvasGroup.alpha = 1.0f;
        middleRectTransform.sizeDelta = new Vector2(150, 150);

        if (middleBagEquipUI != null)
        {
            middleBagEquipUI.inputEnabled = true;
        }

        // Adjust the hierarchy order to ensure the middle object is always in the center
        gameObjects[middleIndex].transform.SetSiblingIndex(1);
        gameObjects[previousIndex].transform.SetSiblingIndex(0);
        gameObjects[nextIndex].transform.SetSiblingIndex(2);
    }
}
