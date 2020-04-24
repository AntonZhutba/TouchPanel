using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TouchPanel.CustomControls;
using TouchPanel.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderedFrame), typeof(BorderedFrameRenderer))]
namespace TouchPanel.Droid.Renderers
{

    class BorderedFrameRenderer : FrameRenderer
    {
        public BorderedFrameRenderer(Context context) : base(context)
        {

        }
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            var view = (BorderedFrame)Element;
            if (true)
            {
                using (var strokePaint = new Paint())
                using (var rect = new RectF(0, 0, canvas.Width, canvas.Height))
                {
                    strokePaint.SetStyle(Paint.Style.Stroke);
                    strokePaint.Color = Element.BorderColor.ToAndroid();
                    strokePaint.StrokeWidth = 1;
                    canvas.DrawRoundRect(rect, Element.CornerRadius, Element.CornerRadius, strokePaint);
                }
            }
        }
    }
}