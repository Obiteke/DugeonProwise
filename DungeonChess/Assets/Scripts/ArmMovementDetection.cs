using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NuitrackSDK;
using NuitrackSDK.Tutorials.FirstProject;

public class ArmMovementDetection : MonoBehaviour
{
    public GameObject SkeletonScript = null;
    public UserData.SkeletonData.Joint joint;
    private Vector3 oldJP = Vector3.zero;
        
    // Start is called before the first frame update
    void Start()
    {
        //joint = NuitrackManager.Users.Current.Skeleton.GetJoint(GetComponent<NativeAvatar>().typeJoint[9]);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 JPosition = NuitrackManager.Users.Current.Skeleton.GetJoint(SkeletonScript.GetComponent<NativeAvatar>().typeJoint[8]).Position;
        float speed = Vector3.Distance(JPosition, oldJP)/Time.deltaTime;
        oldJP = JPosition;


        Debug.Log(speed);
    }
}
