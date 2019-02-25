using UIKit;

namespace ShopPrep.UIClassic.iOS
{
    internal class TransitioningDelegate
    {
        public class TransitioningDelegate : UIViewControllerTransitioningDelegate
        {
            CustomTransitionAnimator animator;

            public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForPresentedController(UIViewController presented, UIViewController presenting, UIViewController source)
            {
                animator = new CustomTransitionAnimator();
                return animator;
            }
        }
    }
}