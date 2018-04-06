using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour {
    public Animator _animator;
    public Text PopupText;

    void OnEnable() {
        AnimatorClipInfo clipInfo = _animator.GetCurrentAnimatorClipInfo(0).First();
        Destroy(gameObject, clipInfo.clip.length);

        PopupText = _animator.GetComponent<Text>();
    }
}