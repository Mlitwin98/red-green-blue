using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerControl : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20;
    [SerializeField] Transform target = default;

    [SerializeField] GameObject[] UIToHide = default;

    Vector3 startingPos;

    bool cameraIsMoving;

    void Start() 
    {
        foreach(var ui in UIToHide) ui.SetActive(false);
        cameraIsMoving = true;
        startingPos = transform.position;
        if (target != null)
        {
            StartCoroutine(MoveToTarget());
        }
    }

    Transform targetBtn;

    private void Update()
    {
        if (targetBtn != null)
        {
            cameraIsMoving = true;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetBtn.position.x, targetBtn.position.y, transform.position.z), moveSpeed * Time.deltaTime);
            foreach(var ui in UIToHide) ui.SetActive(false);
        }
        if( targetBtn != null && targetBtn.position.x == transform.position.x)
        {
            targetBtn = null;
            cameraIsMoving = false;
            foreach(var ui in UIToHide) ui.SetActive(true);
        }
        
    }

    public void ChangeTarget(Transform newTarget){
        if(!cameraIsMoving) targetBtn = newTarget;
    }

    IEnumerator MoveToTarget()
    {
        yield return new WaitForSeconds(1.5f);
        while (transform.position.x != target.position.x)
        {            
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(.5f);
        StartCoroutine(MoveBack());
    }

    IEnumerator MoveBack()
    {
        while (transform.position != startingPos)
        {            
            transform.position = Vector3.MoveTowards(transform.position, startingPos, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        cameraIsMoving = false;
        foreach(var ui in UIToHide) ui.SetActive(true);
    }

    public bool GetCameraIsMoving()
    {
        return cameraIsMoving;
    }
}
