// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using Microsoft.Graph;
using Microsoft.Graph.DotnetCore.Test.Requests.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Diagnostics;

namespace Microsoft.Graph.DotnetCore.Test.Models
{
    public class PageIteratorTests : GraphTestBase
    {
        private PageIterator<Event> pageIterator;

        [Fact]
        public async Task PageIteratorDevTest()
        {
            // Get an initial page results to populate the iterator.
            IUserEventsCollectionPage iUserEventsCollectionPage = await graphClient.Me.Events.Request().Top(2).GetAsync();

            // Create the function to process each entity returned in the pages
            Func<Event,bool> processUser = (e) =>
            {
                bool conditionToSatisfy = true;

                Debug.WriteLine($"Event subject: {e.Subject}");
                return conditionToSatisfy;
            }; 

            // This requires the dev to specify the generic type in the CollectionPage.
            pageIterator = PageIterator<Event>.CreatePageIterator(iUserEventsCollectionPage, processUser);

            await pageIterator.IterateAsync(false);
        }

    }
}
