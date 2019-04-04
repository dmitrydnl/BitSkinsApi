using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitSkinsApiTests
{
    [TestClass]
    public class Initilize
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            BitSkinsApi.Account.AccountData.SetupAccount("Api Key", "Secret Code");
        }
    }
}
