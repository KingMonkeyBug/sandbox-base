using UnityEngine;

public class PhysicsGun : MonoBehaviour
{
    public float maxDistance = 100f;
    public float force = 100f;
    public string[] tagsToIgnore;

    private Rigidbody grabbedObject;
    private SpringJoint springJoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
            {
                if (hit.rigidbody && !ShouldIgnore(hit.rigidbody.gameObject))
                {
                    grabbedObject = hit.rigidbody;
                    springJoint = grabbedObject.gameObject.AddComponent<SpringJoint>();
                    springJoint.connectedAnchor = transform.position;
                    springJoint.spring = force;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (grabbedObject)
            {
                Destroy(springJoint);
                grabbedObject = null;
            }
        }

        if (grabbedObject)
        {
            grabbedObject.velocity = Vector3.zero;
            grabbedObject.angularVelocity = Vector3.zero;
            grabbedObject.transform.position = Vector3.Lerp(grabbedObject.transform.position, transform.position + transform.forward * 2f, Time.deltaTime * 10f);
        }
    }

    bool ShouldIgnore(GameObject obj)
    {
        foreach (string tag in tagsToIgnore)
        {
            if (obj.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
}
