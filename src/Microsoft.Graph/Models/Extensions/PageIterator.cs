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
        private ICollectionPage<T> CurrentPage { get; set; } 
        private Queue<ICollectionPage<T>> PageQueue { get; set; }
        private Func<T, bool> ProcessPageItem { get; set; }

        // Can this be done in a cstor?
        // Can I pass in an IUserEventsCollectionPage which is an ICollectionPage<Event> and access nextPageRequest?
        // Can we change ICollectionPage to define NextPageRequest and InitNextPageRequest?
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="processPageItems">T: the type of object in the collection. bool: return condition when to stop iterating.</param>
        /// <returns></returns>
        public static PageIterator<T> CreatePageIterator(ICollectionPage<T> page, Func<T,bool> processPageItems)
        {
            return new PageIterator<T>()
            {
                CurrentPage = page,
                ProcessPageItem = processPageItems,
                PageQueue = new Queue<ICollectionPage<T>>()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefetchNextPage"></param>
        /// <returns></returns>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">Thrown when a base CollectionPage 
        /// is provided to the PageIterator</exception>
        /// TODO: Handle prefetch
        public async Task IterateAsync(bool prefetchNextPage = false)
        {
            // Runtime determines the type of the ICollectionPage<T>. We need access to the NextPageRequest
            // to call and get the next page.
            dynamic page = CurrentPage;

            bool morePages = true;

            do
            {
                // What happens with heterogenous items in result?
                // Process the current ICollectionPage<T> with Func<T> and release reference.
                foreach (T item in page)
                {
                    bool shouldContinue = ProcessPageItem(item);

                    if (!shouldContinue)
                    {
                        morePages = false;
                        continue;
                    }

                    Debug.WriteLine($"Results: {shouldContinue}");
                }

                // Can expect a RuntimeBinderException here if someone uses a base CollectionPage object. It really should be abstract.
                // TODO: make backlog issue to make CollectionPage abstract.
                if (page.NextPageRequest != null && morePages)
                {
                    page = await page.NextPageRequest.GetAsync();
                }
                else
                {
                    morePages = false;
                }
                

            } while (morePages);

            // TODO: Investigate the use of the queue to queue up requests. 
            // Would the queue run out if the requests take too long?
            // Need to wrap the queue in something that indicates that it is no longer getting updated with CollectionPages.
            Queue<ICollectionPage<T>> queue = new Queue<ICollectionPage<T>>(); 
        }
    }
}
