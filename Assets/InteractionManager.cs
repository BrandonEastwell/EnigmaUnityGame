using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    //[SerializeField] private Material highlightMat;
    public Text ObjectID;
    private Camera mainCamera;
    public GameObject flashlight;
    private int layerMask;
    private void Start()
    {
        mainCamera = Camera.main;
        layerMask = 1 << 3;
        flashlight = GetComponent<>();
    }
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.yellow);
            GameObject hitObj = hit.transform.gameObject;
            Debug.Log("distance from object " + hit.distance);
            if (hit.distance < 3)
            {
                Vector3 pos = getScreenSpaceTransform(hitObj.transform.position);
                ObjectID.text = hitObj.tag;
                //ObjectID.transform.position = pos;
                if (Input.GetKey(KeyCode.E)) {
                    flashlight.
                    hitObj.gameObject.SetActive(false);
                }
            }
        } else
        {
            Debug.DrawLine(ray.origin, hit.point, Color.yellow);
            ObjectID.text = "";
        }
    }
    Vector3 getScreenSpaceTransform(Vector3 pos)
    {
        Vector3 screenPos = mainCamera.ScreenToWorldPoint(pos);
        Debug.Log("screen pos: " + screenPos);
        return screenPos;
    }
}
