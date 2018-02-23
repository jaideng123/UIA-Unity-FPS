using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour {
    private Camera _camera;
    private bool hit;
    private LineRenderer _laser;

    // Use this for initialization
    void Start() {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _laser = GetComponent<LineRenderer>();
    }

    void OnGUI() {
        if (GlobalVariables.Paused) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !GlobalVariables.Paused) {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Debug.Log("Hit " + hit.point);
                StartCoroutine(LineIndicator(hit.point, transform.position));
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null) {
                    Debug.Log("Target Hit");
                    if (!target.dead) {
                        StartCoroutine(HitIndicator());
                    }
                    target.ReactToHit();
                    Messenger.Broadcast(GameEvent.ENEMY_HIT);
                } else {
                    //StartCoroutine(SphereIndicator(hit.point));
                }

            }
        }
    }

    private void ToggleEscape() {
    }

    private IEnumerator SphereIndicator(Vector3 pos) {
        Debug.Log("In Coroutine");
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);
        Debug.Log("After Yield");

        Destroy(sphere);
    }

    private IEnumerator LineIndicator(Vector3 startingPos, Vector3 endingPos) {
        Debug.Log("In Coroutine");
        Vector3[] laserPoints = new Vector3[2];
        laserPoints[0] = startingPos;
        laserPoints[1] = endingPos;
        _laser.SetPositions(laserPoints);
        yield return new WaitForSeconds(.1f);
        _laser.SetPosition(1, startingPos);
    }

    private IEnumerator HitIndicator() {
        Debug.Log("In Coroutine");
        hit = true;
        yield return new WaitForSeconds(.1f);
        hit = false;
        Debug.Log("After Yield");
    }
}
