using UnityEngine;

public class FollowPlayerEyes : MonoBehaviour
{
    public Transform target;

    private void Update() {
       if(target != null)
       {
            // var lookPos = target.position - transform.position;
            // lookPos.y = 0;
            // Debug.Log(lookPos);
            // var rotation = Quaternion.LookRotation(lookPos);
            // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
            // transform.rotation = Quaternion.LookRotation(target.position - transform.position) * Quaternion.Euler(180, 0, 0) * Quaternion.AngleAxis(45f, Vector3.up);
            
            Vector3 lookAtRotation = Quaternion.LookRotation(target.position - transform.position).eulerAngles;

            lookAtRotation.x += 180;
            
            transform.rotation = Quaternion.Euler(Vector3.Scale(lookAtRotation, new Vector3(1, 0, 1)));
            // Vector3 targetPosition = target.position;
            // transform.LookAt(target);
            // transform.Rotate(90, 0, 0);
       }
    }
}
