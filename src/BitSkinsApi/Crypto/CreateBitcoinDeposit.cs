/*
 * BitSkinsApi
 * Copyright (C) 2019 Captious99
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

using System;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Crypto
{
    /// <summary>
    /// Creating a payment request for Bitcoin. 
    /// </summary>
    public static class AccountCreateBitcoinDeposit
    {
        /// <summary>
        /// Allows you to generate a payment request for Bitcoin. 
        /// You can deposit any amount of Bitcoin (more than 0.0002 BTC).
        /// </summary>
        /// <param name="amount">Amount to deposit in USD.</param>
        /// <returns>Payment request for Bitcoin. </returns>
        public static CreatedBitcoinDeposit CreateBitcoinDeposit(double amount)
        {
            string urlRequest = GetUrlRequest(amount);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            CreatedBitcoinDeposit createdBitcoinDeposit = ReadCreatedBitcoinDeposit(result);
            return createdBitcoinDeposit;
        }

        private static string GetUrlRequest(double amount)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/create_bitcoin_payment/");
            urlCreator.AppendUrl($"&amount={amount.ToString(System.Globalization.CultureInfo.InvariantCulture)}");

            return urlCreator.ReadUrl();
        }

        private static CreatedBitcoinDeposit ReadCreatedBitcoinDeposit(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic orderD = responseServerD.data.order;

            CreatedBitcoinDeposit createdBitcoinDeposit = null;
            if (orderD != null)
            {
                string id = null;
                if (orderD.id != null)
                {
                    id = orderD.id;
                }
                double amountInUsd = orderD.amount_in_usd;
                string bitcoinAddress = orderD.bitcoin_address;
                double bitcoinAmount = orderD.bitcoin_amount;
                string bitcoinUri = orderD.bitcoin_uri;
                double currentPricePerBitcoinInUsd = orderD.current_price_per_bitcoin_in_usd;
                DateTime? createdAt = null;
                if (orderD.created_at != null)
                {
                    createdAt = DateTimeExtension.FromUnixTime((long)orderD.created_at);
                }
                DateTime? expiresAt = null;
                if (orderD.expires_at != null)
                {
                    expiresAt = DateTimeExtension.FromUnixTime((long)orderD.expires_at);
                }
                string note = orderD.note;
                bool forSelf = orderD.for_self;
                string provider = orderD.provider;

                createdBitcoinDeposit = new CreatedBitcoinDeposit(id, amountInUsd, bitcoinAddress, bitcoinAmount, 
                    bitcoinUri, currentPricePerBitcoinInUsd, createdAt, expiresAt, note, forSelf, provider);
            }

            return createdBitcoinDeposit;
        }
    }

    /// <summary>
    ///Payment request for Bitcoin. 
    /// </summary>
    public class CreatedBitcoinDeposit
    {
        public string Id { get; private set; }
        public double AmountInUsd { get; private set; }
        public string BitcoinAddress { get; private set; }
        public double BitcoinAmount { get; private set; }
        public string BitcoinUri { get; private set; }
        public double CurrentPricePerBitcoinInUsd { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public DateTime? ExpiresAt { get; private set; }
        public string Note { get; private set; }
        public bool ForSelf { get; private set; }
        public string Provider { get; private set; }

        internal CreatedBitcoinDeposit(string id, double amountInUsd, string bitcoinAddress, double bitcoinAmount, string bitcoinUri,
            double currentPricePerBitcoinInUsd, DateTime? createdAt, DateTime? expiresAt, string note, bool forSelf, string provider)
        {
            Id = id;
            AmountInUsd = amountInUsd;
            BitcoinAddress = bitcoinAddress;
            BitcoinAmount = bitcoinAmount;
            BitcoinUri = bitcoinUri;
            CurrentPricePerBitcoinInUsd = currentPricePerBitcoinInUsd;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
            Note = note;
            ForSelf = forSelf;
            Provider = provider;
        }
    }
}
