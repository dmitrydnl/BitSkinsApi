﻿/*
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

using System.Text;

namespace BitSkinsApi.Server
{
    internal class UrlCreator
    {
        private readonly StringBuilder Url;

        public UrlCreator(string urlStart)
        {
            Url = new StringBuilder(urlStart);
            Url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            Url.Append($"&code={Account.Secret.GetTwoFactorCode()}");
        }

        internal void AppendUrl(string url)
        {
            Url.Append(url);
        }

        internal string ReadUrl()
        {
            return Url.ToString();
        }
    }
}
