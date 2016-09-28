// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.


namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;

    /// <summary>
    /// The type WorkbookFunctionsHex2DecRequestBody.
    /// </summary>
    [DataContract]
    public partial class WorkbookFunctionsHex2DecRequestBody
    {
    
        /// <summary>
        /// Gets or sets Number.
        /// </summary>
        [DataMember(Name = "number", EmitDefaultValue = false, IsRequired = false)]
        public Newtonsoft.Json.Linq.JToken Number { get; set; }
    
    }
}