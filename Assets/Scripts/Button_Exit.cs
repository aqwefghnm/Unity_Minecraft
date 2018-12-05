using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Button_Exit : MonoBehaviour, IPointerClickHandler{
    public void OnPointerClick(PointerEventData e)
    {
        Application.Quit();
    }
}
