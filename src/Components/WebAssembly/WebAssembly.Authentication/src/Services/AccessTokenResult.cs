// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.Components.WebAssembly.Authentication
{
    /// <summary>
    /// Represents the result of trying to provision an access token.
    /// </summary>
    public class AccessTokenResult
    {
        private readonly AccessToken _token;
        private readonly NavigationManager _navigation;

        /// <summary>
        /// Initializes a new instance of <see cref="AccessTokenResult"/>.
        /// </summary>
        /// <param name="status">The status of the result.</param>
        /// <param name="token">The <see cref="AccessToken"/> in case it was successful.</param>
        /// <param name="navigation">The <see cref="NavigationManager"/> to perform redirects.</param>
        /// <param name="redirectUrl">The redirect uri to go to for provisioning the token.</param>
        public AccessTokenResult(AccessTokenResultStatus status, AccessToken token, NavigationManager navigation, string redirectUrl)
        {
            Status = status;
            _token = token;
            _navigation = navigation;
            RedirectUrl = redirectUrl;
        }

        /// <summary>
        /// Gets or sets the status of the current operation. See <see cref="AccessTokenResultStatus"/> for a list of statuses.
        /// </summary>
        public AccessTokenResultStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the URL to redirect to if <see cref="Status"/> is <see cref="AccessTokenResultStatus.RequiresRedirect"/>.
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Determines whether the token request was successful and makes the <see cref="AccessToken"/> available for use when it is.
        /// </summary>
        /// <param name="accessToken">The <see cref="AccessToken"/> if the request was successful.</param>
        /// <returns><c>true</c> when the token request is successful; <c>false</c> otherwise.</returns>
        public bool TryGetToken(out AccessToken accessToken)
        {
            if (Status == AccessTokenResultStatus.Success)
            {
                accessToken = _token;
                return true;
            }
            else
            {
                accessToken = null;
                return false;
            }
        }

        /// <summary>
        /// Determines whether the token request was successful and makes the <see cref="AccessToken"/> available for use when it is.
        /// </summary>
        /// <param name="accessToken">The <see cref="AccessToken"/> if the request was successful.</param>
        /// <param name="redirect">Whether or not to redirect automatically when failing to provision a token.</param>
        /// <returns><c>true</c> when the token request is successful; <c>false</c> otherwise.</returns>
        public bool TryGetToken(out AccessToken accessToken, bool redirect)
        {
            if (TryGetToken(out accessToken))
            {
                return true;
            }
            else
            {
                if (redirect)
                {
                    _navigation.NavigateTo(RedirectUrl);
                }
                return false;
            }
        }
    }
}
