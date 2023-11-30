using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.ProgressBar;
using Syncfusion.Maui.PullToRefresh;

namespace PullToRefreshTemplate;

/// <summary>
/// Base generic class for user-defined behaviors that can respond to conditions and events.
/// </summary>
public class PullToRefreshTemplateBehavior : Behavior<ContentPage>
{
    private SfDataGrid? dataGrid;
    private Syncfusion.Maui.PullToRefresh.SfPullToRefresh? pullToRefresh;
    private OrderInfoViewModel? viewModel;
    private SfCircularProgressBar? progressbar;
    private Frame? frame;
    private Label? progressContent;

    /// <summary>
    /// You can override this method to subscribe to AssociatedObject events and initialize properties.
    /// </summary>
    /// <param name="bindable">ContentPage type parameter named as bindable</param>
    protected override void OnAttachedTo(ContentPage bindable)
    {
        this.viewModel = new OrderInfoViewModel();
        this.progressbar = new SfCircularProgressBar();
        this.frame = new Frame();
        this.progressContent = new Label();

        this.progressContent.TextColor = Color.FromRgb(0, 124, 238);
        this.progressContent.FontSize = 9;
        this.progressContent.WidthRequest = 20;
        this.progressContent.HorizontalTextAlignment = TextAlignment.Center;

        this.frame.BorderColor = Colors.LightGray;
        this.frame.BackgroundColor = Colors.White;
        this.frame.CornerRadius = 30;
        this.frame.Content = this.progressbar;
        this.frame.Padding = 0;
        this.frame.HasShadow = false;

        this.progressbar.SegmentCount = 10;
        this.progressbar.ProgressThickness = 6;
        this.progressbar.ProgressRadiusFactor = 0.7;
        this.progressbar.SegmentGapWidth = 1;
        this.progressbar.WidthRequest = 55;
        this.progressbar.HeightRequest = 55;
        this.progressbar.IndeterminateAnimationDuration = 750;
        this.progressbar.Content = this.progressContent;

        bindable.BindingContext = this.viewModel;
        this.pullToRefresh = bindable.FindByName<Syncfusion.Maui.PullToRefresh.SfPullToRefresh>("pullToRefresh");
        this.dataGrid = bindable.FindByName<SfDataGrid>("dataGrid");
        this.dataGrid.ItemsSource = this.viewModel.OrdersInfo;
        this.pullToRefresh.Refreshing += this.PullToRefresh_Refreshing;
        this.pullToRefresh.Pulling += this.PullToRefresh_Pulling;

        var pullingTemplate = new DataTemplate(() =>
         {
             return new ViewCell { View = this.frame };
         });

        this.pullToRefresh.PullingViewTemplate = pullingTemplate;
        this.pullToRefresh.RefreshingViewTemplate = pullingTemplate;

        base.OnAttachedTo(bindable);
    }

    /// <summary>
    /// You can override this method while View was detached from window
    /// </summary>
    /// <param name="bindable">ContentPage type parameter named as bindable</param>
    protected override void OnDetachingFrom(ContentPage bindable)
    {
        this.pullToRefresh!.Refreshing -= this.PullToRefresh_Refreshing;
        this.pullToRefresh = null;
        this.dataGrid = null;
        base.OnDetachingFrom(bindable);
    }

    /// <summary>
    /// Fired when pulling occurs. 
    /// </summary>
    /// <param name="sender">PullToRefresh_Refreshing event sender</param>
    /// <param name="e">PullToRefresh_Refreshing event args</param>
    private void PullToRefresh_Pulling(object? sender, PullingEventArgs e)
    {
        this.progressbar!.TrackThickness = 0.8;
        this.progressbar.TrackRadiusFactor = 0.1;
        this.progressbar.IsIndeterminate = false;
        this.progressbar.ProgressFill = Color.FromRgb(0, 124, 238);
        this.progressbar.TrackFill = Colors.White;

        var absoluteProgress = Convert.ToInt32(Math.Abs(e.Progress));
        this.progressbar.Progress = absoluteProgress;
        this.progressbar.SetProgress(absoluteProgress, 1, Easing.CubicInOut);
        this.progressContent!.Text = e.Progress.ToString();
    }

    /// <summary>
    /// Fired when pullToRefresh View is refreshing
    /// </summary>
    /// <param name="sender">PullToRefresh_Refreshing event sender</param>
    /// <param name="e">PullToRefresh_Refreshing event args</param>
    private async void PullToRefresh_Refreshing(object? sender, EventArgs e)
    {
        this.progressContent!.IsVisible = false;
        this.pullToRefresh!.IsRefreshing = true;
        await Task.Delay(10);
        await this.AnimateRefresh();
        this.viewModel!.RefreshItemsource(10);
        await Task.Delay(10);
        this.pullToRefresh.IsRefreshing = false;
        this.progressContent.IsVisible = true;
    }

    /// <summary>
    /// Increments the <see cref="ProgressBar"/> progress value
    /// </summary>
    /// <returns>Returns the <see cref="Task"/>.</returns>
    private async Task AnimateRefresh()
    {
        this.progressbar!.Progress = 0;
        this.progressbar.IsIndeterminate = true;

        await Task.Delay(750);
        this.progressbar.ProgressFill = Colors.Red;

        await Task.Delay(750);
        this.progressbar.ProgressFill = Colors.Green;

        await Task.Delay(750);
        this.progressbar.ProgressFill = Colors.Orange;

        await Task.Delay(750);
    }

}
