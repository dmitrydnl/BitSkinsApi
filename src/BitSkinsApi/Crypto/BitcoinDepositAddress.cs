/*
 * BitSkinsApi
 * Copyright (C) 2019 dmitrydnl
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using Newtonsoft.Json;

namespace BitSkinsApi.Crypto
{
    /// <summary>
    /// Work with account's Bitcoin deposit address.
    /// </summary>
    public static class AccountBitcoinDepositAddress
    {
        /// <summary>
        /// WARNING!!! BitSkins stop support CryptoAPI! This api call can work not correct!
        /// Allows you to retrieve your account's permanent Bitcoin deposit address. 
        /// Any funds sent to this address are credited to BitSkins at the current conversion rate. 
        /// Conversion rates are locked in when the Bitcoin network broadcasts your transaction.
        /// </summary>
        /// <returns>Account's permanent Bitcoin deposit address.</returns>
        public static BitcoinDepositAddress GetBitcoinDepositAddress()
        {
            string urlRequest = GetUrlRequest();
            string result = Server.ServerRequest.RequestServer(urlRequest);
            BitcoinDepositAddress bitcoinDepositAddress = ReadBitcoinDepositAddress(result);
            return bitcoinDepositAddress;
        }

        private static string GetUrlRequest()
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_permanent_deposit_address/");
            return urlCreator.ReadUrl();
        }

        private static BitcoinDepositAddress ReadBitcoinDepositAddress(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic dataD = responseServerD.data;

            BitcoinDepositAddress bitcoinDepositAddress = null;
            if (dataD != null)
            {
                string address = dataD.address;
                double minimumAcceptableDepositAmount = dataD.minimum_acceptable_deposit_amount;
                
                bitcoinDepositAddress = new BitcoinDepositAddress(address, minimumAcceptableDepositAmount);
            }

            return bitcoinDepositAddress;
        }
    }

    /// <summary>
    /// Account's permanent Bitcoin deposit address.
    /// </summary>
    public class BitcoinDepositAddress
    {
        public string Address { get; private set; }
        public double MinimumAcceptableDepositAmount { get; private set; }

        internal BitcoinDepositAddress(string address, double minimumAcceptableDepositAmount)
        {
            Address = address;
            MinimumAcceptableDepositAmount = minimumAcceptableDepositAmount;
        }
    }
}
