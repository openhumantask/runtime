namespace OpenHumanTask.Runtime.IntegrationTests.Data
{
    public static class FaultFactory
    {

        public static Fault Create()
        {
            return new("fake-fault-name", "fake-fault-description");
        }

    }

}
