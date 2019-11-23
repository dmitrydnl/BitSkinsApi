using Newtonsoft.Json;
using BitSkinsApi.Market;
using BitSkinsApi.CheckParameters;

namespace BitSkinsApi.BuyOrder
{
    /// <summary>
    /// Work with expected place in queue.
    /// </summary>
    public static class PlaceInQueue
    {
        /// <summary>
        /// Allows you to retrieve the expected place in queue for a new buy order without creating the buy order.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="name">The name of the item you want to purchase.</param>
        /// <param name="price">The price at which you want to purchase the item.</param>
        /// <returns>Expected place in queue.</returns>
        public static int? GetExpectedPlaceInQueue(AppId.AppName app, string name, double price)
        {
            CheckParameters(name, price);
            string urlRequest = GetUrlRequest(app, name, price);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            int? expectedPlaceInQueue = ReadExpectedPlaceInQueue(result);
            return expectedPlaceInQueue;
        }

        private static void CheckParameters(string name, double price)
        {
            Checking.NotEmptyString(name, "name");
            Checking.PositiveDouble(price, "price");
        }

        private static string GetUrlRequest(AppId.AppName app, string name, double price)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_expected_place_in_queue_for_new_buy_order/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&name={name}");
            urlCreator.AppendUrl($"&price={price.ToString(System.Globalization.CultureInfo.InvariantCulture)}");

            return urlCreator.ReadUrl();
        }

        private static int? ReadExpectedPlaceInQueue(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic dataD = responseServerD.data;

            int? expectedPlaceInQueue = dataD.expected_place_in_queue ?? null;
            return expectedPlaceInQueue;
        }
    }
}
