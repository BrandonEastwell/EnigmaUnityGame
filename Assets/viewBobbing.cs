using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(posFollower))]
public class viewBobbing : MonoBehaviour
{
    public float effectIntensity;
    public float effectIntensityX;
    public float effectSpeed;

    private posFollower follower;
    private Vector3 offset;
    private float sinTime;
    void Start()
    {   
        follower = GetComponent<posFollower>();
        offset = follower.offset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));
        if (inputVector.magnitude > 0f)
        {
            sinTime += Time.deltaTime * effectSpeed;
        }
        else
        {
            sinTime = 0f;
        }
        float sinAmountY = -Mathf.Abs(effectIntensity * Mathf.Sin(sinTime));
        Vector3 sinAmountX = follower.transform.right * effectIntensity * Mathf.Cos(sinTime) * effectIntensityX;

        follower.offset = new Vector3
        {
            x = -sinAmountX.x,
            y = offset.y + sinAmountY,
            z = offset.z
        };

        follower.offset += sinAmountX;
    }
}
