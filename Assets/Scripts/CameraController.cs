using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variables
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 6;
    public bool cameraMoving = false;
    int width;
    int height;

    void Start() 
    {
        WorldGenerator worldGenerator = GameObject.Find("Grid/Tilemap").GetComponent<WorldGenerator>();
        width = GameData.width;
        height = GameData.height;

        transform.position = worldGenerator.getCityPosition();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            StartCoroutine(detectCameraMoving());

            Vector3 nextPosition = Camera.main.transform.position + direction;
            if (nextPosition.x >= 0 && nextPosition.x <= width && nextPosition.y >= 0 && nextPosition.y <= height)
                Camera.main.transform.position += direction;
        }

        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }


    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    public IEnumerator detectCameraMoving()
    {
        yield return new WaitForSeconds(0.1f);
        
        if (Input.GetMouseButton(0))
            cameraMoving = true;
        else
            cameraMoving = false;
    }
}
