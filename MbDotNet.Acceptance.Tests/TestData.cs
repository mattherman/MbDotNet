namespace MbDotNet.Acceptance.Tests
{
    internal class TestData
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public TestData(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}