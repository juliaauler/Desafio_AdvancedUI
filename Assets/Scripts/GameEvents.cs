using System;
using UnityEngine;

public class GameEvents : MonoBehaviour  {
    public static GameEvents Current;

    private void Awake() {
        Current = this;
    }

    public event Action OnChestTriggerEnter;
    public void ChestTriggerEnter() {
        if (OnChestTriggerEnter != null) {
            OnChestTriggerEnter();
        }
    }
    
    public event Action OnChestTriggerExit;
    public void ChestTriggerExit() {
        if (OnChestTriggerExit != null) {
            OnChestTriggerExit();
        }
    }
}
