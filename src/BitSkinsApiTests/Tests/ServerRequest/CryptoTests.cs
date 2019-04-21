using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitSkinsApi.Crypto;

namespace BitSkinsApiTests.ServerRequest
{
    [TestClass]
    public class CryptoTests
    {
        [TestMethod]
        public void GetBitcoinDepositAddressTest()
        {
            AccountBitcoinDepositAddress.GetBitcoinDepositAddress();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetBitcoinDepositRateTest()
        {
            BitcoinExchangeRate.GetBitcoinDepositRate();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void CreateBitcoinDepositTest()
        {
            BitcoinDepositRate bitcoinDepositRate = BitcoinExchangeRate.GetBitcoinDepositRate();
            double usd = Math.Round(bitcoinDepositRate.PricePerBitcoinInUsd * 0.001, 2);
            CreatingBitcoinDeposit.CreateBitcoinDeposit(usd);

            Assert.IsTrue(true);
        }
    }
}
