﻿// Set an off-center projection, where perspective's vanishing
// point is not necessarily in the center of the screen.
//
// left/right/top/bottom define near plane size, i.e.
// how offset are corners of camera's near plane.
// Tweak the values and you can see camera's frustum change.

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraStereo : MonoBehaviour
{
    public float scaleFactor;
    public float dimW, dimH, depth, ipdM;
    public float H, W, D, IPD, s;
    public float left = -0.2F;
    public float right = 0.2F;
    public float top = 0.2F;
    public float bottom = -0.2F;

    public bool leftCam;

    void Start() 
    {
        Calculate();
    }

    void LateUpdate()
    {
        Camera cam = GetComponent<Camera>();
        s = cam.nearClipPlane / D;

        if (leftCam) 
        {
            ComputeLeftChanges();
        }
        else { ComputeRightChanges(); }

        Matrix4x4 m = PerspectiveOffCenter(left, right, bottom, top, cam.nearClipPlane, cam.farClipPlane);
        cam.projectionMatrix = m;
    }
    static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
    {
        float x = 2.0F * near / (right - left);
        float y = 2.0F * near / (top - bottom);
        float a = (right + left) / (right - left);
        float b = (top + bottom) / (top - bottom);
        float c = -(far + near) / (far - near);
        float d = -(2.0F * far * near) / (far - near);
        float e = -1.0F;
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = x;
        m[0, 1] = 0;
        m[0, 2] = a;
        m[0, 3] = 0;
        m[1, 0] = 0;
        m[1, 1] = y;
        m[1, 2] = b;
        m[1, 3] = 0;
        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = c;
        m[2, 3] = d;
        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = e;
        m[3, 3] = 0;
        return m;
    }

    void ComputeLeftChanges() 
    {
        this.left = -s * (W - IPD)/2;
        this.right = s * (W + IPD)/2;
        this.bottom = -s * H/2;
        this.top = s * H/2;
    }

    void ComputeRightChanges() 
    {
        this.left = -s * (W + IPD) / 2;
        this.right = s * (W - IPD) / 2;
        this.bottom = -s * H / 2;
        this.top = s * H / 2;
    }

    void Calculate() 
    {
        this.H = this.scaleFactor * this.dimH;
        this.W = this.scaleFactor * this.dimW;
        this.D = this.scaleFactor * this.depth;
        this.IPD = this.scaleFactor * this.ipdM;
        if (leftCam)
        {
            this.transform.localPosition = new Vector3(-this.IPD / 2, 0, 0);
        }
        else { this.transform.localPosition = new Vector3(this.IPD / 2, 0, 0); }
    }
}
