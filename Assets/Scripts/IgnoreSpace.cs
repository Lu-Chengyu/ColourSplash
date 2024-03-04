using UnityEngine;
using UnityEngine.EventSystems; 

public class IgnoreSpace : MonoBehaviour, IUpdateSelectedHandler
{
    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key event consumed.");
            eventData.Use();
        }
    }
}
