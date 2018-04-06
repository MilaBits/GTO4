using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PopupController : MonoBehaviour {
    private static Popup bouncingPopup;
    private static Popup slidingPopup;
    private static Canvas canvas;

    private static bool _raise;

    public static void Initialize() {
        bouncingPopup = Resources.Load<Popup>("Prefabs/BouncePopup");
        slidingPopup = Resources.Load<Popup>("Prefabs/SlidePopup");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public static void CreatePopup(string text, Transform location) {
        if (!bouncingPopup) Initialize();

        Popup instance = Instantiate(bouncingPopup);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.PopupText.text = text;
    }

    public static void CreateSlidingPopup(string text, Color color) {
        if (!slidingPopup) Initialize();

        Popup instance = Instantiate(slidingPopup);
        instance.transform.SetParent(canvas.transform);

        Vector2 pos = canvas.pixelRect.center;
        if (_raise) {
            pos.y += canvas.pixelRect.height / 5;
        }
        else {
            pos.y += canvas.pixelRect.height / 5 / 2;
        }

        _raise = !_raise;
        pos.y += canvas.pixelRect.height / 5;
        instance.transform.position = pos;
        instance.PopupText.text = text;
        instance.PopupText.color = color;
    }
}