using System;
using Android.Views;
using Android.Views.Animations;

namespace projektMobilne
{
    class MyAnimation : Animation
    {
        private View mView;
        private int mOrginalHeight;
        private int mTargetHeight;
        private int mGrowBy;

        public MyAnimation(View view, int targetHeight)
        {
            mView = view;
            mOrginalHeight = view.Height;
            mTargetHeight = targetHeight;
            mGrowBy = mTargetHeight - mOrginalHeight;
        }

        protected override void ApplyTransformation(float interpolatedTime, Transformation t)
        {
            mView.LayoutParameters.Height = (int)(mOrginalHeight + (mGrowBy * interpolatedTime));
            mView.RequestLayout();
        }

        public override bool WillChangeBounds()
        {
            return true;
        }
    }
}
