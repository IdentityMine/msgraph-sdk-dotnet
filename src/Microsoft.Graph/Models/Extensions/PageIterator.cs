using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Graph
{
    /**
    https://github.com/microsoftgraph/msgraph-sdk-dotnet/blob/dev/src/Microsoft.Graph.Core/Models/CollectionPage.cs
    https://github.com/microsoftgraph/msgraph-sdk-dotnet/blob/dev/src/Microsoft.Graph.Core/Models/ICollectionPage.cs
    https://github.com/microsoftgraph/msgraph-sdk-dotnet/blob/dev/src/Microsoft.Graph/Requests/Generated/IUserEventsCollectionPage.cs
    https://github.com/microsoftgraph/msgraph-sdk-dotnet/blob/dev/tests/Microsoft.Graph.Test/Requests/Functional/OneDriveTests.cs#L201
    The challenge is that it NextRequestLink set on the on the IUserEventCollectionpage interface and not the ICollectionPage<T>.
    
    Spec
        https://github.com/microsoftgraph/msgraph-sdk-design/blob/master/tasks/PageIteratorTask.md
    **/
    public class PageIterator<T> where T : Entity
    {
        private GraphServiceClient Client { get; set; }
        private ICollectionPage<T> CurrentPage { get; set; } // Could we make this dynamic to access NextPageRequest?
        //private dynamic CurrentPage { get; set; }
        private Func<T, bool> ProcessPages { get; set; }

        // Can this be done in a cstor?
        // Can I pass in an IUserEventsCollectionPage which is an ICollectionPage<Event> and access nextPageRequest?
        // Can we change ICollectionPage to define NextPageRequest and InitNextPageRequest?
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="page"></param>
        /// <param name="processPages">T: the type of object in the collection. bool: return condition when to stop iterating.</param>
        /// <returns></returns>
        public static PageIterator<T> CreatePageIterator(ICollectionPage<T> page, Func<T,bool> processPages)
        {
            return new PageIterator<T>()
            {
                Client = client,
                CurrentPage = page,
                ProcessPages = processPages
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefetchNextPage"></param>
        /// <returns></returns>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">Thrown when a base CollectionPage 
        /// is provided to the PageIterator</exception>
        public async Task IterateAsync(bool prefetchNextPage = false)
        {
            dynamic nextPage; // contains the next page to be consumed by the iterator.

            // Fetch the next page 
            if (prefetchNextPage)
            {
                // We need make this dyanmic to access 
                dynamic page = CurrentPage;

                // TODO: check that the there is a nextLink and use the client
                // to get the next page link. Ideally, if we have a GraphServiceClient,
                // we can get use the NextPageRequest. 
                if (page.NextPageRequest != null)
                {
                    nextPage = await page.NextPageRequest.GetAsync();
                }

                // TODO: Loop through each request, clone out results, and call the delegate.
            }
            
            // Process the current ICollectionPage<T> with Func<T> and release reference.
            foreach (T e in CurrentPage)
            {
                bool result = ProcessPages(e);

                Debug.WriteLine($"Results: {result}");
            }

            // Runtime determines the type of the ICollectionPage<T>. We need access to the NextPageRequest
            // to call and get the next page.
            dynamic page = CurrentPage;
            

            // Can expect a RuntimeBinderException here if someone uses a base CollectionPage object. It really should be abstract.
            // TODO: make backlog issue to make CollectionPage abstract.
            if (page.NextPageRequest != null)
            {
                nextPage = await page.NextPageRequest.GetAsync();
            }

            // Access nextLink and make next paged call.
            object nextLink;
            if (CurrentPage.AdditionalData.TryGetValue("@odata.nextLink", out nextLink))
            {
                // No reflection in standard 1.1.
                //Type pageType = CurrentPage.GetType();
                //pageType.GetProperty("NextPageRequest", typeof(IBaseRequest));


                // If it does, process it with Func<T>
                Debug.WriteLine($"Nextlink: {nextLink.ToString()}");
            }
        }
    }
}
