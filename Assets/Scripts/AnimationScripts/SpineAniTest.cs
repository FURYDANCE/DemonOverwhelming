using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    public class SpineAniTest : MonoBehaviour
    {
        public SkeletonAnimation skAni;
        bool set;
        private void Awake()
        {
            //skAni.skeletonDataAsset = Resources.Load<SkeletonDataAsset>("SpineAnimations/" + "reimu" + "/" + "reimu" + "_SkeletonData");
            ////skAni.GetComponent<MeshRenderer>().material = Resources.Load<Material>("SpineAnimations/" + "reimu" + "/" + "reimu" + "_Material");
            ////skAni.GetComponent<SkeletonRenderer>();

            //skAni.GetComponent<MeshRenderer>().sortingLayerName = "Layer1";
            //skAni.GetComponent<MeshRenderer>().sortingOrder = 0;

            //skAni.state = new Spine.AnimationState(new Spine.AnimationStateData(skAni.skeletonDataAsset.GetSkeletonData(true)));



        }
        // Start is called before the first frame update
        void Start()
        {

            skAni.state.SetAnimation(0, "stand", true);

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
                skAni.state.SetAnimation(0, "attack", false);
            if (Input.GetKeyDown(KeyCode.N))
                skAni.state.SetAnimation(0, "die", false);

            //if (skAni.state == null)
            //{
            //    Debug.Log("kkk");
            //    skAni.state = new Spine.AnimationState(new Spine.AnimationStateData(skAni.skeletonDataAsset.GetSkeletonData(true)));
            //}
            //if (!set)
            //{
            //    skAni.state.SetAnimation(0, "stand", true);
            //    set = true;
            //}
        }
        IEnumerator SkAniInstitate()
        {
            yield return new WaitForSeconds(0.05f);
            skAni.state.SetAnimation(0, "attack", true);

        }
    }
}