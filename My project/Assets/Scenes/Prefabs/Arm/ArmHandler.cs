using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;




public class ArmHandler : MonoBehaviour
{
    [SerializeField] Transform startTransform;
    [SerializeField] Transform endTransform;
    Transform currentTransform;

    public bool endReached = false;
    private bool transition = false;
    private bool start = true;

    //ArmHandler armHandler;
    //HandBottle handBottle;

    private bool swing;
    private bool swingEnd = true;
    private bool swingStart = false;



    private void Start()
    {
        currentTransform = endTransform;
    }

    //private void Update()
    //{
    //    HandleSwingMovement();
    //}


    public void Reinitialize()
    {
        swing = true;
        swingEnd = true;
        swingStart = false;
    }

    public void HandleSwingMovement()
    {
        if (swing)
        {
            if (swingEnd)
            {
                swingEnd = SwingArmEnd();
            }
            else
            {
                swingStart = true;
            }
            if (swingStart)
            {
                swingStart = SwingArmStart();

                if (!swingStart)
                {
                    swing = false;
                }
            }

        }


    }



    public bool SwingArmEnd()
    {

        transform.rotation = Quaternion.Slerp(transform.rotation, endTransform.rotation, 20.0f * Time.deltaTime);

        float angleBetween = Mathf.Abs(transform.rotation.eulerAngles.y - endTransform.rotation.eulerAngles.y);

        if (angleBetween < 2.0f)
        {
            return false;
        }

        return true;

    }


    public bool SwingArmStart()
    {

        transform.rotation = Quaternion.Slerp(transform.rotation, startTransform.rotation, 20.0f * Time.deltaTime);

        float angleBetween = Mathf.Abs(transform.rotation.eulerAngles.y - startTransform.rotation.eulerAngles.y);

        if (angleBetween  < 2.0f)
        {
            return false;
        }
        return true;
    }

}
