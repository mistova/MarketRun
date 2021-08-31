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

    AnimationController anim;

    private void Start()
    {
        screenWidth = Screen.width;
        meshSize = rootMesh.mesh.bounds.size;
        anim = GetComponent<AnimationController>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 displacement = Vector3.forward * forwardSpeed;

        if (Input.GetMouseButtonDown(0))
        {
            clickedPositionX = Input.mousePosition.x;
            isMove = true;
            anim.SetPushing(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMove = false;
            anim.SetPushing(false);
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

        TurnWheels(Mathf.Atan2(displacement.x, displacement.z) * Mathf.Rad2Deg);

        transform.position += displacement * Time.deltaTime;
    }

    private void TurnWheels(float rot)
    {
        foreach (Transform t in wheels)
            t.localRotation = Quaternion.Euler(0, 0, rot);
    }

    internal void Finished()
    {
        anim.SetPushing(false);
        this.enabled = false;
    }
}
