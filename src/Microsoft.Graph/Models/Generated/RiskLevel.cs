// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Model\EnumType.cs.tt


namespace Microsoft.Graph
{
    using Newtonsoft.Json;

    /// <summary>
    /// The enum RiskLevel.
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    public enum RiskLevel
    {
    
        /// <summary>
        /// low
        /// </summary>
        Low = 0,
	
        /// <summary>
        /// medium
        /// </summary>
        Medium = 1,
	
        /// <summary>
        /// high
        /// </summary>
        High = 2,
	
    }
}