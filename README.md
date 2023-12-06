## Host .NET MAUI DataGrid as pullable content

The `PullToRefresh` control provides support for loading any custom control as pullable content. To host the .NET MAUI Datagrid inside the PullToRefresh.

<ol>
<li> Add the required assembly references as discussed in the <a href="https://help.syncfusion.com/maui/datagrid/getting-started">DataGrid</a> and PullToRefresh.<li>
<li> Import PullToRefresh and DataGrid control namespace as follows.</li>
<br/>

    xmlns:sfgrid="clr-namespace:Syncfusion.Maui.DataGrid;assembly=Syncfusion.Maui.DataGrid"
    xmlns:pulltoRefresh="clr-namespace:Syncfusion.Maui.PullToRefresh;assembly=Syncfusion.Maui.PullToRefresh"


    using Syncfusion.Maui.DataGrid;
    using Syncfusion.Maui.PullToRefresh;

<br/>
<li> Define the DataGrid as PullableContent of the PullToRefresh.</li> 
<li> Handle the pull to refresh events for refreshing the data. </li>
<li> Customize the required properties of the DataGrid andPullToRefresh based on your requirement.</li>
</ol>

    <ContentPage.Content>
        <Grid>
            <pulltoRefresh:SfPullToRefresh x:Name="pullToRefresh"
                                        RefreshViewHeight="50"
                                        RefreshViewThreshold="30"
                                        PullingThreshold="150"
                                        RefreshViewWidth="50"
                                        ProgressThickness='{OnPlatform Android="3", Default="2"}'
                                        TransitionMode="SlideOnTop"
                                        Margin="{StaticResource margin}"
                                        IsRefreshing="False">
                <pulltoRefresh:SfPullToRefresh.PullableContent>
                    <sfgrid:SfDataGrid x:Name="dataGrid"
                                    HeaderRowHeight="52"
                                    RowHeight="48"
                                    SortingMode="Single"
                                    ItemsSource="{Binding OrdersInfo}"
                                    AutoGenerateColumnsMode="None"
                                    ColumnWidthMode="Fill"
                                    HorizontalScrollBarVisibility="Always"
                                    VerticalScrollBarVisibility="Always">
                        . . .
                        . . . .

                    </sfgrid:SfDataGrid>
                </pulltoRefresh:SfPullToRefresh.PullableContent>
            </pulltoRefresh:SfPullToRefresh>
        </Grid>
    </ContentPage.Content>

## How to run the sample

1. Clone the sample and open it in Visual Studio.

   N> If you download the sample using the "Download ZIP" option, right-click it, select Properties, and then select Unblock.

2. Register your license key in the App.cs file as demonstrated in the following code.

        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");
            
            InitializeComponent();
            
            MainPage = new AppShell();
        }

    Refer to this [link](https://help.syncfusion.com/common/essential-studio/licensing/overview) for more details.

3. Clean and build the application.
4. Run the application.