using UnityEngine;

public class WorldBillboardUI : MonoBehaviour {
    private void LateUpdate() {
        Vector3 target = transform.position + Camera.main.transform.forward;
        transform.LookAt(target, Camera.main.transform.up);
    }
}
