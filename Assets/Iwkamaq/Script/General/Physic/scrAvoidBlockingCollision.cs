using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrAvoidBlockingCollision : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        //tentative pour éviter que le joueur soit bloqué dans un mur 
        //if (objectBody.velocity == Vector3.zero && Input.GetKey(KeyCode.Return))

        Vector3 objectCenter = Vector3.zero;
        Vector3 collisionCenter = Vector3.zero;

        #region l'objet de base a un boxCollider
        if (GetComponent<BoxCollider>() != null)
        {
            
            objectCenter = new Vector3(transform.position.x + transform.localScale.x * GetComponent<BoxCollider>().center.x, 0, transform.position.z + transform.localScale.y * GetComponent<BoxCollider>().center.y);

            # region l'objet de collision a un box collider
            if (collision.gameObject.GetComponent<BoxCollider>() != null)
            {
                collisionCenter = new Vector3(collision.transform.position.x + collision.transform.localScale.x * collision.gameObject.GetComponent<BoxCollider>().center.x, 0, collision.transform.position.z + collision.transform.localScale.y * collision.gameObject.GetComponent<BoxCollider>().center.y);
                Debug.Log(collisionCenter);

                if ((Mathf.Abs(objectCenter.x - collisionCenter.x) < Mathf.Abs((transform.localScale.x * GetComponent<BoxCollider>().size.x / 2) + (collision.transform.localScale.x * collision.gameObject.GetComponent<BoxCollider>().size.x / 2) - 0.1f))
                    && (Mathf.Abs(objectCenter.z - collisionCenter.z) < Mathf.Abs((transform.localScale.y * GetComponent<BoxCollider>().size.y / 2) + (collision.transform.localScale.y * collision.gameObject.GetComponent<BoxCollider>().size.y / 2) - 0.1f))
                    )
                {
                    transform.Translate(new Vector3(objectCenter.x - collisionCenter.x, 0, objectCenter.z - collisionCenter.z).normalized, Space.World);
                }
            }
            #endregion
            else
            {
                # region l'objet de collision a un capsule collider
                if (collision.gameObject.GetComponent<CapsuleCollider>() != null)
                {
                    collisionCenter = new Vector3(collision.transform.position.x + collision.transform.localScale.x * collision.gameObject.GetComponent<CapsuleCollider>().center.x, 0, collision.transform.position.z + collision.transform.localScale.y * collision.gameObject.GetComponent<CapsuleCollider>().center.y);
                    Debug.Log(collisionCenter);

                    Vector3 capsuleCenterCollider = new Vector3(Mathf.Clamp(collision.gameObject.GetComponent<CapsuleCollider>().height - collision.gameObject.GetComponent<CapsuleCollider>().radius * 2, 0, Mathf.Infinity), 0.2f, collision.gameObject.GetComponent<CapsuleCollider>().radius * 2);

                    if (collision.gameObject.GetComponent<CapsuleCollider>().direction == 0)
                    {
                        capsuleCenterCollider = new Vector3(capsuleCenterCollider.x, capsuleCenterCollider.z, capsuleCenterCollider.y);
                    }
                    else
                    {
                        capsuleCenterCollider = new Vector3(capsuleCenterCollider.z, capsuleCenterCollider.x, capsuleCenterCollider.y);
                    }


                    if ((Mathf.Abs(objectCenter.x - collisionCenter.x) < Mathf.Abs((transform.localScale.x * capsuleCenterCollider.x / 2) + (collision.transform.localScale.x * capsuleCenterCollider.x / 2) - 0.1f))
                        && (Mathf.Abs(objectCenter.z - collisionCenter.z) < Mathf.Abs((transform.localScale.y * capsuleCenterCollider.y / 2) + (collision.transform.localScale.y * capsuleCenterCollider.y / 2) - 0.1f))
                        )
                    {
                        transform.Translate(new Vector3(objectCenter.x - collisionCenter.x, 0, objectCenter.z - collisionCenter.z).normalized, Space.World);
                    }
                    
                }
                #endregion
            }
        }
        #endregion
    }
}
