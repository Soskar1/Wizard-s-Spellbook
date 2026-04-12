using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace WizardsSpellbook.UnityAnimationScripting
{
    public class AnimationPlayer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;

        private int _currentAnimationState;

        public void PlayAnimation(int stateHash)
        {
            _currentAnimationState = stateHash;

            // Note: Here I am using Play instead of CrossFade to ensure the animation starts from the beginning immediately.
            // With CrossFade, there could be a slight blending delay.
            Animator.Play(_currentAnimationState, 0, 0);

            // Note: Force the animator to update immediately to apply the new state
            Animator.Update(0);
        }
        
        public async Task WaitForStateToFinish(int stateHash, CancellationToken cancellationToken = default)
        {
            await WaitUntilState(stateHash, cancellationToken);
            await WaitUntilFinish(stateHash, cancellationToken);
        }

        private async Task WaitUntilState(int stateHash, CancellationToken cancellationToken = default)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var info = Animator.GetCurrentAnimatorStateInfo(0);
                if (info.fullPathHash == stateHash)
                {
                    break;
                } 

                await Task.Yield();
            }
        }

        private async Task WaitUntilFinish(int stateHash, CancellationToken cancellationToken = default)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (!Animator.IsInTransition(0))
                {
                    var info = Animator.GetCurrentAnimatorStateInfo(0);
                    if (info.fullPathHash == stateHash && info.normalizedTime >= 1f)
                    {
                        return;
                    }
                }

                await Task.Yield();
            }
        }
    }
}