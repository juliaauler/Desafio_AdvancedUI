using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEnemyLife : MonoBehaviour {
    private CanvasGroup _enemy;
    public LayerMask EnemyLayer;
    [Range(0, 1)] private float alphaValue = 0;
    private bool isShowing;

    void Start() {
        _enemy = GameObject.FindWithTag("EnemyCanvas").GetComponent<CanvasGroup>();
        isShowing = false;
    }

    void Update() {
        ShowEnemyHealthBar();
    }

    private void ShowEnemyHealthBar() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity,
                EnemyLayer, QueryTriggerInteraction.Collide)) {
            if (!isShowing) {
                StartCoroutine(ShowHealthCoroutine(true));

                isShowing = true;
            }
        }
        else if (!Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity,
                     EnemyLayer, QueryTriggerInteraction.Collide)) {
            StartCoroutine(ShowHealthCoroutine(false));

            isShowing = false;
        }

        IEnumerator ShowHealthCoroutine(bool show) {
            if (!show) {
                _enemy.alpha -= Mathf.Lerp(0, 1, Time.deltaTime);
            }
            
            if (show) {
                while (_enemy.alpha < 1) {
                    _enemy.alpha += Mathf.Lerp(0, 1, Time.deltaTime);
                }
            }

            yield return null;
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.red);
    }
}