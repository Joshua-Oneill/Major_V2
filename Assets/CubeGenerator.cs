using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator
{
   
    public static void CreateCube(GameObject gameObject, Vector3 offset, Material textureAtlas, Vector2 textureOffset, Sides neighbours)
    {
        //https://docs.unity3d.com/Manual/Example-CreatingaBillboardPlane.html

        Mesh frontMesh = new Mesh();
        Mesh topMesh = new Mesh();
        Mesh bottomMesh = new Mesh();
        Mesh backMesh = new Mesh();
        Mesh leftMesh = new Mesh();
        Mesh rightMesh = new Mesh();

        Vector3[] vertices = new Vector3[8] //initialize a vector array
        {
            new Vector3(0, 0, 0) + offset, //0
            new Vector3(1, 0, 0) + offset, //1
            new Vector3(0, 1, 0) + offset, //2
            new Vector3(1, 1, 0) + offset, //3
            new Vector3(1, 0, 1) + offset, //4
            new Vector3(1, 1, 1) + offset, //5
            new Vector3(0, 0, 1) + offset, //6
            new Vector3(0, 1, 1) + offset, //7
        };

        Vector3[] normals = new Vector3[]
        {   -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
        };

        Vector2[] calcUVs(float x, float y)
        {
            float offsetX = 0.0625f * x;
            float offsetY = 0.0625f * y;
  
            return new Vector2[4] 
            {   
                new Vector2(offsetX - 0.0625f, offsetY - 0.0625f),  //left bottom corner           
                new Vector2(offsetX, offsetY - 0.0625f),            //right bottom corner
                new Vector2(offsetX, offsetY),                      //right top corner              
                new Vector2(offsetX - 0.0625f, offsetY)             //left top corner 
            
            };
        }

        Vector2[] calcSideUVs(float x, float y)
        {
            float offsetX = 0.0625f * x;
            float offsetY = 0.0625f * y;

            return new Vector2[4]
            {
                new Vector2(offsetX - 0.0625f, offsetY - 0.0625f), //left bottom corner 
                new Vector2(offsetX - 0.0625f, offsetY),       //left top corner 
                new Vector2(offsetX, offsetY - 0.0625f), //right bottom corner
                new Vector2(offsetX, offsetY)       //right top corner 
            };

        }

        

        Vector2[] uv = calcUVs(textureOffset.x, textureOffset.y); //the paramteres here corrospond to the row and column on the texture atlas, example dirt would be row 3 column 16

        Vector2[] uv1 = calcSideUVs(textureOffset.x, textureOffset.y);

        if (!neighbours.front)
        {
            GameObject frontFace = new GameObject("frontFace");
            frontFace.transform.SetParent(gameObject.transform);
            MeshFilter frontMeshFilter = frontFace.AddComponent<MeshFilter>();
            MeshRenderer frontMeshRenderer = frontFace.AddComponent<MeshRenderer>();

            frontFace.AddComponent<MeshCollider>();

            frontMeshRenderer.material = textureAtlas;
            frontMesh.vertices = new Vector3[] { vertices[0], vertices[1], vertices[2], vertices[3] };
            frontMesh.triangles = new int[] { 0, 2, 1, 2, 3, 1 };
            frontMesh.normals = normals;
            frontMesh.uv = uv1;
            frontMeshFilter.mesh = frontMesh;


        }
        if (!neighbours.top)
        {
            GameObject topFace = new GameObject("topFace");
            topFace.transform.SetParent(gameObject.transform);
            MeshFilter topMeshFilter = topFace.AddComponent<MeshFilter>();
            MeshRenderer topMeshRenderer = topFace.AddComponent<MeshRenderer>();
            
            
            topMeshRenderer.material = textureAtlas;
            topMesh.vertices = new Vector3[] { vertices[2], vertices[3], vertices[5], vertices[7] };
            topMesh.triangles = new int[] { 3, 2, 1, 0, 3, 1 };
            topMesh.normals = normals;
            topMesh.uv = uv;
            topMeshFilter.mesh = topMesh;

            MeshCollider topCollider = topFace.AddComponent(typeof(MeshCollider)) as MeshCollider;
            topCollider.sharedMesh = null;
            topCollider.sharedMesh = topMesh;
            topCollider.convex = true;

        }
        if (!neighbours.bottom) 
        {
            GameObject bottomFace = new GameObject("bottomFace");
            bottomFace.transform.SetParent(gameObject.transform);
            MeshFilter bottomMeshFilter = bottomFace.AddComponent<MeshFilter>();
            MeshRenderer bottomMeshRenderer = bottomFace.AddComponent<MeshRenderer>();
            bottomMeshRenderer.material = textureAtlas;
            bottomMesh.vertices = new Vector3[] { vertices[0], vertices[1], vertices[4], vertices[6] };
            bottomMesh.triangles = new int[] { 3, 0, 2, 0, 1, 2 };
            bottomMesh.normals = normals;
            bottomMesh.uv = uv;
            bottomMeshFilter.mesh = bottomMesh;
        }
        if (!neighbours.back)
        {
            GameObject backFace = new GameObject("backFace");
            backFace.transform.SetParent(gameObject.transform);
            MeshFilter backMeshFilter = backFace.AddComponent<MeshFilter>();
            MeshRenderer backMeshRenderer = backFace.AddComponent<MeshRenderer>();
            backMeshRenderer.material = textureAtlas;
            backMesh.vertices = new Vector3[] { vertices[4], vertices[5], vertices[6], vertices[7] };
            backMesh.triangles = new int[] { 0, 3, 2, 0, 1, 3 };
            backMesh.normals = normals;
            backMesh.uv = uv1;
            backMeshFilter.mesh = backMesh;
        }
        if (!neighbours.left)
        {
            GameObject leftFace = new GameObject("leftFace");
            leftFace.transform.SetParent(gameObject.transform);
            MeshFilter leftMeshFilter = leftFace.AddComponent<MeshFilter>();
            MeshRenderer leftMeshRenderer = leftFace.AddComponent<MeshRenderer>();
            leftMeshRenderer.material = textureAtlas;
            leftMesh.vertices = new Vector3[] { vertices[0], vertices[2], vertices[6], vertices[7] };
            leftMesh.triangles = new int[] { 0, 2, 3, 3, 1, 0 };
            leftMesh.normals = normals;
            leftMesh.uv = uv1;
            leftMeshFilter.mesh = leftMesh;
        }
        if (!neighbours.right)
        {
            GameObject rightFace = new GameObject("rightFace");
            rightFace.transform.SetParent(gameObject.transform);
            MeshFilter rightMeshFilter = rightFace.AddComponent<MeshFilter>();
            MeshRenderer rightMeshRenderer = rightFace.AddComponent<MeshRenderer>();
            rightMeshRenderer.material = textureAtlas;
            rightMesh.vertices = new Vector3[] { vertices[1], vertices[3], vertices[4], vertices[5] };
            rightMesh.triangles = new int[] { 1, 2, 0, 1, 3, 2 };
            rightMesh.normals = normals;
            rightMesh.uv = uv1;
            rightMeshFilter.mesh = rightMesh;
        }
    }

}
