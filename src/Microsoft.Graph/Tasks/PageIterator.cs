using System;
using System.Threading.Tasks;

/**

Spec
    https://github.com/microsoftgraph/msgraph-sdk-design/blob/master/tasks/PageIteratorTask.md
**/

namespace Microsoft.Graph
{
    /// <summary>
    /// Use PageIterator&lt;T&gt; to automatically page through result sets across multiple calls 
    /// and process each item in the result set.
    /// </summary>
    /// <typeparam name="T">The common entity type returned in the result set.</typeparam>
    public class PageIterator<T> where T : Entity
    {
        private ICollectionPage<T> initialPage;
        private Func<T, bool> processPageItem;

        /// <summary>
        /// Creates the PageIterator with the results of an initial paged request. 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="processPageItems">A Func delegate that processes type T in the result set and should return false if the iterator should cancel processing.</param>
        /// <returns>A PageIterator&lt;T&gt; that will process additional result pages based on the rules specified in Func&lt;T,bool&gt; processPageItems</returns>
        public static PageIterator<T> CreatePageIterator(ICollectionPage<T> page, Func<T,bool> processPageItems)
        {
            if (page == null)
                throw new ArgumentNullException("page");

            if (processPageItems == null)
                throw new ArgumentNullException("processPageItems");

            return new PageIterator<T>()
            {
                initialPage = page,
                processPageItem = processPageItems
            };
        }

        /// <summary>
        /// Fetches page collections and iterates through each page of items and processes it according to the Func&lt;T, bool&gt; set in <see cref="CreatePageIterator"/>. 
        /// </summary>
        /// <returns>The task object that represents the results of this asynchronous operation.</returns>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">Thrown when a base CollectionPage does not implement NextPageRequest.
        /// is provided to the PageIterator</exception>
        public async Task IterateAsync()
        {
            // We need access to the NextPageRequest to call and get the next page. ICollectionPage<T> doesn't define NextPageRequest.
            // We are making this dynamic so we can access NextPageRequest.
            dynamic page = initialPage;

            bool shouldFetchMorePages = true; // Set false if no more pages to fetch or if processPageItem() returns false

            do
            {
                // Process each item in a page.
                foreach (T item in page)
                {
                    bool shouldContinue = processPageItem(item);

                    // Cancel processing of items in the page and stop requesting more pages.
                    if (!shouldContinue)
                    {
                        shouldFetchMorePages = false;
                        break;
                    }
                }

                // Fetch the next page of results. RuntimeBinderException expection can be thrown if page is a base CollectionPage object.
                if (page.NextPageRequest != null && shouldFetchMorePages)
                {
                    page = await page.NextPageRequest.GetAsync();
                }
                else
                {
                    shouldFetchMorePages = false;
                }
            } while (shouldFetchMorePages);
        }
    }
}