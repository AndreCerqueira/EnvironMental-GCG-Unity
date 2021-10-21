using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    int mapWidth;
    int mapHeight;

    void Start() 
    {
        WorldGenerator worldGenerator = GameObject.Find("Grid/Tilemap").GetComponent<WorldGenerator>();
        mapWidth = worldGenerator.width;
        mapHeight = worldGenerator.height;
    }

    // Update is called once per frame
    void Update()
    {
        // Min x: 0
        // Min y: 0
        // Max x: width
        // Max y: height

        
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

            Vector3 nextPosition = Camera.main.transform.position + direction;
            if (nextPosition.x >= 0 && nextPosition.x <= mapWidth && nextPosition.y >= 0 && nextPosition.y <= mapHeight)
                Camera.main.transform.position += direction;
        }

        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }


    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
