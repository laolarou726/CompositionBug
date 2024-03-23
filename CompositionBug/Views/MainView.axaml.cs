using System;
using System.Numerics;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Rendering.Composition;

namespace CompositionBug.Views;

public partial class MainView : UserControl
{
    private bool _flag;

    public MainView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        BeginAnimation(false);
    }

    private void BeginAnimation(bool flag)
    {
        var easing = new SplineEasing(0.1, 0.9, 0.2);

        var from = flag ? new Vector3(-500, 225, 0) : new Vector3(150, 225, 0);
        var to = flag ? new Vector3(150, 225, 0) : new Vector3(-500, 225, 0);

        var visual = ElementComposition.GetElementVisual(Rect)!;
        var compositor = visual.Compositor;
        var ani = compositor.CreateVector3KeyFrameAnimation();

        ani.InsertKeyFrame(0f, from, easing);
        ani.InsertKeyFrame(1f, to, easing);

        ani.Duration = TimeSpan.FromMilliseconds(445);

        visual.StartAnimation("Offset", ani);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        _flag = !_flag;
        BeginAnimation(_flag);
    }
}
