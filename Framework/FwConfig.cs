namespace Framework
{
    public class FwConfig
    {
        public DriverSettings Driver { get; set; }

        public TestSettings Test { get; set; }
    }

    public class DriverSettings
    {
        public string Browser { get; set; }

        public int WaitSeconds { get; set; }
    }

    public class TestSettings
    {
        public string Url { get; set; }
    }
}
