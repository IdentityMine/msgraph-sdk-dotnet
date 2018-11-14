// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\EntityRequest.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Linq.Expressions;

    /// <summary>
    /// The type PrivilegedRoleRequest.
    /// </summary>
    public partial class PrivilegedRoleRequest : BaseRequest, IPrivilegedRoleRequest
    {
        /// <summary>
        /// Constructs a new PrivilegedRoleRequest.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="IBaseClient"/> for handling requests.</param>
        /// <param name="options">Query and header option name value pairs for the request.</param>
        public PrivilegedRoleRequest(
            string requestUrl,
            IBaseClient client,
            IEnumerable<Option> options)
            : base(requestUrl, client, options)
        {
        }

        /// <summary>
        /// Creates the specified PrivilegedRole using POST.
        /// </summary>
        /// <param name="privilegedRoleToCreate">The PrivilegedRole to create.</param>
        /// <returns>The created PrivilegedRole.</returns>
        public System.Threading.Tasks.Task<PrivilegedRole> CreateAsync(PrivilegedRole privilegedRoleToCreate)
        {
            return this.CreateAsync(privilegedRoleToCreate, CancellationToken.None);
        }

        /// <summary>
        /// Creates the specified PrivilegedRole using POST.
        /// </summary>
        /// <param name="privilegedRoleToCreate">The PrivilegedRole to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created PrivilegedRole.</returns>
        public async System.Threading.Tasks.Task<PrivilegedRole> CreateAsync(PrivilegedRole privilegedRoleToCreate, CancellationToken cancellationToken)
        {
            this.ContentType = "application/json";
            this.Method = "POST";
            var newEntity = await this.SendAsync<PrivilegedRole>(privilegedRoleToCreate, cancellationToken).ConfigureAwait(false);
            this.InitializeCollectionProperties(newEntity);
            return newEntity;
        }

        /// <summary>
        /// Deletes the specified PrivilegedRole.
        /// </summary>
        /// <returns>The task to await.</returns>
        public System.Threading.Tasks.Task DeleteAsync()
        {
            return this.DeleteAsync(CancellationToken.None);
        }

        /// <summary>
        /// Deletes the specified PrivilegedRole.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        public async System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken)
        {
            this.Method = "DELETE";
            await this.SendAsync<PrivilegedRole>(null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the specified PrivilegedRole.
        /// </summary>
        /// <returns>The PrivilegedRole.</returns>
        public System.Threading.Tasks.Task<PrivilegedRole> GetAsync()
        {
            return this.GetAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets the specified PrivilegedRole.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The PrivilegedRole.</returns>
        public async System.Threading.Tasks.Task<PrivilegedRole> GetAsync(CancellationToken cancellationToken)
        {
            this.Method = "GET";
            var retrievedEntity = await this.SendAsync<PrivilegedRole>(null, cancellationToken).ConfigureAwait(false);
            this.InitializeCollectionProperties(retrievedEntity);
            return retrievedEntity;
        }

        /// <summary>
        /// Updates the specified PrivilegedRole using PATCH.
        /// </summary>
        /// <param name="privilegedRoleToUpdate">The PrivilegedRole to update.</param>
        /// <returns>The updated PrivilegedRole.</returns>
        public System.Threading.Tasks.Task<PrivilegedRole> UpdateAsync(PrivilegedRole privilegedRoleToUpdate)
        {
            return this.UpdateAsync(privilegedRoleToUpdate, CancellationToken.None);
        }

        /// <summary>
        /// Updates the specified PrivilegedRole using PATCH.
        /// </summary>
        /// <param name="privilegedRoleToUpdate">The PrivilegedRole to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The updated PrivilegedRole.</returns>
        public async System.Threading.Tasks.Task<PrivilegedRole> UpdateAsync(PrivilegedRole privilegedRoleToUpdate, CancellationToken cancellationToken)
        {
            this.ContentType = "application/json";
            this.Method = "PATCH";
            var updatedEntity = await this.SendAsync<PrivilegedRole>(privilegedRoleToUpdate, cancellationToken).ConfigureAwait(false);
            this.InitializeCollectionProperties(updatedEntity);
            return updatedEntity;
        }

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        public IPrivilegedRoleRequest Expand(string value)
        {
            this.QueryOptions.Add(new QueryOption("$expand", value));
            return this;
        }

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        public IPrivilegedRoleRequest Expand(Expression<Func<PrivilegedRole, object>> expandExpression)
        {
		    if (expandExpression == null)
            {
                throw new ArgumentNullException(nameof(expandExpression));
            }
            string error;
            string value = ExpressionExtractHelper.ExtractMembers(expandExpression, out error);
            if (value == null)
            {
                throw new ArgumentException(error, nameof(expandExpression));
            }
            else
            {
                this.QueryOptions.Add(new QueryOption("$expand", value));
            }
            return this;
        }

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        public IPrivilegedRoleRequest Select(string value)
        {
            this.QueryOptions.Add(new QueryOption("$select", value));
            return this;
        }

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        public IPrivilegedRoleRequest Select(Expression<Func<PrivilegedRole, object>> selectExpression)
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }
            string error;
            string value = ExpressionExtractHelper.ExtractMembers(selectExpression, out error);
            if (value == null)
            {
                throw new ArgumentException(error, nameof(selectExpression));
            }
            else
            {
                this.QueryOptions.Add(new QueryOption("$select", value));
            }
            return this;
        }

        /// <summary>
        /// Initializes any collection properties after deserialization, like next requests for paging.
        /// </summary>
        /// <param name="privilegedRoleToInitialize">The <see cref="PrivilegedRole"/> with the collection properties to initialize.</param>
        private void InitializeCollectionProperties(PrivilegedRole privilegedRoleToInitialize)
        {

            if (privilegedRoleToInitialize != null && privilegedRoleToInitialize.AdditionalData != null)
            {

                if (privilegedRoleToInitialize.Assignments != null && privilegedRoleToInitialize.Assignments.CurrentPage != null)
                {
                    privilegedRoleToInitialize.Assignments.AdditionalData = privilegedRoleToInitialize.AdditionalData;

                    object nextPageLink;
                    privilegedRoleToInitialize.AdditionalData.TryGetValue("assignments@odata.nextLink", out nextPageLink);
                    var nextPageLinkString = nextPageLink as string;

                    if (!string.IsNullOrEmpty(nextPageLinkString))
                    {
                        privilegedRoleToInitialize.Assignments.InitializeNextPageRequest(
                            this.Client,
                            nextPageLinkString);
                    }
                }

            }


        }
    }
}