using PathCreation;
using UnityEngine;
using NaughtyAttributes;

public class ProceduralDogBody : MonoBehaviour {
    [Header("Refs")]
    public MeshFilter meshFilter;

    [Header("Params")]
    [MinValue(3)]
    [MaxValue(128)]
    public int detail;
    [MinValue(0)]
    public float radius;

    private Mesh mesh;
    private VertexPath vertexPath;

    private void GenerateMesh () {
        //init mesh
        mesh = new Mesh();
        mesh.Clear();

        //init arrays
        Vector3[] verts = new Vector3[detail * vertexPath.NumPoints];
        int[] tris = new int[(vertexPath.NumPoints - 1) * detail * 2 * 3];
        Vector3[] normals = new Vector3[verts.Length];

        float angleRad = Mathf.PI * 2 / detail;
        Vector3 forward = Vector3.zero;

        #region All Segments Minus Last One
        for ( int i = 0; i < vertexPath.NumPoints - 1; i++ ) {
            Vector3 p = vertexPath.GetPoint( i );
            Vector3 nextP = vertexPath.GetPoint( i + 1 );

            forward = (nextP - p).normalized;

            for ( int j = 0; j < detail; j++ ) {
                Vector3 localOffset = new Vector3( Mathf.Cos( angleRad * j ), Mathf.Sin( angleRad * j ), 0 ) * radius;
                Vector3 pos = p + Quaternion.LookRotation(forward) * localOffset;

                verts[i * detail + j] = pos;
                normals[i * detail + j] = ( pos - p ).normalized;
            }
        }
        #endregion

        #region Last Segment
        Vector3 point = vertexPath.GetPoint( vertexPath.NumPoints - 1 );

        for ( int j = 0; j < detail; j++ ) {
            Vector3 localOffset = new Vector3( Mathf.Cos( angleRad * j ), Mathf.Sin( angleRad * j ), 0 ) * radius;
            Vector3 pos = point + Quaternion.LookRotation(forward) * localOffset;

            verts[( vertexPath.NumPoints - 1 ) * detail + j] = pos;
            normals[( vertexPath.NumPoints - 1 ) * detail + j] = ( pos - point ).normalized;
        }
        #endregion

        #region Tris
        for ( int i = 0; i < vertexPath.NumPoints - 1; i++ ) {
            for ( int j = 0; j < detail; j++ ) {
                int startIndex = i * detail + j;
                if ( j == detail - 1 ) {
                    tris[startIndex * 6] = startIndex;
                    tris[startIndex * 6 + 1] = startIndex - detail + 1;
                    tris[startIndex * 6 + 2] = startIndex + detail;

                    tris[startIndex * 6 + 3] = startIndex + detail;
                    tris[startIndex * 6 + 4] = startIndex - detail + 1;
                    tris[startIndex * 6 + 5] = startIndex + 1;
                }
                else {
                    tris[startIndex * 6] = startIndex;
                    tris[startIndex * 6 + 1] = startIndex + 1;
                    tris[startIndex * 6 + 2] = startIndex + detail;

                    tris[startIndex * 6 + 3] = startIndex + detail;
                    tris[startIndex * 6 + 4] = startIndex + 1;
                    tris[startIndex * 6 + 5] = startIndex + detail + 1;
                }
            }
        }
        #endregion

        //set all
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.normals = normals;

        meshFilter.mesh = mesh;
    }

    public void SetVertexPath(VertexPath vp ) {
        this.vertexPath = vp;
        GenerateMesh();
    }
}