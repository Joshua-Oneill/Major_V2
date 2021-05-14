using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator
{
   
    public static void CreateCube(GameObject gameObject, Vector3 offset, Material textureAtlas, Vector2 textureOffset, Sides neighbours)
    {
        //https://docs.unity3d.com/Manual/Example-CreatingaBillboardPlane.html
        //The purpose of this Script is to simply create on Cube with individual faces by defining its Vertices Tringles and then the texture
        //or UV for each face. it Also adds a Collider to each face so the player can walk over and interact with each face and cube.
        //this script requires the most parameters passing in motly all of the information it needs rather then storing and assigning the data to variables within the scripta nd through the unity inspector

        Mesh frontMesh = new Mesh();
        Mesh topMesh = new Mesh();
        Mesh bottomMesh = new Mesh();
        Mesh backMesh = new Mesh();
        Mesh leftMesh = new Mesh();
        Mesh rightMesh = new Mesh();

        Vector3[] vertices = new Vector3[8] //initialize a vector array for all the vertices needed to create a cube an offset is also added so that we can spawn multiple cubes all just adjacent to the origianl by using the x,y,z offset
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

        Vector3[] normals = new Vector3[] //creates lines inbetween the vertices?
        {   -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
        };

        //these two functions return a Vector2 array that will corrospond to a certain location on the texture atlas that is being passed into the main script
        //this calcUV function has a x,y offset passed into it which is then modified by the decimal numbers to move the sample location of the texture atlas.
        //since the atlas is a 16 by 16 cube we access each spot by multiplying the wanted offset x or y by 1/16 then we can get acces to say offset 3 and 2 whihc is 2/16 3/16. 
        //this coordinate is then sampled from the texture atlas and projected or wrappped onto the material of the cube face. 
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

        //this entire cluster of if statements are used to determine if a face is supposed to spawn or not depending if there will be a face next to it, or a neighbour.
        //this neighbour check is done elsewhere whihc passes in a boolean value for each of the 6 faces. this will tell the if statment whether to instatiate the corrosponding face or not. 
        if (!neighbours.front)
        {
            GameObject frontFace = new GameObject("frontFace");   //create a gameobject for the face and give it a name 
            frontFace.transform.SetParent(gameObject.transform);  //sets the transform of the face to be under a combined empty object so the unity hierachy looks cleaner and adds a universal transform for all combined faces
            MeshFilter frontMeshFilter = frontFace.AddComponent<MeshFilter>();   
            MeshRenderer frontMeshRenderer = frontFace.AddComponent<MeshRenderer>();

            

            frontMeshRenderer.material = textureAtlas; //adds the texture atlas to the face
            frontMesh.vertices = new Vector3[] { vertices[0], vertices[1], vertices[2], vertices[3] }; //uses the correct corresponding vertices to creat this front face 
            frontMesh.triangles = new int[] { 0, 2, 1, 2, 3, 1 }; //again calls the corropsonding triangles to form of the triangles for the face 
            frontMesh.normals = normals; //assigns the normals array so the lines appear 
            frontMesh.uv = uv1; //this fave takes the uv1 coordinates to correctly show the texture from the atlas onto the face with the requested offset
            frontMeshFilter.mesh = frontMesh; //aassigning mesh filter 

            //this section adds a collider and conforms the mesh to the face so the player can walk on and interact with the face
            MeshCollider frontCollider = frontFace.AddComponent(typeof(MeshCollider)) as MeshCollider;
            frontCollider.sharedMesh = null;
            frontCollider.sharedMesh = frontMesh;
            frontCollider.convex = true;

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

            MeshCollider backCollider = backFace.AddComponent(typeof(MeshCollider)) as MeshCollider;
            backCollider.sharedMesh = null;
            backCollider.sharedMesh = backMesh;
            backCollider.convex = true;


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

            MeshCollider leftCollider = leftFace.AddComponent(typeof(MeshCollider)) as MeshCollider;
            leftCollider.sharedMesh = null;
            leftCollider.sharedMesh = leftMesh;
            leftCollider.convex = true;
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

            MeshCollider rightCollider = rightFace.AddComponent(typeof(MeshCollider)) as MeshCollider;
            rightCollider.sharedMesh = null;
            rightCollider.sharedMesh = rightMesh;
            rightCollider.convex = true;
        }
    }

}
