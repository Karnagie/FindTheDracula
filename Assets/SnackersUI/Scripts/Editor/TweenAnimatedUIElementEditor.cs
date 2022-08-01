using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace SnackersUI.Editor
{
    [CustomEditor(typeof(TweenAnimatedUIElement), true)]
    public class TweenAnimatedUIElementEditor : UnityEditor.Editor
    {


        public override void OnInspectorGUI()
        {

            DrawDefaultInspector();
            EditorGUILayout.Space(10);

            serializedObject.Update();
            var target = (TweenAnimatedUIElement)this.target;
            var anims = target.GetComponents<TweenAnimationBase>();

            var inAnimsProp = serializedObject.FindProperty("_inAnimations");
            var inAnims = new List<TweenAnimationBase>();
            for (int i = 0; i < inAnimsProp.arraySize; i++)
            {
                inAnims.Add(
                    (TweenAnimationBase)inAnimsProp.GetArrayElementAtIndex(i).objectReferenceValue
                );
            }

            var outAnimsProp = serializedObject.FindProperty("_outAnimations");
            var outAnims = new List<TweenAnimationBase>();
            for (int i = 0; i < outAnimsProp.arraySize; i++)
            {
                outAnims.Add(
                    (TweenAnimationBase)outAnimsProp.GetArrayElementAtIndex(i).objectReferenceValue
                );
            }

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.LabelField("In animations", EditorStyles.boldLabel);
            var inAnimCandidates = anims.Except(outAnims).ToList();
            foreach (var anim in inAnimCandidates)
            {
                var descr = anim.GetDescription();
                var enabled = EditorGUILayout.ToggleLeft(descr, inAnims.IndexOf(anim) > -1);
                if (enabled && inAnims.IndexOf(anim) == -1)
                {
                    inAnims.Add(anim);
                }
                else if (!enabled && inAnims.IndexOf(anim) >= 0)
                {
                    inAnims.Remove(anim);
                }
            }


            if (EditorGUI.EndChangeCheck())
            {
                inAnims = inAnims.Where(anim => anim != null).ToList();
                inAnimsProp.arraySize = inAnims.Count;
                for (int i = 0; i < inAnims.Count; i++)
                {
                    var anim = inAnims[i];
                    var p = inAnimsProp.GetArrayElementAtIndex(i);
                    p.objectReferenceValue = anim;
                }
            }



            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Out animations", EditorStyles.boldLabel);
            EditorGUI.BeginChangeCheck();
            var outAnimCandidates = anims.Except(inAnims).ToList();
            foreach (var anim in outAnimCandidates)
            {
                var descr = anim.GetDescription();
                var enabled = EditorGUILayout.ToggleLeft(descr, outAnims.IndexOf(anim) > -1);
                if (enabled && outAnims.IndexOf(anim) == -1)
                {
                    outAnims.Add(anim);
                }
                else if (!enabled && outAnims.IndexOf(anim) >= 0)
                {
                    outAnims.Remove(anim);
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                outAnims = outAnims.Where(anim => anim != null).ToList();
                outAnimsProp.arraySize = outAnims.Count;
                for (int i = 0; i < outAnims.Count; i++)
                {
                    var anim = outAnims[i];
                    var p = outAnimsProp.GetArrayElementAtIndex(i);
                    p.objectReferenceValue = anim;
                }
            }

            serializedObject.ApplyModifiedProperties();
            // inAnimsProp.objectReferenceValue = inAnims;
            // outAnimsProp.objectReferenceValue = outAnims;
        }
    }
}