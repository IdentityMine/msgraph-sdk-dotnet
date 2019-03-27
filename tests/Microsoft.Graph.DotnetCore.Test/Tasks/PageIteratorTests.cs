// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Graph.DotnetCore.Test.Requests.Functional;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.Graph.DotnetCore.Test.Tasks
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
                bool shouldContinue = true;

                if (e.Subject.Contains("Latin"))
                    shouldContinue = false;
                Debug.WriteLine($"Event subject: {e.Subject}");
                return shouldContinue;
            }; 

            // This requires the dev to specify the generic type in the CollectionPage.
            pageIterator = PageIterator<Event>.CreatePageIterator(iUserEventsCollectionPage, processUser);

            await pageIterator.IterateAsync();
        }

        [Fact]
        public async Task Given_Concrete_CollectionPage_It_Throws_RuntimeBinderException()
        {
            pageIterator = PageIterator<Event>.CreatePageIterator(new CollectionPage<Event>(), (e) => { return true; });
            await Assert.ThrowsAsync<RuntimeBinderException>(() => pageIterator.IterateAsync());
        }

        [Fact]
        public void Given_Null_CollectionPage_It_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => PageIterator<Event>.CreatePageIterator(null, (e) => { return true; }));
        }

        [Fact]
        public void Given_Null_Delegate_It_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => PageIterator<Event>.CreatePageIterator(new CollectionPage<Event>(), null));
        }

        [Fact]
        public async Task Given_Concrete_Generated_CollectionPage_It_Iterates_Page_Items()
        {
            int inputEventCount = 17;
            var page = new UserEventsCollectionPage();
            for (int i = 0; i < inputEventCount; i++)
            {
                page.Add(new Event() { Subject = $"Subject{i.ToString()}" });
            }

            List<Event> events = new List<Event>();

            pageIterator = PageIterator<Event>.CreatePageIterator(page, (e) => 
            {
                events.Add(e);
                return true;
            });

            await pageIterator.IterateAsync();

            Assert.Equal(inputEventCount, events.Count);
        }

        [Fact]
        public async Task Given_Concrete_Generated_CollectionPage_It_Stops_Iterating_Pageitems()
        {
            int inputEventCount = 17;
            var page = new UserEventsCollectionPage();
            for (int i = 0; i < inputEventCount; i++)
            {
                page.Add(new Event() { Subject = $"Subject{i.ToString()}" });
            }

            List<Event> events = new List<Event>();

            pageIterator = PageIterator<Event>.CreatePageIterator(page, (e) =>
            {
                if (e.Subject == "Subject7")
                    return false;
                
                events.Add(e);
                return true;
            });

            await pageIterator.IterateAsync();

            Assert.Equal(7, events.Count);
        }

        // Given_Concrete_Generated_CollectionPage_It_Stops_Iterating_Across_Pages
        // Given_Concrete_Generated_CollectionPage_It_Iterates_Across_Pages
    }
}
