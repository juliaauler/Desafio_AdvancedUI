using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

    public bool isEnabled;
    public Animator animator;
    private CanvasGroup _chestIcon;

    private void Start() {
        GameEvents.Current.OnChestTriggerEnter += OnChestEnter;
        GameEvents.Current.OnChestTriggerExit += OnChestExit;
        _chestIcon = gameObject.GetComponentInChildren<CanvasGroup>();
        _chestIcon.alpha = 0f;
    }

    private void OnChestEnter() {
        isEnabled = true;
        StartCoroutine(ShowIconCoroutine(true));
    }

    private void OnChestExit() {
        isEnabled = false;
        StartCoroutine(ShowIconCoroutine(false));
    }

    public void ChestInteraction() {
        if (isEnabled) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Close")) {
                animator.SetBool("Interaction", true);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open")) {
                animator.SetBool("Interaction", false);
            }
        }
    }
    
    IEnumerator ShowIconCoroutine(bool show) {
        if (!show) {
            while (_chestIcon.alpha > 0f) {
                _chestIcon.alpha -= Mathf.Lerp(0f, 1f, 0.001f);
            }
        }
            
        if (show) {
            while (_chestIcon.alpha < 1f) {
                _chestIcon.alpha += Mathf.Lerp(0f, 1f, 0.01f);
            }
        }

        yield return null;
    }
}