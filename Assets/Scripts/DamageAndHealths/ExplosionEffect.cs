using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class ExplosionEffect : MonoBehaviour
{
    private PlayableGraph _graph;

    public void StartExplosion(AnimationClip _explosionAnimation, Color color)
    {
        StartCoroutine(DestroyAfterAnimation(_explosionAnimation));

        GetComponent<SpriteRenderer>().color = color;

        var animator = GetComponent<Animator>();
        _graph = PlayableGraph.Create();
        var playableOutput = AnimationPlayableOutput.Create(_graph, "Animation", animator);
        var clipPlayable = AnimationClipPlayable.Create(_graph, _explosionAnimation);
        playableOutput.SetSourcePlayable(clipPlayable);
        _graph.Play();
    }

    private IEnumerator DestroyAfterAnimation(AnimationClip _explosionAnimation)
    {
        yield return new WaitForSeconds(_explosionAnimation.length - 0.05f);
        _graph.Destroy();
        Destroy(gameObject);
    }
}
