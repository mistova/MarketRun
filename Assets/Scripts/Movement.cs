using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float forwardSpeed, sideSpeed;
    [SerializeField] Transform[] wheels;

    [SerializeField] Vector2 limit;
    [SerializeField] MeshFilter rootMesh;
    Vector3 meshSize;

    bool isMove, isFinished, isInCollidingArea;
    Vector3 goingPlace;

    [SerializeField] float collidingSpeedMultiplier = 0.3f;
    [SerializeField] float collidingTime = 0.3f;
    [SerializeField] float speedMultiplierInFrontObstacle = 0.7f;

    float screenWidth;
    float clickedPositionX;

    [SerializeField] Animator anim;

    private void Start()
    {
        screenWidth = Screen.width;
        meshSize = rootMesh.mesh.bounds.size;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 displacement = Vector3.forward * forwardSpeed;

        if (isFinished)
        {
            if (transform.position.x > -1.5f || transform.position.x < -2.5f)
            {
                displacement.x -= new Vector3(transform.position.x + 2, 0, 0).normalized.x;
                displacement.x *= sideSpeed * 0.1f;
            }

            displacement.y = Mathf.Sin(Mathf.PI / 180) * displacement.z;
        }
        else if (isInCollidingArea)
        {
            displacement = goingPlace.normalized * forwardSpeed;
            displacement *= speedMultiplierInFrontObstacle;
        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                clickedPositionX = Input.mousePosition.x;
                isMove = true;
                anim.SetBool("isPushing", true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isMove = false;
                anim.SetBool("isPushing", false);
            }
            else if (Input.GetMouseButton(0))
            {
                float moveScale = (Input.mousePosition.x - clickedPositionX) / screenWidth;
                displacement.x += sideSpeed * moveScale;

                float xSize = Vector3.Scale(transform.localScale, meshSize).x;

                if (displacement.x > 0 && transform.position.x + xSize / 2 > limit.y)
                {
                    displacement.x = 0;
                    clickedPositionX = Input.mousePosition.x;
                }
                else if (displacement.x < 0 && transform.position.x - xSize / 2 < limit.x)
                {
                    displacement.x = 0;
                    clickedPositionX = Input.mousePosition.x;
                }
            }

            if (!isMove)
            {
                displacement = Vector3.zero;
            }
        }

        TurnWheels(Mathf.Atan2(displacement.x, displacement.z) * Mathf.Rad2Deg);

        UpdateSlider();

        transform.position += displacement * Time.deltaTime;
    }

    private void TurnWheels(float rot)
    {
        foreach (Transform t in wheels)
            t.localRotation = Quaternion.Euler(0, 0, rot);
    }

    internal void GoFinishLine()
    {
        isFinished = true;
        anim.SetBool("isPushing", true);

        //StartCoroutine(ConfigurePlaneAsync());
    }

    internal void InsideOfCollidingArea(Vector3 collidedObject)
    {
        goingPlace = collidedObject;
        goingPlace.y = 0;
        isInCollidingArea = true;
        anim.SetBool("isPushing", true);
    }

    internal void OutsideOfCollidingArea()
    {
        StartCoroutine(CollidingAndOutAsync());
    }

    void UpdateSlider()
    {
        //UIController.Instance.SetSliderValue(transform.position.z);
        //UIController.Instance.SetSurferCountPosition(transform.position, transform.lossyScale.y);
    }

    IEnumerator CollidingAndOutAsync()
    {
        forwardSpeed *= collidingSpeedMultiplier;
        yield return new WaitForSeconds(collidingTime);
        forwardSpeed /= collidingSpeedMultiplier;
        isInCollidingArea = false;
        anim.SetBool("isPushing", false);
    }

    /*IEnumerator ConfigurePlaneAsync()
    {
        Vector3 rot = plane.rotation.eulerAngles;
        for (float i = 0.05f; i <= 10; i += 0.05f)
        {
            plane.rotation = Quaternion.Euler(rot + Vector3.right * i);
            yield return new WaitForSeconds(0.01f);
        }
    }*/
}
