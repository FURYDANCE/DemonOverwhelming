using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    public class MapDrag : MonoBehaviour
    {
        public float sensivity;
        public bool Draging;
        Vector3 objectStartV3;
        Vector3 startV3;
        Vector3 offset;
        public float maxX;
        float startMaxX;
        public float maxY;
        float startMaxY;

        public float minX;
        float startMinX;

        public float minY;
        float startMinY;


        public float targetScale;
        Vector3 v;
        float startHeight;
        SceneManager_MapScene mapSceneManager;
        void Start()
        {
            objectStartV3 = transform.position;
            startMaxX = maxX;
            startMaxY = maxY;
            startMinX = minX;
            startMinY = minY;
            startHeight = transform.position.z;
            mapSceneManager = SceneManager_MapScene.instance;
        }

        // Update is called once per frame
        void Update()
        {

            //transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), ref v, 0.5f);
            if (mapSceneManager && mapSceneManager.isInformationing)
                return;
            if (Input.GetAxis("Mouse ScrollWheel") != 0) //滚轮缩放
            {
                float S = Input.GetAxis("Mouse ScrollWheel") * 20;

                //改变物体大小 
                if (startHeight + S < -51 || startHeight + S > 20)
                    return;
                //if (targetScale + S > 1.6f || targetScale + S < 0.5f)
                //    return;
                startHeight += S;
                //maxX = startMaxX * targetScale * targetScale;
                //maxY = startMaxY * targetScale * targetScale;
                //minX = startMinX * targetScale * targetScale;
                //minY = startMinY * targetScale * targetScale;

            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Draging = true;
                startV3 = Input.mousePosition;
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                offset = Input.mousePosition - startV3;

                transform.position = objectStartV3 - offset * sensivity;
                if (transform.position.x > maxX)
                    transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
                if (transform.position.x < minX)
                    transform.position = new Vector3(minX, transform.position.y, transform.position.z);
                if (transform.position.y > maxY)
                    transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
                if (transform.position.y < minY)
                    transform.position = new Vector3(transform.position.x, minY, transform.position.z);

            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                objectStartV3 = transform.position;
            }
            RefreshPos();
        }
        public void SetCameraScale(float S)
        {
            startHeight = S;
        }
        public void RefreshPos()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startHeight);

        }
        public void RefreshPos(Vector3 target, Vector2 offset)
        {
            transform.position = new Vector3(target.x + offset.x, target.y + offset.y, startHeight);

        }
    }
}