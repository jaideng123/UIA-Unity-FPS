using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour {
    private Camera _camera;
    private bool hit;

    // Use this for initialization
    void Start() {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = new Color(0, 0, 0);
        int size = 20;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*", style);
        if (hit) {
            GUI.Label(new Rect(posX - 3f, posY - 3f, size, size), "O", style);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Debug.Log("Hit " + hit.point);
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null) {
                    Debug.Log("Target Hit");
                    if (!target.dead) {
                        StartCoroutine(HitIndicator());
                    }
                    target.ReactToHit();
                } else {
                    StartCoroutine(SphereIndicator(hit.point));
                }

            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos) {
        Debug.Log("In Coroutine");
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);
        Debug.Log("After Yield");

        Destroy(sphere);
    }

    private IEnumerator HitIndicator() {
        Debug.Log("In Coroutine");
        hit = true;
        yield return new WaitForSeconds(.1f);
        hit = false;
        Debug.Log("After Yield");
    }
}
