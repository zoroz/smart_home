﻿namespace SmartHomeUniversal.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new SmartHomeUniversal.App());
        }
    }
}
