// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\EntityCollectionRequestBuilder.cs.tt
namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The type DeviceAppManagementWindowsInformationProtectionPoliciesCollectionRequestBuilder.
    /// </summary>
    public partial class DeviceAppManagementWindowsInformationProtectionPoliciesCollectionRequestBuilder : BaseRequestBuilder, IDeviceAppManagementWindowsInformationProtectionPoliciesCollectionRequestBuilder
    {
        /// <summary>
        /// Constructs a new DeviceAppManagementWindowsInformationProtectionPoliciesCollectionRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="IBaseClient"/> for handling requests.</param>
        public DeviceAppManagementWindowsInformationProtectionPoliciesCollectionRequestBuilder(
            string requestUrl,
            IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public IDeviceAppManagementWindowsInformationProtectionPoliciesCollectionRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public IDeviceAppManagementWindowsInformationProtectionPoliciesCollectionRequest Request(IEnumerable<Option> options)
        {
            return new DeviceAppManagementWindowsInformationProtectionPoliciesCollectionRequest(this.RequestUrl, this.Client, options);
        }

        /// <summary>
        /// Gets an <see cref="IWindowsInformationProtectionPolicyRequestBuilder"/> for the specified DeviceAppManagementWindowsInformationProtectionPolicy.
        /// </summary>
        /// <param name="id">The ID for the DeviceAppManagementWindowsInformationProtectionPolicy.</param>
        /// <returns>The <see cref="IWindowsInformationProtectionPolicyRequestBuilder"/>.</returns>
        public IWindowsInformationProtectionPolicyRequestBuilder this[string id]
        {
            get
            {
                return new WindowsInformationProtectionPolicyRequestBuilder(this.AppendSegmentToRequestUrl(id), this.Client);
            }
        }

        
    }
}
