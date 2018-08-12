using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK;

public class CameraController : MonoBehaviour
{
    public new Camera camera
    {
        get { return this._camera.Get(this); }
    }
    private LazyLoadedComponentRef<Camera> _camera = new LazyLoadedComponentRef<Camera>();

    [Header("Config")]
    public float orthoSizeScrollSpeed = 10f;
    public float minMaxPlaneSize = 20;
    public float minOrthoSize = 3;
    public float maxOrthoSize = 10f;
    public float movementSpeed = 10f;

    [Header("Debug")]
    [SerializeField]
    private Vector3 min;
    [SerializeField]
    private Vector3 minXDir;
    [SerializeField]
    private Vector3 minYDir;
    [SerializeField]
    private Vector2 location;
    [SerializeField]
    private float orthoSize = 1f;


    public void Start()
    {
        this.min = this.transform.position + (this.transform.right * -this.minMaxPlaneSize) + (this.transform.up * this.minMaxPlaneSize);

        this.minXDir = this.transform.right * this.minMaxPlaneSize*2f;
        this.minYDir = this.transform.up * -this.minMaxPlaneSize*2f;
        this.location = new Vector2(.5f, .5f);
    }

    public void Update()
    {
        float vertical = -Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        this.location += new Vector2(horizontal * Time.deltaTime * this.movementSpeed, vertical * Time.deltaTime * this.movementSpeed);
        this.location.x = Mathf.Clamp01(this.location.x);
        this.location.y = Mathf.Clamp01(this.location.y);

        this.orthoSize += -Input.GetAxisRaw("Mouse ScrollWheel") * this.orthoSizeScrollSpeed * Time.deltaTime;
        this.orthoSize = Mathf.Clamp01(this.orthoSize);

        this.transform.position = this.min + (this.minXDir * this.location.x) + (this.minYDir * this.location.y);
        this.camera.orthographicSize = Mathf.Lerp(this.minOrthoSize, this.maxOrthoSize, this.orthoSize);
    }
}
