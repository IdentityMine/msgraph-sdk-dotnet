// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\IMethodCollectionPage.cs.tt

namespace Microsoft.Graph
{
    using Newtonsoft.Json;

    /// <summary>
    /// The interface IReportRootGetOffice365ServicesUserCountsCollectionPage.
    /// </summary>
    [JsonConverter(typeof(InterfaceConverter<ReportRootGetOffice365ServicesUserCountsCollectionPage>))]
    public interface IReportRootGetOffice365ServicesUserCountsCollectionPage : ICollectionPage<Office365ServicesUserCounts>
    {
        /// <summary>
        /// Gets the next page <see cref="IReportRootGetOffice365ServicesUserCountsRequest"/> instance.
        /// </summary>
        IReportRootGetOffice365ServicesUserCountsRequest NextPageRequest { get; }

        /// <summary>
        /// Initializes the NextPageRequest property.
        /// </summary>
        void InitializeNextPageRequest(IBaseClient client, string nextPageLinkString);
    }
}