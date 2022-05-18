using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTriggerArea : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GameEvents.Current.ChestTriggerEnter();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            GameEvents.Current.ChestTriggerExit();
        }
    }
}